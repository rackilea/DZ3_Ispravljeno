using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DZ3.Interfaces;
using Microsoft.AspNetCore.Identity;
using zad2.Models;

namespace zad2.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public TodoController(ITodoRepository repository,UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<Guid> LoggedInUser()
        {
            // Get currently logged -in user using userManager
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            return (Guid.Parse(currentUser.Id));
        }

        public async Task<IActionResult> Index()
        {
            Guid userId = await Task.Run(LoggedInUser);
            var todos = _repository.GetActive(userId);
            return View(todos);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid userId = await Task.Run(LoggedInUser);
                var item = new DZ3.Classes.TodoItem(model.Text, userId);
                _repository.Add(item);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Completed()
        {
            Guid userId = await Task.Run(LoggedInUser);
            var completedTodos = _repository.GetCompleted(userId);
            return View(completedTodos);
        }

        public async Task<IActionResult> MarkCompleted(Guid id)
        {
            Guid userId = await Task.Run(LoggedInUser);
            var markedTodos = _repository.MarkAsCompleted(id,userId);
            return RedirectToAction("Index");
        }
    }
}