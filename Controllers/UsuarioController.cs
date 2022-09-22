using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public UsuarioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Guardar(Usuario usuario)
        {
            context.Add(usuario);
            await context.SaveChangesAsync();
            return usuario.Id;
        }

        [HttpGet]
        public async Task<List<Usuario>> Traer()
        {
            List<Usuario> usuariosList = new List<Usuario>();
            usuariosList = await context.Usuarios.ToListAsync();
            return usuariosList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> TraerPorID(int id)
        {
            var usuario = new Usuario();
            usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null) return NotFound();
            return usuario;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.Id == id);
            if (usuario != null)
            {
                context.Remove(usuario);
                await context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Actualizar(int id, string name)
        {
            var usuarioCheck = context.Usuarios.FirstOrDefault(x => x.Id == id);
            if (usuarioCheck == null)
            {
                return NotFound();
            }
            Usuario usuario = usuarioCheck;

            if(name != null)
                usuario.Nombre = name;
            context.Attach(usuario).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }

    }
}
