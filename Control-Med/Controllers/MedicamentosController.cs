using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Models;
using ProyectoAPI.Data;

namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicamentosController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Doctor,Enfermero")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Medicamentos);
        }

        [HttpPost]
        public IActionResult Post(Medicamento nuevo)
        {
            _context.Medicamentos.Add(nuevo);
            _context.SaveChanges(); 
            return Ok("Medicamento agregado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var m = _context.Medicamentos.FirstOrDefault(x => x.Id == id);

            if (m == null)
                return NotFound("No encontrado");

            _context.Medicamentos.Remove(m);
            return Ok("Eliminado");
        }
    }
}