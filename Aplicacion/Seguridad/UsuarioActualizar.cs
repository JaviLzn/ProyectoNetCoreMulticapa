using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioActualizar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }

        }

        public class ValidarEjecuta : AbstractValidator<Ejecuta>
        {
            public ValidarEjecuta()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class ManejarEjecuta : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CursosOnlineContext context;
            private readonly UserManager<Usuario> userManager;
            private readonly IJwtGenerador jwtGenerador;
            private readonly IPasswordHasher<Usuario> passwordHasher;

            public ManejarEjecuta(CursosOnlineContext context,
                                  UserManager<Usuario> userManager,
                                  IJwtGenerador jwtGenerador,
                                  IPasswordHasher<Usuario> passwordHasher)
            {
                this.context = context;
                this.userManager = userManager;
                this.jwtGenerador = jwtGenerador;
                this.passwordHasher = passwordHasher;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await userManager.FindByNameAsync(request.UserName);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                                                 new { mensaje = "No se encuentra el usuario con este UserName" });
                }

                var correoUsado = await context.Users.Where(x => x.Email == request.Email && x.UserName == usuario.UserName).AnyAsync();
                if (correoUsado)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.InternalServerError,
                                                 new { mensaje = "No este email ya está usado por otro usuario." });
                }

                usuario.NombreCompleto = $"{request.Nombre} {request.Apellidos}";
                usuario.PasswordHash = passwordHasher.HashPassword(usuario, request.Password);
                usuario.Email = request.Email;

                var resultado = await userManager.UpdateAsync(usuario);

                var roles = await userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(roles);

                if (!resultado.Succeeded)
                {
                    throw new Exception("No se puedo actualizar el usuario");
                }
                
                return new UsuarioData
                {
                    NombreCompleto = usuario.NombreCompleto,
                    Token = jwtGenerador.CrearToken(usuario, listaRoles),
                    Email = usuario.Email,
                    UserName = usuario.UserName
                };
            }
        }
    }
}
