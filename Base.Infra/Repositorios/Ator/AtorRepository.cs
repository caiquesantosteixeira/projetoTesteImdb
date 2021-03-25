using Base.Domain.DTOs;
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
    public class AtorRepository:IAtor
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;
        private readonly IUserIdentity  _userIdentity;
        public AtorRepository(testeimdbContext context,ILog log, IUserIdentity userIdentity)
        {
            _ctx = context;
            _log = log;
            _userIdentity = userIdentity;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.Ator.Select(a => new AtorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().ToListAsync();
                return new Retorno(true, "Perfil Cadastrado com Sucesso.", lista);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar Login", ex: ex);
                throw new Exception("Erro ao Consultar Login", ex);
            }
        }

        public async Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null)
        {
            try
            {
                var lista = _ctx.Ator.Select(a => new AtorDTO
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

                var dados = Paginacao.GetPage<Ator, AtorDTO>(_ctx.Ator, lista, QtdPorPagina, PagAtual);
                return new Retorno(true, "Dados Paginados", dados);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Paginar Usuarios", ex: ex);
                throw new Exception("Erro ao Paginar Usuarios", ex);
            }
        }

        public async Task<Retorno> GetById(string id)
        {
            try
            {
                var ator = await _ctx.Ator.Select(a => new AtorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

                return new Retorno(true, "Ator", ator);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar Usuario", ex: ex);
                throw new Exception("Erro ao Consultar Usuario", ex);
            }
        }

        

        public async Task<Retorno> Cadastrar(Ator ator)
        {
            
            if (!_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Só Administradores podem cadastrar.", "Só Administradores podem cadastrar."); ;
            }

            try
            {
                _ctx.Ator.Add(ator);
                await _ctx.SaveChangesAsync();

                return  new Retorno(true, "Ator cadastrado com sucesso.", "Ator cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Ator", ex: ex);
                throw new Exception("Erro ao Cadastrar o Ator", ex);
            }
        }

        public async Task<Retorno> Atualizar(Ator ator)
        {
            if (!_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Só Administradores podem cadastrar.", "Só Administradores podem cadastrar."); ;
            }

            try
            {
                var usuExist = await _ctx.Ator.Select(a => new AtorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == ator.Id);

                if (usuExist == null)
                    return new Retorno(false, "Ator Não existe", "Ator Não existe");

                _ctx.Ator.Update(ator);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "Ator atualizado com sucesso.", "Ator cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Ator", ex: ex);
                throw new Exception("Erro ao Cadastrar o Ator", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            if (!_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Só Administradores podem cadastrar.", "Só Administradores podem cadastrar."); ;
            }

            try
            {
                var ator = await _ctx.Ator.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (ator == null)
                {
                    return new Retorno(false, "Não Autorizado", "Ator não encontrado.");
                }


                _ctx.Ator.Remove(ator);
                _ctx.SaveChanges();
                return new Retorno(true, "Ator Excluido.", "Ator Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir Ator", ex: ex);
                throw new Exception("Erro ao Excluir Ator", ex);
                throw new Exception("Erro ao Excluir Ator", ex);
            }
        }
    }
}
