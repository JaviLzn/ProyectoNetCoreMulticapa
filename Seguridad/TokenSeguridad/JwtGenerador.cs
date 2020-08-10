using System;
using System.Security.Claims;
using System.Collections.Generic;
using Aplicacion.Contratos;
using Dominio;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Seguridad
{
    public class JwtGenerador : IJwtGenerador
    {
        public string CrearToken(Usuario usuario, List<string> rolesUsuario)
        {
            // * Lista de Claims
            // La data del usuario que quiero compartir
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };
            if (rolesUsuario != null)
            {
                foreach (var rol in rolesUsuario)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescipcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = credenciales
            };

            var tokenManejador = new JwtSecurityTokenHandler();
            var token = tokenManejador.CreateToken(tokenDescipcion);

            return tokenManejador.WriteToken(token);
        }
    }
}