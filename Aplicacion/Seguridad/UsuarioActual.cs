using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {
        public class Ejecuta : IRequest<UsuarioData> { }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> userManager;
            private readonly IJwtGenerador jwtGenerador;
            private readonly IUsuarioSesion usuarioSesion;
            private readonly CursosOnlineContext context;
            public Manejador(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion, CursosOnlineContext context)
            {
                this.userManager = userManager;
                this.jwtGenerador = jwtGenerador;
                this.usuarioSesion = usuarioSesion;
                this.context = context;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await userManager.FindByNameAsync(usuarioSesion.ObtenerUsuarioSesion());

                var rolesUsuario = await userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(rolesUsuario);

                                var imagenPerfil = await context.Documento.Where(x => x.ObjetoReferencia == new Guid(usuario.Id)).FirstOrDefaultAsync();
                ImagenGeneral imagenGeneral = null;

                if (imagenPerfil != null)
                {
                    imagenGeneral = new ImagenGeneral
                    {
                        Data = Convert.ToBase64String(imagenPerfil.Contenido),
                        Nombre = imagenPerfil.Nombre,
                        Extension = imagenPerfil.Extension
                    };
                }

                return new UsuarioData
                {
                    NombreCompleto = usuario.NombreCompleto,
                    UserName = usuario.UserName,
                    Token = jwtGenerador.CrearToken(usuario, listaRoles),
                    Email = usuario.Email,
                    ImagenPerfil = imagenGeneral
                };
            }
        }
    }
}