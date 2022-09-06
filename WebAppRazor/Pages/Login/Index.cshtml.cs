using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebAppRazor.Pages.Login
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ReturnUrl { get; set; }


        public async Task<IActionResult> OnPost()
        {
            if(Username != "admin" || Password != "nimda")
            {
                ModelState.AddModelError("Credentials", "Invalid credentials");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Role, "Delete"),
                new Claim(ClaimTypes.Role, "Edit")
            };


            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!Url.IsLocalUrl(ReturnUrl))
                ReturnUrl = Url.Content("/");
            return Redirect(ReturnUrl);
        }
    }
}
