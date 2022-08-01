using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Infarstructure;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext context;

        public ToDoController(ToDoContext context)
        {
            this.context = context; 

        }
        //GET / 
        public  async Task<ActionResult> Index()
        {
            IQueryable<ToDoList> items = from i in context.ToDoList orderby i.Id select i;

            List<ToDoList> todolist = await items.ToListAsync();

            return View(todolist); 
        }

        //GET /Create 

        public IActionResult Create() => View();

        //POST /Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ToDoList item)
        {
            if(ModelState.IsValid )
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The ToDo has been added! ";

                return RedirectToAction("Index"); 
            }

            return View(item);
        }
        //GET /edit/id 
        public async Task<ActionResult> Edit(int Id)
        {
            ToDoList item = await context.ToDoList.FindAsync(Id); 
            if(item == null )
            {
                return NotFound(); 
            }

            return View(item);
        }
        //POST /edit/id 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ToDoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The ToDo has been Updated! ";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        //GET /delete/id 
        public async Task<ActionResult> Delete(int Id)
        {
            ToDoList item = await context.ToDoList.FindAsync(Id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exitst ! ";
            } else
            {
                context.ToDoList.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The ToDo has been Deleted! ";

                return RedirectToAction("Index");

            }

            return View(item);
        }
    }
}
