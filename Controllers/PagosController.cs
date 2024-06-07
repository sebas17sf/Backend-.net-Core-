namespace WebApplication2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Model;

[ApiController]
[Route("api/pagos")]
public class PagosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PagosController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    [HttpPost]
    public async Task<ActionResult<PagosModel>> PostPagosModel(PagosModel pagosModel)
    {
        if (pagosModel == null)
        {
            return BadRequest("PagosModel is null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.PagosModels.Add(pagosModel);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Pago agregado", pagosModel });
    }
   

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PagosModel>>> GetPagosModel()
    {
        var pagos = await _context.PagosModels.ToListAsync();
        return Ok(pagos);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<PagosModel>> DeletePagosModel(int id)
    {
        var pagosModel = await _context.PagosModels.FindAsync(id);
        if (pagosModel == null)
        {
            return NotFound();
        }

        _context.PagosModels.Remove(pagosModel);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Pago eliminado", pagosModel });
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<PagosModel>> PutPagosModel(int id, [FromBody] PagosModel pagosModel)
    {
        if (id != pagosModel.id)
        {
            return BadRequest();
        }

        _context.Entry(pagosModel).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Pago actualizado", pagosModel });
    }
    
    
   
}