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
        [HttpPost("[action]")]
        public async Task<ActionResult<Unit>> Crear(RolNuevo.Ejecuta data)
        {
            return await Mediator.Send(data);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<Unit>> Eliminar(RolEliminar.Ejecuta data)
        {
            return await Mediator.Send(data);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<IdentityRole>>> Lista()
        {
            return await Mediator.Send(new RolLista.Ejecuta());
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Unit>> AgregarRolUsuario(UsuarioRolAgregar.Ejecuta data)
        {
            return await Mediator.Send(data);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Unit>> EliminarRolUsuario(UsuarioRolEliminar.Ejecuta data)
        {
            return await Mediator.Send(data);
        }

        [HttpGet("[action]/{username}")]
        public async Task<ActionResult<List<string>>> ObtenerRolesUsuario(string username)
        {
            return await Mediator.Send(new ObtenerRolesUsuario.Ejecuta { UserName = username });
        }
    }
}