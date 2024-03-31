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
    public class ProgramaController : ControllerBase
    {
        private readonly DBContext DBContext;

        public ProgramaController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        //Se crean los métodos del CRUD
        [HttpGet("GetProgramas")]
        public async Task<ActionResult<List<ProgramaDTO>>> Get()
        {
            var List = await DBContext.Programas.Select(
                s => new ProgramaDTO
                {
                    Id = s.Id,
                    Descripcin = s.Descripcin,
                    Nivel = s.Nivel,
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

        [HttpGet("GetProgramaById")]
        public async Task<ActionResult<ProgramaDTO>> GetProgramaById(String Id)
        {
            ProgramaDTO Programa = await DBContext.Programas.Select(
                    s => new ProgramaDTO
                    {
                        Id = s.Id,
                        Descripcin = s.Descripcin,
                        Nivel = s.Nivel
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (Programa == null)
            {
                return NotFound();
            }
            else
            {
                return Programa;
            }
        }

        [HttpPost("InsertPrograma")]
        public async Task<HttpStatusCode> InsertUser(ProgramaDTO Programa)
        {
            var entity = new Programa()
            {
                Id = Programa.Id,
                Descripcin = Programa.Descripcin,
                Nivel = Programa.Nivel
            };

            DBContext.Programas.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdatePrograma")]
        public async Task<HttpStatusCode> UpdatePrograma(ProgramaDTO Programa)
        {
            var entity = await DBContext.Programas.FirstOrDefaultAsync(s => s.Id == Programa.Id);

            entity.Id = Programa.Id;
            entity.Descripcin = Programa.Descripcin;
            entity.Nivel = Programa.Nivel;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeletePrograma/{Id}")]
        public async Task<HttpStatusCode> DeletePrograma(String Id)
        {
            var entity = new Programa()
            {
                Id = Id
            };
            DBContext.Programas.Attach(entity);
            DBContext.Programas.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
