using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebAppMVC.Controllers
{
    public class UsersController : Controller
    {
        private ICrudService<User> _service;

        public UsersController(ICrudService<User> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.ReadAsync());
        }
    }
}
