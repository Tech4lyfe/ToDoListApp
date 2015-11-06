//Todo: I would have created a root level js folder
/// <reference path="ToDoList.js" />
/// <reference path="~/Scripts/jquery-2.1.4.intellisense.js" />
/// <reference path="~/Scripts/jquery-ui-1.11.4.js" />
$(function () {
    "use strict";
  
    //Todo: Why did you create another iffy
    $(function () {
          $("#sortable").sortable();
         
       });
  
    //Todo: Would be easier to read if you created functions
    $(".addButton").on("click", function () {
        var $newTask = $("#entryValue").val();
        //Todo: Could make TodoListId global
        var $TodolistId = $("#ToDoListId").val();

        $.ajax({
            method: "Post",
            url: "/ToDoLists/CreateToDoItem",
            data: { Entry: $newTask, ToDoListId: $TodolistId },
            success: function (result) {
                var $toDoList = $(".uncompleted").append("<li class='list-group-item clearfix'>" + "<div style='overflow:hidden'>" + "<input id='ToDoItemId' name='ToDoItemId' type='hidden' value= " + result.itemId + "> " + "" + result.item + "" + "" + "<button class='doneButton btn btn-success btn-sm pull-right'>" + "<span class='glyphicon glyphicon-ok'></span>" + "</button>" + " " + "</div>" + "</li>");
            },
            error: function () {
                alert("error");
            }
        });
    });

    //When Success button Clicked, remove task from todo list and append to completed tasks

    $(document).on("click", ".doneButton", function (e) {
        e.defaultPrevented;
        var $clickedElement = $(this);
        var $toDoItemId = $clickedElement.siblings("#ToDoItemId").val();

        $.ajax({
            method: "Post",
            url: "/ToDoLists/CompletedTask",
            data: { toDoItemId: $toDoItemId },
            success: function (result) {
                //Todo: Instead of rewriting the html. It would be easier to copy detach the clickon html and then attach it too the completed list
                var completedTask = $clickedElement.offsetParent().text();
                $clickedElement.offsetParent().remove();
                var $doneList = $(".completed").append("<li class='list-group-item clearfix'>" + "<div style='overflow:hidden'>"+"<input id='CompletedToDoItemId' name='CompletedToDoItemId' type='hidden' value= " + result.itemId + "> " + completedTask + "<button class='deleteButton btn btn-danger btn-small pull-right'>" + "<span class='glyphicon glyphicon-remove'></span>" + "</button>" + "</li>");
            },
            error: function () {
                alert("error");
            }
        });
    });

    $(document).on("click", ".deleteButton", function () {
        var removeClickElement = $(this);
        var $ToDoItemId = removeClickElement.siblings("#CompletedToDoItemId").val();

        $.ajax({
            method: "Post",
            url: "/ToDoLists/DeletedTodoItem",
            data: { toDoItemId: $ToDoItemId },
            success: function () {
                
                    var removeTask = removeClickElement.offsetParent().remove();
                
            },
            error: function () {
                alert("error");
            }
        });
    });
})();
















