using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string NombreCompleto { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class EjecutaValidador : AbstractValidator<Ejecuta>{

            public EjecutaValidador()
            {
                RuleFor(x => x.NombreCompleto).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CursosOnlineContext context;
            private readonly UserManager<Usuario> userManager;
            private readonly IJwtGenerador jwtGenerador;

            public Manejador(CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
            {
                this.context = context;
                this.userManager = userManager;
                this.jwtGenerador = jwtGenerador;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existeEmail = await context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existeEmail)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El Email se encuentra en uso."});
                };

                var existeUserName = await context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if (existeUserName)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El Nombre de Usuario ya se encuentra en uso."});
                };

                var usuario = new Usuario {
                    NombreCompleto = request.NombreCompleto,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var resultado = await userManager.CreateAsync(usuario, request.Password);

                if (resultado.Succeeded)
                {
                   return new UsuarioData {
                       NombreCompleto = usuario.NombreCompleto,
                       Token = jwtGenerador.CrearToken(usuario, null),
                       UserName = usuario.UserName,
                       Email = usuario.Email
                   };
                } else if(resultado.Errors.Count()  >0)
                {
                     throw new ManejadorExcepcion(HttpStatusCode.BadRequest, resultado.Errors);
                }

                throw new Exception("No se pudo agregar el nuevo usuario");
            }
        }


    }
}
