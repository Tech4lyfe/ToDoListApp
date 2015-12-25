using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Models.TodoEntities
{
    public class ToDoList
    {
        //Todo: Spacing
        public int? ToDoListId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Create Date")]
        [ReadOnly(true)]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDateTime { get; set; }

       

        public virtual List<ToDoItem> ToDoItems { get; set; }
    }
}