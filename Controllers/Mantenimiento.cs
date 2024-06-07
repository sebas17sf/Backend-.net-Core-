namespace WebApplication2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Model;

[ApiController]
[Route("api/mantenimiento")]
public class Mantenimiento : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public Mantenimiento(ApplicationDbContext context)
    {
        _context = context;
    }

    
    [HttpPost]
    public async Task<ActionResult<MantenimientoModel>> PostMantenimientoModel([FromBody] MantenimientoModel mantenimientoModel)
    {
        if (mantenimientoModel == null)
        {
            return BadRequest("MantenimientoModel is null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.MantenimientoModels.Add(mantenimientoModel);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Mantenimiento agregado", mantenimientoModel });
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MantenimientoModel>>> GetMantenimientoModel()
    {
        var mantenimientos = await _context.MantenimientoModels.ToListAsync();
        return Ok(mantenimientos);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<MantenimientoModel>> DeleteMantenimientoModel(int id)
    {
        var mantenimientoModel = await _context.MantenimientoModels.FindAsync(id);
        if (mantenimientoModel == null)
        {
            return NotFound();
        }

        _context.MantenimientoModels.Remove(mantenimientoModel);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Mantenimiento eliminado", mantenimientoModel });
    }
    
   [HttpPut("{id}")]
public async Task<ActionResult<MantenimientoModel>> PutMantenimientoModel(int id, [FromBody] MantenimientoModel mantenimientoModel)
{
    if (mantenimientoModel == null)
    {
        return BadRequest("MantenimientoModel is null.");
    }

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var mantenimiento = await _context.MantenimientoModels.FindAsync(id);
    if (mantenimiento == null)
    {
        return NotFound();
    }

    
    _context.Entry(mantenimiento).CurrentValues.SetValues(mantenimientoModel);

    await _context.SaveChangesAsync();
    
    return Ok(new { message = "Mantenimiento actualizado", mantenimiento });
}
    
}