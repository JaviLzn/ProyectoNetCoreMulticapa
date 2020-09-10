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
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> userManager;
            private readonly SignInManager<Usuario> signInManager;
            private readonly IJwtGenerador jwtGenerador;
            private readonly CursosOnlineContext context;
            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador, CursosOnlineContext context)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.jwtGenerador = jwtGenerador;
                this.context = context;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await userManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }
                var resultado = await signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                var rolesUsuario = await userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(rolesUsuario);

                var imagenPerfil = await context.Documento.Where(x => x.ObjetoReferencia == new Guid(usuario.Id)).FirstOrDefaultAsync();

                ImagenGeneral imagenCliente = null;
                if (imagenPerfil != null)
                {
                    imagenCliente = new ImagenGeneral
                    {
                        Data = Convert.ToBase64String(imagenPerfil.Contenido),
                        Extension = imagenPerfil.Extension,
                        Nombre = imagenPerfil.Nombre
                    };
                }

                if (resultado.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = jwtGenerador.CrearToken(usuario, listaRoles),
                        UserName = usuario.UserName,
                        Email = usuario.Email,
                        ImagenPerfil = imagenCliente
                    };
                }

                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);

            }
        }
    }
}