import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { TodoItem } from '../../models/todo-item';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './todos.html',
  styleUrl: './todos.scss',
})
export class TodosComponent implements OnInit {
  private readonly todoService = inject(TodoService);
  private readonly fb = inject(FormBuilder);

  form: FormGroup = this.fb.group({
    name: ['', Validators.required],
    isComplete: [false],
  });

  todos = signal<TodoItem[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadTodos();
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.error.set(null);

    this.todoService.create(this.form.value).subscribe({
      next: (newTodo) => {
        this.todos.update((currentTodos) => [...currentTodos, newTodo]);
        this.form.reset({ name: '', isComplete: false });
      },
      error: (err) => {
        console.error('Error creating todo:', err);
        this.error.set('Failed to create todo. Please try again.');
      },
    });
  }

  deleteTodo(todo: TodoItem): void {
    this.error.set(null);

    this.todoService.delete(todo.id).subscribe({
      next: () => {
        this.todos.update((currentTodos) => currentTodos.filter((t) => t.id !== todo.id));
      },
      error: (err) => {
        console.error('Error deleting todo:', err);
        this.error.set('Failed to delete todo. Please try again.');
      },
    });
  }

  toggleComplete(todo: TodoItem): void {                                                                                                  
    this.error.set(null);                                                                                                                 
                                                                                                                                          
    const updated = { ...todo, isComplete: !todo.isComplete };
    this.todoService.update(todo.id, updated).subscribe({                                                                                 
      next: () => {                                                                      
        this.todos.update((currentTodos) =>
          currentTodos.map((t) => (t.id === todo.id ? updated : t))                                                                       
        );
      },                                                                                                                                  
      error: (err) => {                                                                  
        console.error('Failed to update todo:', err);
        this.error.set('Failed to update todo. Please try again.');                                                                       
      },
    });                                                                                                                                   
  } 

  private loadTodos(): void {
    this.loading.set(true);
    this.error.set(null);

    this.todoService.getAll().subscribe({
      next: (data) => {
        this.todos.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err.message || 'Failed to load todos');
        this.loading.set(false);
      },
    });
  }
}
