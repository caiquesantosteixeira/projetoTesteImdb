﻿using Base.Domain.DTOs;
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
    public class GeneroController : BaseController
    {
        private readonly IGeneroService _rep;

        public GeneroController(IGeneroService repositorioLogin)
        {
            _rep = repositorioLogin;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var retorno = await _rep.GetAll();
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao obter genero", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
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
                GerarLog("Erro ao obter paginacao do genero", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GeneroInsertDTO genero )
        {
            try
            {
                var retorno = (Retorno)await _rep.Cadastrar(genero);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Cadstrar o genero", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] GeneroUpdateDTO genero)
        {
            try
            {
                var retorno = (Retorno)await _rep.Atualizar(genero);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Atualizar o genero", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] GeneroDTO genero)
        {
            try
            {
                var retorno = (Retorno)await _rep.Excluir(genero);
                if (retorno.Sucesso == false)
                    return BadRequest(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                GerarLog("Erro ao Excluir o genero", ex: ex);
                return StatusCode(500, ex.ToString());
            }
        }

    }
}
