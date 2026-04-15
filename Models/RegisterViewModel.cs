using Microsoft.AspNetCore.Mvc;

namespace Control_Med.Models
{
    public class RegisterViewModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Rol { get; set; }
        public IActionResult Register()
        {
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}