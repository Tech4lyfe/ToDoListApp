namespace ToDoListApp.Models.TodoEntities
{
    public class ToDoItem
    {
        public int? ToDoItemId { get; set; }

        public ToDoList ToDoList { get; set; }

        public bool Completed { get; set; }

        public string Entry { get; set; }

        

           
    }
}