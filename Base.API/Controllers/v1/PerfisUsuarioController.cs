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
    public class PerfisUsuarioController : BaseController
    {     
        

        [HttpGet("paginacao")]
        public async Task<IActionResult> GetPage([FromServices] IPerfilUsuario perfil, int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null, int id = 0)
        {
            try
            {
                var retorno = await perfil.DadosPaginado(QtdPorPagina, PagAtual, Filtro, ValueFiltro, id);
                return Ok(new Retorno (true, "Dados GetAll", retorno));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Obter Paginacao", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("permissoesperfil/{IdPerfil}")]
        public async Task<IActionResult> PermissoesPerfilAsync(int IdPerfil, [FromServices] IPerfilUsuario perfil)
        {
            try
            {
                var retorno = await perfil.PermissaoById(IdPerfil);
                return Ok(new Retorno (true, "Dados GetAll", retorno));               
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Obter as Permissões do Perfil.", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PerfilUsuarioCommand command, [FromServices] PerfilUsuarioHandler handler)
        {
            try
            {
                var retorno =  (Retorno) await handler.Handle(command, EPerfilUsuario.ADICIONAR);
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
                GerarLog("Erro ao Cadastrar o Perfil do Usuario", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] IPerfilUsuario perfil)
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
                GerarLog("Erro ao Excluir o Perfil do Usuario", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

    }
}
