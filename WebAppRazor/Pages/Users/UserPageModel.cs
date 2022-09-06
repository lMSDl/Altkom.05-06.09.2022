using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users
{
    public abstract class UserPageModel : PageModel
    {
        public ICrudService<User> Service { get; }

        public UserPageModel(ICrudService<User> service)
        {
            Service = service;
        }
    }
}
