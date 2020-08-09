using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class RolController : MiControllerBase
    {
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> CrearRol(RolNuevo.Ejecuta data){
            return await Mediator.Send(data);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> EliminarRol (RolEliminar.Ejecuta data){
            return await Mediator.Send(data);
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<IdentityRole>>> ListaRoles(){
            return await Mediator.Send(new RolLista.Ejecuta());
        }
    }
}