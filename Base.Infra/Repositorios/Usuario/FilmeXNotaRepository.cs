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
    public class FilmeXNotaRepository : IFilmeXNota
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;
        private readonly IUserIdentity _userIdentity;
        public FilmeXNotaRepository(testeimdbContext context, ILog log, IUserIdentity userIdentity)
        {
            _ctx = context;
            _log = log;
            _userIdentity = userIdentity;
        }


        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.FilmeXnota.Select(a => new FilmeXnota
                {
                    Id = a.Id,
                    IdFilme = a.IdFilme,
                    IdUsuario = a.IdUsuario
                }).AsNoTracking().ToListAsync();
                return new Retorno(true, "", lista);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar FilmeXNota", ex: ex);
                throw new Exception("Erro ao Consultar FilmeXNota", ex);
            }
        }


        public async Task<Retorno> Cadastrar(FilmeXnota filmeXNota)
        {

            if (_userIdentity.ValidarUsuario())
            {
                return new Retorno(false, "Somente usuário não admins podem votar", "Somente usuário não admins podem votar"); 
            }
            try
            {
                _ctx.FilmeXnota.Add(filmeXNota);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXNota cadastrado com sucesso.", "FilmeXFilmeXNota cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXFilmeXNota", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXFilmeXNota", ex);
            }
        }

        public async Task<Retorno> Atualizar(FilmeXnota filmeXNota)
        {
            try
            {
                var usuExist = await _ctx.FilmeXnota.Select(a => new FilmeXnota
                {
                    Id = a.Id
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == filmeXNota.Id);

                if (usuExist == null)
                    return new Retorno(false, "FilmeXNota Não existe", "FilmeXNota Não existe");

                _ctx.FilmeXnota.Update(filmeXNota);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "FilmeXNota atualizado com sucesso.", "FilmeXNota cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o FilmeXNota", ex: ex);
                throw new Exception("Erro ao Cadastrar o FilmeXNota", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var filmeXNota = await _ctx.FilmeXnota.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (filmeXNota == null)
                {
                    return new Retorno(false, "Não Autorizado", "FilmeXNota não encontrado.");
                }


                _ctx.FilmeXnota.Remove(filmeXNota);
                _ctx.SaveChanges();
                return new Retorno(true, "FilmeXNota Excluido.", "FilmeXNota Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir FilmeXNota", ex: ex);
                throw new Exception("Erro ao Excluir FilmeXNota", ex);
                throw new Exception("Erro ao Excluir FilmeXNota", ex);
            }
        }
    }
}
