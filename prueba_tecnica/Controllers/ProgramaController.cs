using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica.DTO;
using prueba_tecnica.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Diagnostics;

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
        [HttpGet]
        public async Task<ActionResult<List<ProgramaDTO>>> Get()
        {
            System.Console.WriteLine("Ingreso get_porgrama");
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

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProgramaDTO>> GetProgramaById(String Id)
        {
            System.Console.WriteLine("Ingreso get_by_id_porgrama");
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

        [HttpPost]
        public async Task<HttpStatusCode> InsertProgram(ProgramaDTO Programa)
        {
            System.Console.WriteLine("Ingreso post_porgrama");
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

        [HttpPut]
        public async Task<HttpStatusCode> UpdatePrograma(ProgramaDTO Programa)
        {
            System.Console.WriteLine("Ingreso put_porgrama");
            var entity = await DBContext.Programas.FirstOrDefaultAsync(s => s.Id == Programa.Id);

            entity.Descripcin = Programa.Descripcin;
            entity.Nivel = Programa.Nivel;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("{Id}")]
        public async Task<HttpStatusCode> DeletePrograma(String Id)
        {
            System.Console.WriteLine("Ingreso delete_porgrama");
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
