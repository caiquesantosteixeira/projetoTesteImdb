using Base.Domain.DTOs;
using Base.Domain.Entidades;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Retornos;
using Base.Repository.Contracts;
using Base.Repository.Helpers.Paginacao;
using Base.Rpepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string TipoOrdenação, string Filtro = null, string ValueFiltro = null)
        {
            try
            {
                var lista = _ctx.Filme.Select(a => new FilmeDTO
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    
                }).ToList();
                foreach (var item in lista) {
                    item.Atores = _ctx.Ator
                        .Join(_ctx.FilmeXator, ator => ator.Id, fa => fa.IdAtor, (ator, fa) => new {ator, fa })
                         .Select(x => new AtorDTO
                    {
                        Id = x.ator.Id ,
                        Nome = x.ator.Nome
                    }).ToList();

                    item.Diretores = _ctx.Diretor
                        .Join(_ctx.FilmeXdiretor, diretor => diretor.Id, fd => fd.Id, (diretor, fd) => new { diretor, fd })
                         .Select(x => new DiretorDTO
                         {
                        Id = x.diretor.Id,
                        Nome = x.diretor.Nome
                    }).ToList();

                    item.Generos = _ctx.Genero
                        .Join(_ctx.FilmeXgenero, genero => genero.Id, fg => fg.Id, (genero, fg) => new { genero, fg })
                         .Select(x => new GeneroDTO
                         {
                        Id = x.genero.Id,
                        Nome = x.genero.Nome
                    }).ToList();

                    var quantRegistros = _ctx.FilmeXnota.Where(b => b.IdFilme == item.Id).Count();
                    if (quantRegistros > 0)
                    {
                        item.MediaNota = (_ctx.FilmeXnota.Where(b => b.IdFilme == item.Id).Sum(a => a.Nota)) / quantRegistros;
                    }
                }

                var novaLista = new List<FilmeDTO>();
                //filtro
                if (Filtro != null && ValueFiltro != null)
                {
                    Filtro = Filtro.Trim().ToUpper();

                    switch (Filtro)
                    { 
                        case "DIRETOR":
                           
                            foreach (var filme in lista) {
                                foreach (var diretor in filme.Diretores) {
                                    if (diretor.Nome.Contains(ValueFiltro)){
                                        novaLista.Add(filme);
                                    }
                                }
                            }

                            
                            break;
                        case "NOME":
                            lista = lista.Where(a =>
                                                    a.Nome.Contains(ValueFiltro))
                                         .OrderBy(a => a.Nome).ToList();
                            break;
                        case "GENERO":
                            foreach (var filme in lista)
                            {
                                foreach (var genero in filme.Generos)
                                {
                                    if (genero.Nome.Contains(ValueFiltro))
                                    {
                                        novaLista.Add(filme);
                                    }
                                }
                            }
                            break;

                        case "ATORES":
                            foreach (var filme in lista)
                            {
                                foreach (var atores in filme.Atores)
                                {
                                    if (atores.Nome.Contains(ValueFiltro))
                                    {
                                        novaLista.Add(filme);
                                    }
                                }
                            }
                            break;
                    }
                    lista = novaLista;
                }
                else
                {
                    lista = lista.OrderBy(a => a.Id).ToList();
                }

                if (TipoOrdenação == "NOTA")
                {
                    lista.OrderBy(a => a.MediaNota);
                }
                else 
                {
                    lista.OrderBy(a => a.Nome);
                }

                var dados = Paginacao.GetPage<Filme, FilmeDTO>(_ctx.Filme, lista.AsQueryable(), QtdPorPagina, PagAtual);
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
