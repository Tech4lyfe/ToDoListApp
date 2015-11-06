using ToDoListApp.Models.TodoEntities;

namespace ToDoListApp.Models.ViewModels.Todo
{
    public class TodoDetailViewModel
    {
        public ToDoItem NewToDoItem { get; set; }
        public ToDoList ToDoList { get; set; }
       
    }
}