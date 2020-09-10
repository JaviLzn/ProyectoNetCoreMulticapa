using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class RolNuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
        }

        public class ValidarEjecutar : AbstractValidator<Ejecuta>
        {
            public ValidarEjecutar()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class ManejarEjecuta : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> roleManager;

            public ManejarEjecuta(RoleManager<IdentityRole> roleManager)
            {
                this.roleManager = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var rol = await roleManager.FindByNameAsync(request.Nombre);
                if (rol != null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Ya existe el rol" });
                }

                var resultado = await roleManager.CreateAsync(new IdentityRole(request.Nombre));
                if (resultado.Succeeded)
                {
                    return Unit.Value;
                }

                throw new Exception("No se puedo crear el rol");
            }
        }
    }
}