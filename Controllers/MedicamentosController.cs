using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Models;
using ProyectoAPI.Data;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Medicamentos);
        }

        [Authorize(Roles = "Doctor,Enfermero")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        private IActionResult View(Medicamento med)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post(Medicamento nuevo)
        {
            _context.Medicamentos.Add(nuevo);
            _context.SaveChanges(); 
            return Ok("Medicamento agregado");
        }

        public async Task<IActionResult> Create(Medicamento med)
        {
            if (ModelState.IsValid)
            {
                // guardar en BD
                return RedirectToAction("Index");
            }

            return View(med);
        }


        [Authorize(Roles = "Doctor")]
        public IActionResult Edit(int id)
        {
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
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

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var med = await _context.Medicamentos.FindAsync(id);

            if (med != null)
            {
                _context.Medicamentos.Remove(med);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}