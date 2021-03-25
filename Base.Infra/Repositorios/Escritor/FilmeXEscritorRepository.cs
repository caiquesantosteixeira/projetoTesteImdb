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
    public class FilmeXEscritorRepository:IFilmeXEscritor
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;
        private readonly IUserIdentity _userIdentity;
        public FilmeXEscritorRepository(testeimdbContext context, ILog log, IUserIdentity userIdentity)
        {
            _ctx = context;
            _log = log;
            _userIdentity = userIdentity;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.FilmeXescritor.Select(a => new FilmeXescritor
                {
                    Id = a.Id,
                    IdFilme = a.IdFilme,
                    IdEscritor = a.IdEscritor
                }).AsNoTracking().ToListAsync();
                return new Retorno(true, "", lista);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar FilmeXescritor", ex: ex);
                throw new Exception("Erro ao Consultar FilmeXescritor", ex);
            }
        }


        public async Task<Retorno> Cadastrar(FilmeXescritor FilmeXEscritor)
        {
            if (!_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Só Administradores podem persistir.", "Só Administradores podem persistir."); ;
            }
            try
            {
                _ctx.FilmeXescritor.Add(FilmeXEscritor);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXEscritor cadastrado com sucesso.", "FilmeXFilmeXEscritor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXFilmeXEscritor", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXFilmeXEscritor", ex);
            }
        }

        public async Task<Retorno> Atualizar(FilmeXescritor filmeXFilmeXEscritor)
        {
            if (!_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Só Administradores podem persistir.", "Só Administradores podem persistir."); ;
            }
            try
            {
                var usuExist = await _ctx.FilmeXescritor.Select(a => new FilmeXEscritorDTO
                {
                    Id = a.Id
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == filmeXFilmeXEscritor.Id);

                if (usuExist == null)
                    return new Retorno(false, "FilmeXEscritor Não existe", "FilmeXEscritor Não existe");

                _ctx.FilmeXescritor.Update(filmeXFilmeXEscritor);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXEscritor atualizado com sucesso.", "FilmeXEscritor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXEscritor", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXEscritor", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            if (!_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Só Administradores podem persistir.", "Só Administradores podem persistir."); ;
            }
            try
            {
                var FilmeXEscritor = await _ctx.FilmeXescritor.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (FilmeXEscritor == null)
                {
                    return new Retorno(false, "Não Autorizado", "FilmeXEscritor não encontrado.");
                }


                _ctx.FilmeXescritor.Remove(FilmeXEscritor);
                _ctx.SaveChanges();
                return new Retorno(true, "FilmeXEscritor Excluido.", "FilmeXEscritor Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir FilmeXEscritor", ex: ex);
                throw new Exception("Erro ao Excluir FilmeXEscritor", ex);
                throw new Exception("Erro ao Excluir FilmeXEscritor", ex);
            }
        }
    }
}
