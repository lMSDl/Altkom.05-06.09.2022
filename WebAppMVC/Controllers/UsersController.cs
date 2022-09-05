using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebAppMVC.Controllers
{
    public class UsersController : Controller
    {
        private ICrudService<User> _service;
        //private IEnumerable<User> _entities = new List<User> { new User() { Id = 1, Username = "a", Password = "b" } };

        public UsersController(ICrudService<User> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            //return View(_entities);
            return View(await _service.ReadAsync());
        }
    }
}
