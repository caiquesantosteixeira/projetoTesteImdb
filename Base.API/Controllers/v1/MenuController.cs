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
    public class MenuController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IMenu repository)
        {
            try
            {
                var dados = await repository.GetAll();
                return Ok(new Retorno (true, "Dados GetAll", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao listar todos os dados do Menu", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] MenuCommand menuCommand, [FromServices] MenuHandler handler)
        {
            try
            {
                var retorno = (Retorno)await handler.Handle(menuCommand, EMenu.ATUALIZAR);
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
                GerarLog("Erro ao Atualizar o Menu", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MenuCommand menuCommand, [FromServices] MenuHandler handler)
        {
            try
            {
                var retorno = (Retorno) await handler.Handle(menuCommand, EMenu.CADASTRAR);
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
                GerarLog("Erro ao Cadastrar o Menu", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IMenu repository)
        {
            try
            {
                var dados = await repository.GetById(id);
                return Ok(new Retorno (true, "Dados by Id", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao listar os dados por id do Menu", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] IMenu repository)
        {
            try
            {
                var dados = await repository.Excluir(id);
                if(dados.Sucesso == false)
                {
                    return BadRequest(dados);
                }
                else
                {
                    return Ok(dados);
                }                
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Excluir o Menu", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
