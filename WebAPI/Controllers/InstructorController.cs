
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Instructores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Instructor;

namespace WebAPI.Controllers
{
    public class InstructorController: MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> ListaInstructores () {
            return await Mediator.Send(new Consulta.Peticion());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CrearInstructor(Nuevo.CrearInstructor datos){
            return await Mediator.Send(datos);
        }
    }
}