
using Control_Med.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProyectoAPI.Data; 
using ProyectoAPI.Models; 
using Microsoft.EntityFrameworkCore; 
public class ApplicationUser : IdentityUser
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicamentosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize(Roles = "Doctor,Enfermero")]
        public async Task<ActionResult<IEnumerable<Medicamento>>> GetMedicamentos()
        {
            return await _context.Medicamentos.ToListAsync();
        }
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<Medicamento>> PostMedicamento(Medicamento medicamento)
        {
            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();
            return Ok(medicamento);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            return NoContent();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> PutMedicamento(int id, Medicamento medicamento)
        {
            return NoContent();
        }
    }
}
namespace Control_Med.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser>_userManager;

        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
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