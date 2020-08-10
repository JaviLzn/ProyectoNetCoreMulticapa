using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class UsuarioRolAgregar
    {
        public class Ejecuta :IRequest{
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

        public class ManejarEjecuta : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            public ManejarEjecuta(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
            }

            public async  Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var rol = await roleManager.FindByNameAsync(request.RolNombre);
                if (rol == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encuentra el rol"});
                }

                var usuario = await userManager.FindByNameAsync(request.UserName);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encuentra el usuario"});
                }

                var resultado = await userManager.AddToRoleAsync(usuario, rol.Name);

                if (resultado.Succeeded)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo agregar el rol al usuario");
            }
        }
    }
}