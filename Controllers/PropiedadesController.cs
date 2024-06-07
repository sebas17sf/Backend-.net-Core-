using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Controllers;
[ApiController]
[Route("api/propiedades")]
public class PropiedadesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PropiedadesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<PropiedadesModel>> PostPropiedadesModel(PropiedadesModel propiedadesModel)
    {
        if (propiedadesModel == null)
        {
            return BadRequest("PropiedadesModel is null.");
        }

        var propiedades = await _context.PropiedadesModels.FindAsync(propiedadesModel.id);
        if (propiedades != null)
        {
            return BadRequest("PropiedadesModel already exists.");
        }

        _context.PropiedadesModels.Add(propiedadesModel);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Propiedad agregada", propiedadesModel });
    }
    
    [HttpGet]
    public async Task<ActionResult> GetPropiedadesModel()
    {
        var propiedades = await _context.PropiedadesModels.ToListAsync();
        return Ok(propiedades);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePropiedadesModel(int id)
    {
        var propiedades = await _context.PropiedadesModels.FindAsync(id);
        if (propiedades == null)
        {
            return NotFound();
        }

        _context.PropiedadesModels.Remove(propiedades);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Propiedad eliminada", propiedades });
    }
   
    private bool PropiedadesModelExists(int id)
    {
        return _context.PropiedadesModels.Any(e => e.id == id);
    }
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult> PutPropiedadesModel(int id, PropiedadesModel propiedadesModel)
    {
        if (id != propiedadesModel.id)
        {
            return BadRequest();
        }

        _context.Entry(propiedadesModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PropiedadesModelExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(new { message = "Propiedad actualizada", propiedadesModel });
    }
}
