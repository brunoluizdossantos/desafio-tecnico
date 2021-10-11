using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjetoWeb.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacaoController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AutorizacaoController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("cadastro")]
        public async Task<ActionResult> RegisterUser([FromBody] UsuarioDTO usuarioDto)
        {
            var user = new IdentityUser
            {
                UserName = usuarioDto.Email,
                Email = usuarioDto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false);

            return Ok(GeraToken(usuarioDto));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioDTO usuarioDto)
        {
            // Verificar as credenciais e retorna um valor
            var result = await _signInManager.PasswordSignInAsync(usuarioDto.Email, usuarioDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(GeraToken(usuarioDto));
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Login e senha inválidos");
                return BadRequest(ModelState);
            }
        }

        public UsuarioToken GeraToken(UsuarioDTO usuario)
        {
            // Define declarações do usuário
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Gera uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            // Gera a assinatura digital do token usando o algoritmo Hmac e a chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tempo de expiração do token
            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            // Classe que representa um token JWT e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais);

            // Retorna os dados com o token e informações
            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token gerado com sucesso"
            };
        }
    }
}
