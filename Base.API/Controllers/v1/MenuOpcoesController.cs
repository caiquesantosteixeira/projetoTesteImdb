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
    public class MenuOpcoesController : BaseController
    {

        [HttpGet("OpcoesEmpresa")]
        public async Task<IActionResult> OpcoesEmpresaAsync([FromServices] IMenuOpcoes repository)
        {
            try
            {
                var dados = await repository.OpcoesEmpresa();
                return Ok(new Retorno (true, "Dados OpcoesEmpresa", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao listar o OpcoesEmpresaAsync", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IMenuOpcoes repository)
        {
            try
            {
                var dados = await repository.GetAll();
                return Ok(new Retorno(true, "Dados OpcoesEmpresa", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao listar o OpcoesEmpresa", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("paginacao")]
        public async Task<IActionResult> GetPage([FromServices] IMenuOpcoes repository, int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null)
        {
            try
            {
                // Preparacao da query

                var dados = await repository.DadosPaginado(QtdPorPagina, PagAtual, Filtro, ValueFiltro);
                return Ok(new Retorno (true, "Dados Paginado", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao listar os DadosPaginado OpcoesEmpresaAsync", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] MenuOpcoesCommand command, [FromServices] MenuOpcoesHandler handler)
        {
            try
            {
                var retorno = (Retorno) await handler.Handle(command, EMenuOpcoes.ATUALIZAR);
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
                GerarLog("Erro ao Atualizar o MenuOpcoes", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MenuOpcoesCommand command, [FromServices] MenuOpcoesHandler handler)
        {
            try
            {
                var retorno = (Retorno) await handler.Handle(command, EMenuOpcoes.CADASTRAR);
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
                GerarLog("Erro ao Cadastrar o MenuOpcoes", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] IMenuOpcoes repository)
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
                GerarLog("Erro ao Excluir o OpcoesEmpresaAsync", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
