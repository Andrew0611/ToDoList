using Microsoft.AspNetCore.Mvc;

namespace UserPage.Controllers
{
    public class TodolistController : Controller
    {
        public IActionResult Todolist()
        {
            return View();
        }
    }
}
