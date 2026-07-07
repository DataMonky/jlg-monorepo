import { Component } from '@angular/core';
import { TodosComponent } from './components/todos/todos';

@Component({
  selector: 'app-root',
  imports: [TodosComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {}
