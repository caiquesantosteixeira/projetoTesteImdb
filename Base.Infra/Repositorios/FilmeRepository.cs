using Base.Domain.DTOs;
using Base.Domain.Entidades;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Retornos;
using Base.Repository.Contracts;
using Base.Repository.Helpers.Paginacao;
using Base.Rpepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Repository.Repositorios.Usuario
{
    public class FilmeRepository:IFilme
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;

        public FilmeRepository(testeimdbContext context, ILog log)
        {
            _ctx = context;
            _log = log;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.Filme.Select(a => new FilmeDTO
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
                var lista = _ctx.Filme.Select(a => new FilmeDTO
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

                var dados = Paginacao.GetPage<Filme, FilmeDTO>(_ctx.Filme, lista, QtdPorPagina, PagAtual);
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

                var filme = await _ctx.Filme.Select(a => new FilmeDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

                return new Retorno(true, "Filme", filme);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar Usuario", ex: ex);
                throw new Exception("Erro ao Consultar Usuario", ex);
            }
        }

        public async Task<Retorno> Cadastrar(Filme filme)
        {
            try
            {
                _ctx.Filme.Add(filme);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "Filme cadastrado com sucesso.", "Filme cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Filme", ex: ex);
                throw new Exception("Erro ao Cadastrar o Filme", ex);
            }
        }

        public async Task<Retorno> Atualizar(Filme filme)
        {
            try
            {
                var exist = await _ctx.Filme.Select(a => new FilmeDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == filme.Id);


                if (exist == null)
                    return new Retorno(false, "Filme Não existe", "Filme Não existe");

                _ctx.Filme.Update(filme);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "Filme atualizado com sucesso.", "Filme cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Filme", ex: ex);
                throw new Exception("Erro ao Cadastrar o Filme", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var filme = await _ctx.Filme.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (filme == null)
                {
                    return new Retorno(false, "Não Autorizado", "Filme não encontrado.");
                }


                _ctx.Filme.Remove(filme);
                _ctx.SaveChanges();
                return new Retorno(true, "Filme Excluido.", "Filme Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir Filme", ex: ex);
                throw new Exception("Erro ao Excluir Filme", ex);
                throw new Exception("Erro ao Excluir Filme", ex);
            }
        }
    }
}
