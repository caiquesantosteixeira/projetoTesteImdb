using Base.Domain.Commands.Menu;
using Base.Domain.Commands.Menu.Enums;
using Base.Domain.Handler.Menu;
using Base.Domain.Repositorios.Menu;
using Base.Domain.Retornos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Base.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuOpcoesBotoesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IMenuOpcoesBotoes repository)
        {
            try
            {
                var dados = await repository.GetAll();
                return Ok(new Retorno (true, "Dados MenuOpcoesBotoes", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao listar o MenuOpcoesBotoes", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromServices] IMenuOpcoesBotoes repository, int id)
        {
            try
            {
                var dados = await repository.GetPermissoesMenu(id);
                return Ok(new Retorno (true, "Dados MenuOpcoesBotoes", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao listar o Get MenuOpcoesBotoes", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MenuOpcoesBotoesCommand command, [FromServices] MenuOpcoesBotoesHandler handler)
        {
            try
            {
                var retorno = (Retorno)await handler.Handle(command, EMenuOpcoesBotoes.CADASTRAR);
                if (!retorno.Sucesso)
                {
                    return BadRequest(retorno);
                }
                else
                {
                    return Ok(retorno);
                }
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Cadastrar o MenuOpcoesBotoes", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] IMenuOpcoesBotoes repository)
        {
            try
            {
                var retorno = await repository.Excluir(id);
                if (!retorno.Sucesso)
                {
                    return BadRequest(retorno);
                }
                else
                {
                    return Ok(retorno);
                }
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Excluir o MenuOpcoesBotoes", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
