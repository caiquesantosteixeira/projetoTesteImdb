using Base.Domain.Entidades.Menu;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Repositorios.Menu;
using Base.Domain.Retornos;
using Base.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Infra.Repositorios.Menu
{
    public class PerfilRepository : IPerfil
    {
        private readonly DataContext _ctx;
        private readonly IdentityDataContext _ctxIdentity;
        private readonly ILog _log;
        public PerfilRepository(DataContext dataContext, IdentityDataContext identityDataContext, ILog log)
        {
            _ctx = dataContext;
            _ctxIdentity = identityDataContext;
            _log = log;
        }

        public async Task<Perfil> Atualizar(Perfil perfilUsuario)
        {
            try
            {
               _ctx.Perfil.Update(perfilUsuario);
                await _ctx.SaveChangesAsync();
                return perfilUsuario;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Atualizar o Perfil", ex);
            }
        }

        public async Task<Perfil> Cadastrar(Perfil perfilUsuario)
        {
            try
            {       
                _ctx.Perfil.Add(perfilUsuario);
                await _ctx.SaveChangesAsync();
                return perfilUsuario;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Cadastrar o Perfil", ex);
            }            
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var perfil = await _ctx.Perfil.FirstOrDefaultAsync(a => a.Id == id);
                if (perfil == null)
                {
                    return new Retorno(false, "Não autorizado!", "Perfil não encontrado.");
                }

                var ExistUsuario = await _ctxIdentity.Usuarios.FirstOrDefaultAsync(a => a.IdPerfil == id);
                if (ExistUsuario != null)
                {
                    return new Retorno(false, "Não autorizado!", "Já existe um usuário com esse perfil.");
                }
                var perfilUsuarioList = await _ctx.PerfilUsuario.AsNoTracking().Where(a => a.IdPerfil == id).ToListAsync();
                if(perfilUsuarioList.Count > 0)
                {
                    foreach (var perfilUsuario in perfilUsuarioList)
                    {
                        var ExitsPerUsuBtn = await _ctx.PerfilUsuarioBotoes.Where(a => a.IdPerfilUsuario == perfilUsuario.Id).ToListAsync();
                        if (ExitsPerUsuBtn.Count() > 0)
                        {
                            _ctx.PerfilUsuarioBotoes.RemoveRange(ExitsPerUsuBtn);
                            await _ctx.SaveChangesAsync();
                        }
                    }

                    // remove o perfil do usuario
                    _ctx.PerfilUsuario.RemoveRange(perfilUsuarioList);
                    await _ctx.SaveChangesAsync();
                }         

                _ctx.Perfil.Remove(perfil);
                await _ctx.SaveChangesAsync();
                return new Retorno (true, "Sucesso", "Registro excluido com sucesso!");
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Excluir o Perfil", ex);
            }
        }

        public async Task<IEnumerable<Perfil>> GetAll()
        {
            try
            {
                var lista = await(_ctx.Perfil.Select(a => new Perfil
                {
                    Id = a.Id,
                    Nome = a.Nome
                }))
                .OrderBy(n => n.Nome)
                .ToListAsync();
                return lista;

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Listar os Perfil", ex);
            }
        }

        public async Task<Perfil> PerfilById(int id)
        {
            try
            {
                return  await _ctx.Perfil.FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Consultar o Perfil", ex);
            }
        }
    }
}
