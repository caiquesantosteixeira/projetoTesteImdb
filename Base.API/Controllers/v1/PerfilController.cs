using Base.Domain.Commands.Usuario;
using Base.Domain.Commands.Usuario.Enums;
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
    public class PerfilController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IPerfil perfil)
        {
            try
            {
                var dados = await perfil.GetAll();
                return Ok(new Retorno (true, "Dados GetAll", dados));                            
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Listar os Perfis do Usuario", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] PerfilCommand perfil, [FromServices] PerfilHandler handler)
        {
            try
            {
                var retorno = (Retorno) await handler.Handle(perfil, EPerfil.ATUALIZAR);
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
                GerarLog("Erro ao Atualizar o Perfil", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PerfilCommand perfil, [FromServices] PerfilHandler handler)
        {
            try
            {
                var retorno = (Retorno)await handler.Handle(perfil, EPerfil.CADASTRAR);
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
                GerarLog("Erro ao Cadastrar o Perfil", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost("duplicar")]
        public async Task<IActionResult> DuplicarPerfilAsync([FromBody] PerfilCommand perfil, [FromServices] PerfilHandler handler)
        {
            try
            {
                var retorno = (Retorno)await handler.Handle(perfil, EPerfil.DUPLICARPERFIL);
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
                GerarLog("Erro ao Cadastrar o Perfil", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IPerfil perfil)
        {
            try
            {                
                var dados = await perfil.PerfilById(id);
                return Ok(new Retorno (true, "Dados by Id", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Obter o Perfil por Id", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, [FromServices] IPerfil perfil)
        {
            try
            {
                var retorno = await perfil.Excluir(id);
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
                GerarLog("Erro ao Excluir o Perfil", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
