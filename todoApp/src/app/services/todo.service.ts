import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TodoItem } from '../models/todo-item';

const API_URL = 'http://localhost:5249/todoitems';

@Injectable({ providedIn: 'root' })
export class TodoService {
  private readonly http = inject(HttpClient);

  getAll(): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]>(API_URL);
  }

  create(item: Omit<TodoItem, 'id'>): Observable<TodoItem> {
    return this.http.post<TodoItem>(API_URL, item);
  }

  update(id: number, item: TodoItem): Observable<void> {
    return this.http.put<void>(`${API_URL}/${id}`, item);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/${id}`);
  }
}
