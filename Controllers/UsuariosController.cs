using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsuariosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<UsuariosModel>> PostUsuariosModel(UsuariosModel usuariosModel)
    {
        if (usuariosModel == null)
        {
            return BadRequest("UsuariosModel is null.");
        }

        
        var roles = await _context.RolesModels.FindAsync(usuariosModel.role_id);
        if (roles == null)
        {
            return BadRequest("Invalid role_id.");
        }

        
        usuariosModel.role_id = roles.id;

        _context.UsuariosModels.Add(usuariosModel);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuario agregado", usuariosModel });
    }
    
    [HttpGet]
    public async Task<ActionResult> GetUsuariosModel()
    {
        var usuarios = await _context.UsuariosModels.ToListAsync();
        return Ok(usuarios);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUsuariosModel(int id)
    {
        var usuarios = await _context.UsuariosModels.FindAsync(id);
        if (usuarios == null)
        {
            return NotFound();
        }

        _context.UsuariosModels.Remove(usuarios);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuario eliminado" });
    }
    
    private async Task<bool> UsuariosModelExists(int id)
    {
        return await _context.UsuariosModels.AnyAsync(e => e.id == id);
    }

   
    [HttpPut("{id}")]
    public async Task<ActionResult> PutUsuariosModel(int id, UsuariosModel usuariosModel)
    {
        if (id != usuariosModel.id)
        {
            return BadRequest();
        }

        var usuarioExistente = await _context.UsuariosModels.FindAsync(id);
        if (usuarioExistente == null)
        {
            return NotFound("Usuario no encontrado.");
        }

        var roles = await _context.RolesModels.FindAsync(usuariosModel.role_id);
        if (roles == null)
        {
            return BadRequest("Invalid role_id.");
        }

        usuarioExistente.nombre = usuariosModel.nombre;
        usuarioExistente.email = usuariosModel.email;
        usuarioExistente.password = usuariosModel.password;
        usuarioExistente.role_id = roles.id;

        _context.Entry(usuarioExistente).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await UsuariosModelExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(new { message = "Usuario actualizado" });
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] UsuariosModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("grupo6Elmejor12345grupo6Elmejor12345");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] 
            {
                new Claim(ClaimTypes.Name, user.nombre.ToString())
               
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }

}