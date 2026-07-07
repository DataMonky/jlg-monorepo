namespace todoApi.Repositories;

using Microsoft.EntityFrameworkCore;

public interface ITodoRepository                                                        
  {
      Task<List<Todo>> GetAllAsync();
      Task<Todo> CreateAsync(Todo todo);                                                                                                  
      Task<bool> UpdateAsync(int id, Todo input);
      Task<bool> DeleteAsync(int id);                                                                                                     
  }                                                                                       

  public class TodoRepository(TodoDb db) : ITodoRepository                                                                                
  {
      public async Task<List<Todo>> GetAllAsync() =>                                                                                      
          await db.Todos.ToListAsync();                                                   
                                                                                                                                          
      public async Task<Todo> CreateAsync(Todo todo)
      {                                                                                                                                   
          db.Todos.Add(todo);                                                             
          await db.SaveChangesAsync();
          return todo;
      }

      public async Task<bool> UpdateAsync(int id, Todo input)                                                                             
      {
          var todo = await db.Todos.FindAsync(id);                                                                                        
          if (todo is null) return false;                                                 
          todo.Name = input.Name;
          todo.IsComplete = input.IsComplete;
          await db.SaveChangesAsync();                                                                                                    
          return true;
      }                                                                                                                                   
                                                                                          
      public async Task<bool> DeleteAsync(int id)
      {
          if (await db.Todos.FindAsync(id) is Todo todo)
          {                                                                                                                               
              db.Todos.Remove(todo);
              await db.SaveChangesAsync();                                                                                                
              return true;                                                                
          }
          return false;
      }
  }