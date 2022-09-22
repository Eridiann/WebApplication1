using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ValuesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Guardar(Registros registro)
        {
            context.Add(registro);
            await context.SaveChangesAsync();
            return registro.Id;
        }

        [HttpGet]
        public async Task<List<Registros>> Traer()
        {
            List<Registros> registroList = new List<Registros>();
            registroList = await context.Registros.ToListAsync();
            return registroList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Registros>> TraerPorID(int id)
        {
            var registro = new Registros();
            registro = await context.Registros.FirstOrDefaultAsync(x => x.Id == id);
            if (registro == null) return NotFound();
            return registro;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var registro = context.Registros.FirstOrDefault(x => x.Id == id);
            if(registro != null)
            {
                context.Remove(registro);
                await context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Actualizar(int id)
        {
            var registroCheck = context.Registros.FirstOrDefault(x => x.Id == id);
            if(registroCheck == null)
            {
                return NotFound();
            }
            Registros registro = registroCheck;

            registro.FechaRegistro = DateTime.Now;
            context.Attach(registro).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
