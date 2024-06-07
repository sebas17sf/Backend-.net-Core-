using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using WebApplication2.Data;
    using WebApplication2.Model;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize] 
        [HttpPost]
        public async Task<ActionResult<RolesModel>> PostRolesModel([FromBody] RolesModel rolesModel)
        {
            if (rolesModel == null)
            {
                return BadRequest("RolesModel is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RolesModels.Add(rolesModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Rol agregado", rolesModel });
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesModel>>> GetRolesModel()
        {
            var roles = await _context.RolesModels.ToListAsync();
            return Ok(roles);
        }
        
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<RolesModel>> DeleteRolesModel(int id)
        {
            var rolesModel = await _context.RolesModels.FindAsync(id);
            if (rolesModel == null)
            {
                return NotFound();
            }

            _context.RolesModels.Remove(rolesModel);
            await _context.SaveChangesAsync();

            return rolesModel;
        }
        
        private bool RolesModelExists(int id)
        {
            return _context.RolesModels.Any(e => e.id == id);
        }
        
       [HttpPut("{id}")]
public async Task<IActionResult> PutRolesModel(int id, RolesModel rolesModel)
{
    if (id != rolesModel.id)
    {
        return BadRequest();
    }

    var existingRole = await _context.RolesModels.FindAsync(id);
    if (existingRole == null)
    {
        return NotFound();
    }

    _context.Entry(existingRole).CurrentValues.SetValues(rolesModel);

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!RolesModelExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return Ok(new { message = "Rol actualizado", rolesModel });
}
        
        
    }
    
}