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
    public class FilmeXGeneroRepository:IFilmeXGenero
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;

        public FilmeXGeneroRepository(testeimdbContext context, ILog log)
        {
            _ctx = context;
            _log = log;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.FilmeXgenero.Select(a => new FilmeXgenero
                {
                    Id = a.Id,
                    IdFilme = a.IdFilme,
                    IdGenero = a.IdGenero
                }).AsNoTracking().ToListAsync();
                return new Retorno(true, "", lista);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar FilmeXGenero", ex: ex);
                throw new Exception("Erro ao Consultar FilmeXGenero", ex);
            }
        }


        public async Task<Retorno> Cadastrar(FilmeXgenero filmeXGenero)
        {
            try
            {
                _ctx.FilmeXgenero.Add(filmeXGenero);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXGenero cadastrado com sucesso.", "FilmeXFilmeXGenero cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXFilmeXGenero", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXFilmeXGenero", ex);
            }
        }

        public async Task<Retorno> Atualizar(FilmeXgenero filmeXGenero)
        {
            try
            {
                var usuExist = await _ctx.FilmeXgenero.Select(a => new FilmeXGeneroDTO
                {
                    Id = a.Id
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == filmeXGenero.Id);

                if (usuExist == null)
                    return new Retorno(false, "FilmeXGenero Não existe", "FilmeXGenero Não existe");

                _ctx.FilmeXgenero.Update(filmeXGenero);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXGenero atualizado com sucesso.", "FilmeXGenero cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXGenero", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXGenero", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var FilmeXGenero = await _ctx.FilmeXgenero.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (FilmeXGenero == null)
                {
                    return new Retorno(false, "Não Autorizado", "FilmeXGenero não encontrado.");
                }


                _ctx.FilmeXgenero.Remove(FilmeXGenero);
                _ctx.SaveChanges();
                return new Retorno(true, "FilmeXGenero Excluido.", "FilmeXGenero Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir FilmeXGenero", ex: ex);
                throw new Exception("Erro ao Excluir FilmeXGenero", ex);
                throw new Exception("Erro ao Excluir FilmeXGenero", ex);
            }
        }
    }
}
