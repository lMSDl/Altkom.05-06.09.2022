using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users
{
    public class AddOrEditModel : UserPageModel
    {
        public AddOrEditModel(ICrudService<User> service) : base(service)
        {
        }

        [BindProperty]
        public User SelectedUser { get; set; }

        public async Task OnGet(int id)
        {
            SelectedUser = await Service.ReadAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(SelectedUser.Id == 0)
            {
                await Service.CreateAsync(SelectedUser);
            }
            else
            {
                await Service.UpdateAsync(SelectedUser.Id, SelectedUser);
            }

            return RedirectToPage("../Index");
        }
    }
}
