using System;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : MiControllerBase
    {
        // http://localhost:5000/api/Usuario/login
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login (Login.Ejecuta datos) {
            return await Mediator.Send(datos);
        }

        // http://localhost:5000/api/Usuario/registrar
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar (Registrar.Ejecuta datos) {
            return await Mediator.Send(datos);
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioData>> UsuarioActual () {
            return await Mediator.Send(new UsuarioActual.Ejecuta());
        }
        
    }
}