using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using KanbanApp.Model;

namespace KanbanApp.Controllers
{
    [Route("[controller]/")]
    [Produces("[application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<AppUsuario> _userManager;
        private readonly SignInManager<AppUsuario> _signInManager;
        private readonly IConfiguration _config;

        public LoginController(UserManager<AppUsuario> userManager, SignInManager<AppUsuario> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._config = configuration;

            this._userManager.CreateAsync(new AppUsuario { UserName = _config["credentials:user"] }, _config["credentials:password"]);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioToken>> Post([FromBody] Usuario user)
        {
            //verifica login e senha
            var result = await _signInManager.PasswordSignInAsync(user.Login, user.Senha, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded) //login e senha ok
            {
                return BuildToken(user);
            }
            else
            {
                return Unauthorized(ModelState); //retorna status 401
            }
        }

        private UsuarioToken BuildToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);
            return new UsuarioToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expiration
            };
        }
    }
}