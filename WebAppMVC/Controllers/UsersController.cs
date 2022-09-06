using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebAppMVC.Controllers
{
    [AutoValidateAntiforgeryToken]
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

        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string phrase)
        {
           var users = await _service.ReadAsync();

            if(!string.IsNullOrEmpty(phrase))
            {
                var properties = typeof(User).GetProperties().Where(x => x.CanRead).ToList();
                users = users.Where(x => properties.Any(xx => xx.GetValue(x)?.ToString()?.Contains(phrase) ?? false)).ToList();
            }

            return View(nameof(Index), users);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _service.ReadAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
