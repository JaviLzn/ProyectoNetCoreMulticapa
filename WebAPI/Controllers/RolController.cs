using System.Threading.Tasks;
using Aplicacion.Seguridad;
using MediatR;
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
    }
}