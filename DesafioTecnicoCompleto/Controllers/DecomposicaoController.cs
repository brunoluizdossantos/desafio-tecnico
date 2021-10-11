using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoWeb.Models;
using ProjetoWeb.Services;
using System;

namespace ProjetoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DecomposicaoController : ControllerBase
    {
        [HttpGet("{numero:int}")]
        public ActionResult<DecomposicaoNumero> GetDecomposicao([FromServices] IServico servico, int numero)
        {
            try
            {
                return servico.CalculaNumerosDivisoresEPrimos(numero);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter os dados");
            }
        }
    }
}
