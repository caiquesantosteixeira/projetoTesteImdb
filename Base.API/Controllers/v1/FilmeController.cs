
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
    public class FilmeController : BaseController
    {
        private readonly IFilmeService _rep;

        public FilmeController(IFilmeService repositorioLogin)
        {
            _rep = repositorioLogin;
        }

        //tipoOrdenacao => NOTA ou ALFABETICA
        //Filtro => DIRETOR ou NOME ou GENERO ou ATORES
        [HttpGet]
        public async Task<IActionResult> GetAll(int QtdPorPagina, int PagAtual, string TipoOrdenação, string Filtro = null, string ValueFiltro = null)
        {
            try
            {

                var retorno = await _rep.GetAllPaginado( QtdPorPagina,  PagAtual,  TipoOrdenação,  Filtro,  ValueFiltro );
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao obter filme", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var retorno = await _rep.Get(id);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao obter paginacao do filme", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FilmeDTO filme)
        {
            try
            {
                var retorno = (Retorno)await _rep.Persistir(filme, ELogin.CADASTRAR);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Cadstrar o filme", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] FilmeDTO filme)
        {
            try
            {
                var retorno = (Retorno)await _rep.Persistir(filme, ELogin.ATUALIZAR);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Atualizar o filme", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] FilmeDTO filme)
        {
            try
            {
                var retorno = (Retorno)await _rep.Persistir(filme, ELogin.EXCLUIR);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Excluir o filme", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

    }
}
