using Base.Domain.Repositorios.Logging;
using Base.Domain.Retornos;
using Base.Repository.Contracts;
using Base.Repository.Helpers.Paginacao;
using Base.Repository.Repositorios.Usuario;
using Base.Rpepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Repository.Repositorios
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
      where TEntity : class
    {
        private readonly testeimdbContext _ctx;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILog _log;
        private readonly IUserIdentity _userIdentity;
        public BaseRepository(testeimdbContext context, ILog log, IUserIdentity userIdentity)
        {
            _ctx = context;
            _dbSet = _ctx.Set<TEntity>();
            _log = log;
            _userIdentity = userIdentity;

        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.Set<TEntity>().ToListAsync();
                return new Retorno(true, "", lista);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar", ex: ex);
                throw new Exception("Erro ao Consultar", ex);
            }
        }
        public async Task<Retorno> GetById(int id)
        {
            try
            {
                var retorno = await _ctx.Set<TEntity>().FindAsync(id);
                _ctx.Entry(retorno).State = EntityState.Detached;
                return new Retorno(true, "", retorno);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar", ex: ex);
                throw new Exception("Erro ao Consultar", ex);
            }
        }

        public async Task<Retorno> Cadastrar(TEntity obj)
        {

            if (!_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Só Administradores podem cadastrar.", "Só Administradores podem cadastrar."); ;
            }

            try
            {
                var retorno = await _dbSet.AddAsync(obj);
                SaveChanges();
                return new Retorno(true, "Cadastrado com sucesso.", retorno.Entity);
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar", ex: ex);
                throw new Exception("Erro ao Cadastrar", ex);
            }
        }

        public Retorno Atualizar(TEntity obj)
        {
            if (!_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Só Administradores podem Atualizar.", "Só Administradores podem cadastrar."); ;
            }
            try
            {
                _dbSet.Attach(obj);
                var retorno =  _dbSet.Update(obj).Entity;
                SaveChanges();
                return new Retorno(true, "Atualizado com sucesso.", retorno); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Atualizar", ex: ex);
                throw new Exception("Erro ao Atualizar", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                if (!_userIdentity.ValidarUsuario())
                {
                    return new Retorno(false, "Só Administradores podem Excluir.", "Só Administradores podem Excluir."); ;
                }

                var retorno = await _dbSet.FindAsync(id);
                _dbSet.Remove(retorno);
                SaveChanges();
                return new Retorno(true, "Excluido.", "Excluido.");
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir Ator", ex: ex);
                throw new Exception("Erro ao Excluir", ex);
            }
        }

        public int SaveChanges()
        {
            return _ctx.SaveChanges();
        }
    }
}
