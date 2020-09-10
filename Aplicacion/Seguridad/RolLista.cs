using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class RolLista
    {
        public class Ejecuta : IRequest<List<IdentityRole>> { }

        public class ManejarEjecuta : IRequestHandler<Ejecuta, List<IdentityRole>>
        {
            private readonly RoleManager<IdentityRole> roleManager;

            public ManejarEjecuta(RoleManager<IdentityRole> roleManager)
            {
                this.roleManager = roleManager;
            }

            public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                return await roleManager.Roles.ToListAsync();
            }
        }
    }
}