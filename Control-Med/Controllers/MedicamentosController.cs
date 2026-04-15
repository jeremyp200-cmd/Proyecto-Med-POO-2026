using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Models;
using ProyectoAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace ProyectoAPI.Controllers
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
        public async Task<IActionResult> Get()
        {
            var lista = await _context.Medicamentos.ToListAsync();
            return Ok(lista);
        }
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Post([FromBody] Medicamento nuevo)
        {
            _context.Medicamentos.Add(nuevo);
            await _context.SaveChangesAsync(); 
            return Ok(new { mensaje = "Medicamento agregado", dato = nuevo });
        }

       
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Put(int id, [FromBody] Medicamento actualizado)
        {
            var med = await _context.Medicamentos.FindAsync(id);
            if (med == null) return NotFound("No encontrado");
            med.Nombre = actualizado.Nombre;
            med.Cantidad = actualizado.Cantidad;
            await _context.SaveChangesAsync();
            return Ok("Medicamento actualizado");
        }

    
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _context.Medicamentos.FirstOrDefaultAsync(x => x.Id == id);
            await _context.SaveChangesAsync();
            return Ok("Eliminado correctamente");
        }
    }
}