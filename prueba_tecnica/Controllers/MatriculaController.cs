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
    public class MatriculaController : ControllerBase
    {
        private readonly DBContext DBContext;
        public MatriculaController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        //Se crean los métodos del CRUD
        [HttpGet("GetMatriculas")]
        public async Task<ActionResult<List<MatriculaDTO>>> Get()
        {
            var List = await DBContext.Matriculas.Select(
                s => new MatriculaDTO
                {
                    Id = s.Id,
                    FechaIncripcin = s.FechaIncripcin,
                    LimitePago = s.LimitePago,
                    CostoMatricula = s.CostoMatricula,
                    Pago = s.Pago,
                    IdAspirante = s.IdAspirante,
                    IdPrograma = s.IdPrograma,
                    IdAspiranteNavigation = s.IdAspiranteNavigation,
                    IdProgramaNavigation = s.IdProgramaNavigation
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

        [HttpGet("GetMatriculaById")]
        public async Task<ActionResult<MatriculaDTO>> GetMatriculaById(int Id)
        {
            MatriculaDTO Matricula = await DBContext.Matriculas.Select(
                    s => new MatriculaDTO
                    {
                        Id = s.Id,
                        FechaIncripcin = s.FechaIncripcin,
                        LimitePago = s.LimitePago,
                        CostoMatricula = s.CostoMatricula,
                        Pago = s.Pago,
                        IdAspirante = s.IdAspirante,
                        IdPrograma = s.IdPrograma,
                        IdAspiranteNavigation = s.IdAspiranteNavigation,
                        IdProgramaNavigation = s.IdProgramaNavigation
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (Matricula == null)
            {
                return NotFound();
            }
            else
            {
                return Matricula;
            }
        }

        [HttpPost("InsertMatricula")]
        public async Task<HttpStatusCode> InsertUser(MatriculaDTO Matricula)
        {
            var entity = new Matricula()
            {
                Id = Matricula.Id,
                FechaIncripcin = Matricula.FechaIncripcin,
                LimitePago = Matricula.LimitePago,
                CostoMatricula = Matricula.CostoMatricula,
                Pago = Matricula.Pago,
                IdAspirante = Matricula.IdAspirante,
                IdPrograma = Matricula.IdPrograma,
                IdAspiranteNavigation = Matricula.IdAspiranteNavigation,
                IdProgramaNavigation = Matricula.IdProgramaNavigation,

            };

            DBContext.Matriculas.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateMatricula")]
        public async Task<HttpStatusCode> UpdateMatricula(MatriculaDTO Matricula)
        {
            var entity = await DBContext.Matriculas.FirstOrDefaultAsync(s => s.Id == Matricula.Id);

            entity.Id = Matricula.Id;
            entity.FechaIncripcin = Matricula.FechaIncripcin;
            entity.LimitePago = Matricula.LimitePago;
            entity.CostoMatricula = Matricula.CostoMatricula;
            entity.Pago = Matricula.Pago;
            entity.IdAspirante = Matricula.IdAspirante;
            entity.IdPrograma = Matricula.IdPrograma;
            entity.IdAspiranteNavigation = Matricula.IdAspiranteNavigation;
            entity.IdProgramaNavigation = Matricula.IdProgramaNavigation;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteMatricula/{Id}")]
        public async Task<HttpStatusCode> DeleteMatricula(int Id)
        {
            var entity = new Matricula()
            {
                Id = Id
            };
            DBContext.Matriculas.Attach(entity);
            DBContext.Matriculas.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
