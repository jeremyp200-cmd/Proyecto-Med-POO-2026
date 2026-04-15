
namespace Control_Med.Controllers
{
    public class RegisterController
    {
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(usuario, model.Password);

                if (result.Succeeded)
                {
                    // 🔥 AQUÍ agregas el rol
                    await _userManager.AddToRoleAsync(usuario, "Doctor");
                    // o "Enfermero"

                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }
    }
}