
using Control_Med.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
internal class ApplicationUser : IdentityUser
{
}

namespace Control_Med.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser>? _userManager;

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager!.CreateAsync(usuario, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario, model.Rol);
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }

        private IActionResult View(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }
    }

}