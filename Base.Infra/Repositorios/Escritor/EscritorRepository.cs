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
    public class EscritorRepository:IEscritor
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;

        public EscritorRepository(testeimdbContext context,ILog log)
        {
            _ctx = context;
            _log = log;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.Escritor.Select(a => new EscritorDTO
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
                var lista = _ctx.Escritor.Select(a => new EscritorDTO
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

                var dados = Paginacao.GetPage<Escritor, EscritorDTO>(_ctx.Escritor, lista, QtdPorPagina, PagAtual);
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

                var escritor = await _ctx.Escritor.Select(a => new EscritorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

                return new Retorno(true, "Escritor", escritor);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar Usuario", ex: ex);
                throw new Exception("Erro ao Consultar Usuario", ex);
            }
        }

        public async Task<Retorno> Cadastrar(Escritor escritor)
        {
            try
            {
                _ctx.Escritor.Add(escritor);
                await _ctx.SaveChangesAsync();

                return  new Retorno(true, "Escritor cadastrado com sucesso.", "Escritor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Escritor", ex: ex);
                throw new Exception("Erro ao Cadastrar o Escritor", ex);
            }
        }

        public async Task<Retorno> Atualizar(Escritor escritor)
        {
            try
            {
                var usuExist = await _ctx.Escritor.Select(a => new EscritorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == escritor.Id);


                if (usuExist == null)
                    return new Retorno(false, "Escritor Não existe", "Escritor Não existe");

                _ctx.Escritor.Update(escritor);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "Escritor atualizado com sucesso.", "Escritor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Escritor", ex: ex);
                throw new Exception("Erro ao Cadastrar o Escritor", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var escritor = await _ctx.Escritor.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (escritor == null)
                {
                    return new Retorno(false, "Não Autorizado", "Escritor não encontrado.");
                }


                _ctx.Escritor.Remove(escritor);
                _ctx.SaveChanges();
                return new Retorno(true, "Escritor Excluido.", "Escritor Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir Escritor", ex: ex);
                throw new Exception("Erro ao Excluir Escritor", ex);
                throw new Exception("Erro ao Excluir Escritor", ex);
            }
        }
    }
}
