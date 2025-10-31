using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("datahora")]
        public IActionResult ObterDataHoraAtual()
        {
            var dataHoraAtual = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            return Ok(new { DataHoraAtual = dataHoraAtual });
        }

        [HttpGet("Apresentar/{nome}")]
        public IActionResult ApresentarNome(string nome)
        {
            var mensagem = $"Olá, {nome}! Seja bem-vindo(a)!";
            return Ok(new { Mensagem = mensagem });
        }

    }
}