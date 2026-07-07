import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { TodoItem } from '../../models/todo-item';

@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todos.html',
  styleUrl: './todos.scss',
})
export class TodosComponent implements OnInit {
  private readonly todoService = inject(TodoService);

  todos = signal<TodoItem[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadTodos();
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
        this.error.set('Failed to load todos');
        this.loading.set(false);
      },
    });
  }
}
