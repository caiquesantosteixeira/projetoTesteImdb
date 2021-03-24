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
    public class FilmeXAtorRepository:IFilmeXAtor
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;

        public FilmeXAtorRepository(testeimdbContext context, ILog log)
        {
            _ctx = context;
            _log = log;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.FilmeXator.Select(a => new FilmeXator
                {
                    Id = a.Id,
                    IdFilme = a.IdFilme,
                    IdAtor = a.IdAtor
                }).AsNoTracking().ToListAsync();
                return new Retorno(true, "", lista);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar FilmeXAtor", ex: ex);
                throw new Exception("Erro ao Consultar FilmeXAtor", ex);
            }
        }


        public async Task<Retorno> Cadastrar(FilmeXator filmeXator)
        {
            try
            {
                _ctx.FilmeXator.Add(filmeXator);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXAtor cadastrado com sucesso.", "FilmeXFilmeXAtor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXFilmeXAtor", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXFilmeXAtor", ex);
            }
        }

        public async Task<Retorno> Atualizar(FilmeXator filmeXFilmeXAtor)
        {
            try
            {
                var usuExist = await _ctx.FilmeXator.Select(a => new FilmeXatorDTO
                {
                    Id = a.Id
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == filmeXFilmeXAtor.Id);

                if (usuExist == null)
                    return new Retorno(false, "FilmeXAtor Não existe", "FilmeXAtor Não existe");

                _ctx.FilmeXator.Update(filmeXFilmeXAtor);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXAtor atualizado com sucesso.", "FilmeXAtor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXAtor", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXAtor", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var FilmeXAtor = await _ctx.FilmeXator.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (FilmeXAtor == null)
                {
                    return new Retorno(false, "Não Autorizado", "FilmeXAtor não encontrado.");
                }


                _ctx.FilmeXator.Remove(FilmeXAtor);
                _ctx.SaveChanges();
                return new Retorno(true, "FilmeXAtor Excluido.", "FilmeXAtor Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir FilmeXAtor", ex: ex);
                throw new Exception("Erro ao Excluir FilmeXAtor", ex);
                throw new Exception("Erro ao Excluir FilmeXAtor", ex);
            }
        }
    }
}
