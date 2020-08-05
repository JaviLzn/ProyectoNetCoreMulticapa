using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //  [Route("api/[controller]/[action]")]
    public class CursosController : MiControllerBase
    {

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Curso>>> ListarCursos()
        {
            return await Mediator.Send(new Consulta.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> DetalleCursoId(int id)
        {
            return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CrearCurso(Nuevo.Ejecuta datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpPut("{id}")]
        public  async Task<ActionResult<Unit>> EditarCurso (int id, Editar.Ejecuta datos){
            datos.CursoId = id;
            return await Mediator.Send(datos);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarCurso (int id) {
            return await Mediator.Send(new Eliminar.Ejecuta{Id = id});
        }
    }
}