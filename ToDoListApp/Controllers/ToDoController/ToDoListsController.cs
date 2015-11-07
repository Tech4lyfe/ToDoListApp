using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDoListApp.Models.DbContext;
using ToDoListApp.Models.TodoEntities;
using ToDoListApp.Models.ViewModels.Todo;

namespace ToDoListApp.Controllers.ToDoController
{
    //Todo: I probably would have seperated some of these methods into two seperate controllers (ToDoItemsController)
    public class ToDoListsController : Controller
    {
        private ToDoContext _db = new ToDoContext();

        // GET: ToDoLists
        public ActionResult Index()
        {
            return View(_db.ToDoLists.ToList());
        }


        // GET: ToDoLists/Details/5
        public ActionResult Details(int? id)
        {
            var model = new TodoDetailViewModel();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            model.ToDoList = _db.ToDoLists.Find(id);
            if (model.ToDoList == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }


        // GET: ToDoLists/Create
        public ActionResult Create()
        {
            //Todo: What is this???
           

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                toDoList.CreateDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                _db.ToDoLists.Add(toDoList);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDoList);
        }

        // GET: ToDoLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = _db.ToDoLists.Find(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            return View(toDoList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(toDoList).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = _db.ToDoLists.Find(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            return View(toDoList);
        }

        // POST: ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoList toDoList = _db.ToDoLists.Find(id);
            _db.ToDoLists.Remove(toDoList);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: ToDoItem/Create
        [HttpGet]
        public ActionResult CreateToDoItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ToDoList toDoList = _db.ToDoLists.Find(id);

            if (toDoList == null)
            {
                return HttpNotFound();
            }

            var model = new ToDoCreateViewModel()
            {
                ToDoList = toDoList
            };

            return View(model);
        }

        // POST: ToDoItem/Create
        [HttpPost]
        public JsonResult CreateToDoItem(string entry, int? toDoListId)
        {
            ToDoItem todoItem = null;

            if (ModelState.IsValid)
            {
                var todoList = _db.ToDoLists.FirstOrDefault(x => x.ToDoListId == toDoListId); //First Todolis that matches the todolist ids

                todoItem = new ToDoItem()
                {
                    Completed = false,
                    Entry = entry,
                    ToDoList = todoList
                };

                _db.ToDoItems.Add(todoItem);
                _db.SaveChanges();
            }
            else
            {
                var errorResult = new { success = "False", Message = "Error Message" };
                return Json(errorResult);
            }

            var response = new { success = true, item = todoItem.Entry, itemId = todoItem.ToDoItemId };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // POST: ToDoItem/Completed
        [HttpPost]
        public JsonResult CompletedTask(int? toDoItemId)
        {

            if (ModelState.IsValid)
            {
                var toDoItem = _db.ToDoItems.FirstOrDefault(x => x.ToDoItemId == toDoItemId);

                if (toDoItem != null) toDoItem.Completed = true;

                _db.SaveChanges();

            }
            else
            {
                var errorResult = new { success = "False", Message = "Error Message" };
                return Json(errorResult);
            }
            var response = new { success = true, itemId = toDoItemId };


            return Json(response);

        }


        // POST: ToDoItem/Delete
        [HttpPost]
        public JsonResult DeletedTodoItem(int? toDoItemId)
        {
            //Todo: Clean up spaces
            if (ModelState.IsValid)
            {
                var toDoItem = _db.ToDoItems.FirstOrDefault(X => X.ToDoItemId == toDoItemId);
                _db.ToDoItems.Remove(toDoItem);
                _db.SaveChanges();


            }
            else
            {
                var errorResult = new { success = "False", Message = "Error Message" };
                return Json(errorResult);
            }
            var response = new { success = "True", Message = "Success" };


            return Json(response);
        }

    }
}
