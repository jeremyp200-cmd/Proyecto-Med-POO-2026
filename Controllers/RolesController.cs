
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Controllers;

namespace Control_Med.Controllers
{
    public class RolesController: Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public RolesController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Register()
    {
        return View();
    }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
    public async Task<IActionResult> Register(string email, string password)
    {
        var usuario = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(usuario, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(usuario, "Doctor");

            return RedirectToAction("Login");
        }

        return View();
    }
}
}