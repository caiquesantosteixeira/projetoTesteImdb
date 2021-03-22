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
    public class PermissoesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IPermissoes repository)
        {
            try
            {
                var dados = await repository.GetAll();
                return Ok(new Retorno (true, "Dados GetAll", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao listar as Permissoes", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("getpermenuopcoes/{idmenuopc}")]
        public async Task<IActionResult> GetPerMenuOpcoesAsync(int idmenuopc, [FromServices] IPermissoes repository)
        {
            try
            {
                var dados = await repository.GetPerMenuOpcoes(idmenuopc);
                return Ok(new Retorno (true, "Dados GetPerMenuOpcoes", dados));
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao listar as Permissoes", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
