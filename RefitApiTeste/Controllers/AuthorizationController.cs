using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text.Json.Serialization;

namespace RefitApiTeste.Controllers
{
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromBody] AutenticarUsuarioRequest request)
        {
            var handler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(
                new GenericIdentity(request.Usuario, "TokenAuth"),
                [
                    new Claim("Id", request.Id.ToString())
                ]);

            var key = new RSACryptoServiceProvider(2048);
            key.ExportParameters(true);            

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(
                    new RsaSecurityKey(key), 
                    SecurityAlgorithms.RsaSha256Signature),
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                NotBefore = DateTime.UtcNow
            });

            var token = handler.WriteToken(securityToken);

            return new JsonResult(token);
        }
    }

    public class AutenticarUsuarioRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
