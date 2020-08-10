using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioRolEliminar
    {
        public class Ejecuta : IRequest
        {
            public string UserName { get; set; }
            public string RolNombre { get; set; }
        }

        public class ValidarEjecuta : AbstractValidator<Ejecuta>
        {
            public ValidarEjecuta()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.RolNombre).NotEmpty();
            }
        }

        public class ManejadorEjecuta : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            public ManejadorEjecuta(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var rol = await roleManager.FindByNameAsync(request.RolNombre);
                if (rol == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el rol" });
                }

                var usuario = await userManager.FindByNameAsync(request.UserName);

                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el usuario" });
                }

                var resultado = await userManager.RemoveFromRoleAsync(usuario, rol.Name);

                return resultado.Succeeded ? Unit.Value : throw new Exception("No se puedo eliminar el rol");
            }
        }
    }
}
