using Base.Domain.Commands.Menu;
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
    public class PerfilUsuarioBotoesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IPerfilUsuarioBotoes repository)
        {
            try
            {
                var dados = await repository.GetAll();
                return Ok(new Retorno (true, "Dados GetAll", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Listar todos os botoes do perfil", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("getbtnperfil/{idPerfil}")]
        public async Task<IActionResult> GetBtnPerfil(int idPerfil, [FromServices] IPerfilUsuarioBotoes repository)
        {
            try
            {
                var retorno = await repository.GetAllPermissaoPerfil(idPerfil);
                return Ok(new Retorno (true, "Botoes do Perfil", retorno));

            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Listar todos os botoes do perfil", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PerfilUsuarioBotoesCommand command, [FromServices] PerfilUsuarioBotoesHandler handler)
        {
            try
            {
                var retorno = (Retorno)await handler.Handle(command, Domain.Commands.Menu.Enums.EPerfilUsuarioBotoes.CADASTRAR);
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
                GerarLog("Erro ao Cadastrar os botoes", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] IPerfilUsuarioBotoes repository)
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
                GerarLog("Erro ao Excluir os botoes do perfil", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        } 
    }
}
