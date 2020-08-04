using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //  [Route("api/[controller]/[action]")]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CursosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> ListarCursos()
        {
            return await _mediator.Send(new Consulta.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> DetalleCursoId(int id)
        {
            return await _mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CrearCurso(Nuevo.Ejecuta datos)
        {
            return await _mediator.Send(datos);
        }

        [HttpPut("{id}")]
        public  async Task<ActionResult<Unit>> EditarCurso (int id, Editar.Ejecuta datos){
            datos.CursoId = id;
            return await _mediator.Send(datos);
        }
    }
}