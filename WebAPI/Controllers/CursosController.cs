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
using Persistencia.DapperConexion.Paginacion;

namespace WebAPI.Controllers
{
    public class CursosController : MiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<CursoDTO>>> ListarCursos()
        {
            return await Mediator.Send(new Consulta.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDTO>> DetalleCursoId(Guid id)
        {
            return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CrearCurso(Nuevo.Ejecuta datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpPut("{id}")]
        public  async Task<ActionResult<Unit>> EditarCurso (Guid id, Editar.Ejecuta datos){
            datos.CursoId = id;
            return await Mediator.Send(datos);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarCurso (Guid id) {
            return await Mediator.Send(new Eliminar.Ejecuta{Id = id});
        }

        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report (PaginacionCurso.Ejecuta data){
            return await Mediator.Send(data);
        }
    }
}