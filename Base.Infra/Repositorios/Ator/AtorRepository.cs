﻿using Base.Domain.DTOs;
using Base.Domain.DTOS.Usuario;
using Base.Domain.Entidades;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Retornos;
using Base.Domain.Shared.Entidades.Usuario;
using Base.Repository.Contracts;
using Base.Repository.Helpers.Paginacao;
using Base.Rpepository.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Repository.Repositorios.Usuario
{
    public class AtorRepository: BaseRepository<Ator>, IAtor
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;
        private readonly IUserIdentity  _userIdentity;
        public AtorRepository(testeimdbContext context,ILog log, IUserIdentity userIdentity) : base(context,log,userIdentity)
        {
            _ctx = context;
            _log = log;
            _userIdentity = userIdentity;
        }
        public async Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null)
        {
            try
            {
                var lista = _ctx.Ator.Select(a => new AtorUpdateDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                });

                //filtro
                if (Filtro != null && ValueFiltro != null)
                {
                    Filtro = Filtro.Trim().ToUpper();

                    switch (Filtro)
                    {
                        case "CODIGO":
                            lista = lista.Where(a => a.Id.ToString() == ValueFiltro);
                            break;
                        case "NOME":
                            lista = lista.Where(a =>
                                                    a.Nome.Contains(ValueFiltro))
                                         .OrderBy(a => a.Nome);
                            break;
                    }
                }
                else
                {

                    lista = lista
                            .OrderBy(a => a.Id);
                }

                var dados = Paginacao.GetPage<Ator, AtorUpdateDTO>(_ctx.Ator, lista, QtdPorPagina, PagAtual);
                return new Retorno(true, "Dados Paginados", dados);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Paginar Usuarios", ex: ex);
                throw new Exception("Erro ao Paginar Usuarios", ex);
            }
        }
    }
}
