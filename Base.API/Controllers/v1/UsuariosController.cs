using Base.Domain.DTOs.Usuario;
using Base.Domain.DTOS.Usuario;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Retornos;
using Base.Repository.Helpers.Security;
using Base.Repository.Repositorios.Usuario;
using Base.Service.Contracts.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Base.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioService _repositorioLogin;       

        public UsuariosController(IUsuarioService repositorioLogin)
        {
            _repositorioLogin = repositorioLogin;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null)
        {
            try
            {
                var retorno = await _repositorioLogin.GetAll( QtdPorPagina,  PagAtual,  Filtro ,  ValueFiltro );
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao obter usuarios", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get( string id)
        {
            try
            {
                var retorno = await _repositorioLogin.Get(id);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao obter paginacao do usuário", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDTO user )
        {
            try
            {               
                var retorno = (Retorno)await _repositorioLogin.Persistir(user,ELogin.CADASTRAR);
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
        [Authorize]
        public async Task<IActionResult> Put([FromBody] UsuarioDTO user)
        {
            try
            {
                var retorno = (Retorno)await _repositorioLogin.Persistir(user, ELogin.ATUALIZAR);
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
        [Authorize]
        [ClaimsAuthorize("CADUSUARIOS", "EXCLUIR")]
        public async Task<IActionResult> Delete([FromBody] UsuarioDTO user)
        {
            try
            {
                var retorno = (Retorno)await _repositorioLogin.Persistir(user, ELogin.EXCLUIR);
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

        /// <summary>
        /// Metodo para Efetuar o Login do Usuário
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpPost("Logar")]
        public async Task<ActionResult> Login(LoginDTO loginUser)
        {
            try
            {               
                var retorno = await _repositorioLogin.Logar(loginUser);
                if (!retorno.Sucesso)
                {
                    return BadRequest(retorno);
                }
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao efetuar Login", ex:ex);
                return StatusCode(500);
            }
        }
    }
}
