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
    public class FilmeXDiretorRepository:IFilmeXDiretor
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;
        private readonly IUserIdentity _userIdentity;
        public FilmeXDiretorRepository(testeimdbContext context, ILog log, IUserIdentity userIdentity)
        {
            _ctx = context;
            _log = log;
            _userIdentity = userIdentity;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.FilmeXdiretor.Select(a => new FilmeXdiretor
                {
                    Id = a.Id,
                    IdFilme = a.IdFilme,
                    IdDiretor = a.IdDiretor
                }).AsNoTracking().ToListAsync();
                return new Retorno(true, "", lista);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar FilmeXdiretor", ex: ex);
                throw new Exception("Erro ao Consultar FilmeXdiretor", ex);
            }
        }


        public async Task<Retorno> Cadastrar(FilmeXdiretor filmeXDiretor)
        {
            if (!_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Só Administradores podem cadastrar.", "Só Administradores podem cadastrar."); ;
            }

            try
            {
                _ctx.FilmeXdiretor.Add(filmeXDiretor);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXDiretor cadastrado com sucesso.", "FilmeXFilmeXDiretor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXFilmeXDiretor", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXFilmeXDiretor", ex);
            }
        }

        public async Task<Retorno> Atualizar(FilmeXdiretor filmeXFilmeXDiretor)
        {
            try
            {
                if (!_userIdentity.ValidarUsuario())
                {
                    return new Retorno(false, "Só Administradores podem cadastrar.", "Só Administradores podem cadastrar."); ;
                }
                var usuExist = await _ctx.FilmeXdiretor.Select(a => new FilmeXDiretorDTO
                {
                    Id = a.Id
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == filmeXFilmeXDiretor.Id);

                if (usuExist == null)
                    return new Retorno(false, "FilmeXDiretor Não existe", "FilmeXDiretor Não existe");

                _ctx.FilmeXdiretor.Update(filmeXFilmeXDiretor);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXDiretor atualizado com sucesso.", "FilmeXDiretor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXDiretor", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXDiretor", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                if (!_userIdentity.ValidarUsuario())
                {
                    return new Retorno(false, "Só Administradores podem cadastrar.", "Só Administradores podem cadastrar."); ;
                }

                var FilmeXDiretor = await _ctx.FilmeXdiretor.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (FilmeXDiretor == null)
                {
                    return new Retorno(false, "Não Autorizado", "FilmeXDiretor não encontrado.");
                }


                _ctx.FilmeXdiretor.Remove(FilmeXDiretor);
                _ctx.SaveChanges();
                return new Retorno(true, "FilmeXDiretor Excluido.", "FilmeXDiretor Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir FilmeXDiretor", ex: ex);
                throw new Exception("Erro ao Excluir FilmeXDiretor", ex);
                throw new Exception("Erro ao Excluir FilmeXDiretor", ex);
            }
        }
    }
}
