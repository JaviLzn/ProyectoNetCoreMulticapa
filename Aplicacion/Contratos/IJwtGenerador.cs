using Dominio;
using System.Collections.Generic;

namespace Aplicacion.Contratos
{
    public interface IJwtGenerador
    {
         string CrearToken(Usuario usuario, List<string> rolesUsuario);
    }
}