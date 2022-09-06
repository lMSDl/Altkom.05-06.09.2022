using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users
{
    public class IndexModel : UserPageModel
    {

        public IndexModel(ICrudService<User> service) : base(service)
        {
        }

        public void OnGet()
        {
        }


    }
}
