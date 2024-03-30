using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica.DTO;
using prueba_tecnica.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace prueba_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspiranteController : ControllerBase
    {
        private readonly DBContext DBContext;

        public AspiranteController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        //Se crean los métodos del CRUD

        [HttpGet("GetAspirantes")]
        public async Task<ActionResult<List<AspiranteDTO>>> Get()
        {
            var List = await DBContext.Aspirantes.Select(
                s => new AspiranteDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    Celular = s.Celular,
                    Correo = s.Correo
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetAspiranteById")]
        public async Task<ActionResult<AspiranteDTO>> GetAspiranteById(String Id)
        {
            AspiranteDTO Aspirante = await DBContext.Aspirantes.Select(
                    s => new AspiranteDTO
                    {
                        Id = s.Id,
                        Nombre = s.Nombre,
                        Apellido = s.Apellido,
                        Celular = s.Celular,
                        Correo = s.Correo
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (Aspirante == null)
            {
                return NotFound();
            }
            else
            {
                return Aspirante;
            }
        }

        [HttpPost("InsertAspirante")]
        public async Task<HttpStatusCode> InsertUser(AspiranteDTO Aspirante)
        {
            var entity = new Aspirante()
            {
                Id = Aspirante.Id,
                Nombre = Aspirante.Nombre,
                Apellido = Aspirante.Apellido,
                Celular = Aspirante.Celular,
                Correo = Aspirante.Correo,
            };

            DBContext.Aspirantes.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateAspirante")]
        public async Task<HttpStatusCode> UpdateAspirante(AspiranteDTO Aspirante)
        {
            var entity = await DBContext.Aspirantes.FirstOrDefaultAsync(s => s.Id == Aspirante.Id);

            entity.Id = Aspirante.Id;
            entity.Nombre = Aspirante.Nombre;
            entity.Apellido = Aspirante.Apellido;
            entity.Celular = Aspirante.Celular;
            entity.Correo = Aspirante.Correo;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteAspirante/{Id}")]
        public async Task<HttpStatusCode> DeleteAspirante(String Id)
        {
            var entity = new Aspirante()
            {
                Id = Id
            };
            DBContext.Aspirantes.Attach(entity);
            DBContext.Aspirantes.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
