using Base.Domain.DTOs;
using Base.Domain.DTOs.Usuario;
using Base.Domain.DTOS.Usuario;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using Base.Repository.Helpers.Security;
using Base.Repository.Repositorios.Usuario;
using Base.Service.Contracts;
using Base.Service.Contracts.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Base.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class FilmeXNotaController : BaseController
    {
        private readonly IFilmeXNotaService _rep;       

        public FilmeXNotaController(IFilmeXNotaService repositorioLogin)
        {
            _rep = repositorioLogin;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FilmeXNotaInsertDTO filmeXNota)
        {
            try
            {
                var retorno = (Retorno)await _rep.Cadastrar(filmeXNota);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Cadstrar o usuário", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] FilmeXNotaUpdateDTO filmeXNota)
        {
            try
            {
                var retorno = (Retorno)await _rep.Atualizar(filmeXNota);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Atualizar o usuário", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] FilmeXNotaDTO filmeXNota)
        {
            try
            {
                var retorno = (Retorno)await _rep.Excluir(filmeXNota);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Excluir o usuário", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

    }
}
