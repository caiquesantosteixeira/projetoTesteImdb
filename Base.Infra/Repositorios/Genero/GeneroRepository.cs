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
    public class GeneroRepository : IGenero
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;

        public GeneroRepository(testeimdbContext context, ILog log)
        {
            _ctx = context;
            _log = log;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.Genero.Select(a => new GeneroDTO
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
                var lista = _ctx.Genero.Select(a => new GeneroDTO
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

                var dados = Paginacao.GetPage<Genero, GeneroDTO>(_ctx.Genero, lista, QtdPorPagina, PagAtual);
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

                var genero = await _ctx.Genero.Select(a => new GeneroDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

                return new Retorno(true, "Genero", genero);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar Usuario", ex: ex);
                throw new Exception("Erro ao Consultar Usuario", ex);
            }
        }

        public async Task<Retorno> Cadastrar(Genero genero)
        {
            try
            {
                _ctx.Genero.Add(genero);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "Genero cadastrado com sucesso.", "Genero cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Genero", ex: ex);
                throw new Exception("Erro ao Cadastrar o Genero", ex);
            }
        }

        public async Task<Retorno> Atualizar(Genero genero)
        {
            try
            {
                var exist = await _ctx.Genero.Select(a => new GeneroDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == genero.Id);

                if (exist == null)
                    return new Retorno(false, "Genero Não existe", "Genero Não existe");

                _ctx.Genero.Update(genero);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "Genero atualizado com sucesso.", "Genero cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Genero", ex: ex);
                throw new Exception("Erro ao Cadastrar o Genero", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var genero = await _ctx.Genero.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (genero == null)
                {
                    return new Retorno(false, "Não Autorizado", "Genero não encontrado.");
                }


                _ctx.Genero.Remove(genero);
                _ctx.SaveChanges();
                return new Retorno(true, "Genero Excluido.", "Genero Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir Genero", ex: ex);
                throw new Exception("Erro ao Excluir Genero", ex);
                throw new Exception("Erro ao Excluir Genero", ex);
            }
        }
    }
}
