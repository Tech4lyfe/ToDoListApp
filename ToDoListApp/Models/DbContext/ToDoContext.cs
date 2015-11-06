using System.Data.Entity;
using ToDoListApp.Models.TodoEntities;

namespace ToDoListApp.Models.DbContext
{
    public class ToDoContext : System.Data.Entity.DbContext
    {
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public ToDoContext() : base("DefaultConnection")
        {
        }
    }
}