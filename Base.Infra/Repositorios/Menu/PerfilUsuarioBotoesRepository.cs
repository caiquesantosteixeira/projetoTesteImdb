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
    public class PerfilUsuarioBotoesRepository: IPerfilUsuarioBotoes
    {
        private readonly DataContext _ctx;
        private readonly ILog _log;
        public PerfilUsuarioBotoesRepository(DataContext context, ILog log)
        {
            _ctx = context;
            _log = log;
        }

        public async Task<PerfilUsuarioBotoes> Cadastrar(PerfilUsuarioBotoes perfil)
        {
            try
            {
                _ctx.PerfilUsuarioBotoes.Add(perfil);
                await _ctx.SaveChangesAsync();
                return perfil;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Atualizar o PerfilUsuarioBotoes", ex);
            }            
        }

        public async Task<bool> Cadastrar(List<PerfilUsuarioBotoes> perfis)
        {
            try
            {
                _ctx.PerfilUsuarioBotoes.AddRange(perfis);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Atualizar o PerfilUsuarioBotoes", ex);
            }            
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var objeto = await _ctx.PerfilUsuarioBotoes.FirstOrDefaultAsync(a => a.Id == id);
                if (objeto == null)
                {
                    return new Retorno (false, "Não autorizado!", "PerfilUsuarioBotoes não encontrado.");
                }

                _ctx.PerfilUsuarioBotoes.Remove(objeto);
                await _ctx.SaveChangesAsync();

                return new Retorno (true, "PerfilUsuarioBotoes excluido com sucesso.", "PerfilUsuarioBotoes excluido com sucesso.");
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Atualizar o PerfilUsuarioBotoes", ex);
            }            
        }

        public async Task<IEnumerable<PerfilUsuarioBotoes>> GetAll()
        {
            try
            {
                var lista = await _ctx.PerfilUsuarioBotoes
                            .Join(_ctx.Permissoes, pub => pub.IdPermissoes, p => p.Id, (pub, p) => new { pub, p })
                            .Select(a => new PerfilUsuarioBotoes
                            {
                                Id = a.pub.Id,
                                IdPermissoes = a.pub.IdPermissoes,
                                IdPerfilUsuario = a.pub.IdPerfilUsuario,
                                Nome = a.p.Nome
                            })
                .OrderBy(n => n.Nome)
                .AsNoTracking()
                .ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao obter todos o PerfilUsuarioBotoes", ex);
            }          
        }

        public async Task<IEnumerable<PerfilUsuarioBotoes>> GetAllPermissaoPerfil(int idPerfil)
        {
            try
            {
                var lista = await _ctx.PerfilUsuarioBotoes
                            .Join(_ctx.Permissoes, pub => pub.IdPermissoes, p => p.Id, (pub, p) => new { pub, p })
                            .Select(a => new PerfilUsuarioBotoes
                            {
                                Id = a.pub.Id,
                                IdPermissoes = a.pub.IdPermissoes,
                                IdPerfilUsuario = a.pub.IdPerfilUsuario,
                                Nome = a.p.Nome
                            })
                .OrderBy(n => n.Nome)
                .Where(a => a.IdPerfilUsuario == idPerfil)
                .AsNoTracking()
                .ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao obter todas as permissoes do perfil", ex);
            }
        }

        public async Task<PerfilUsuarioBotoes> GetPermissaoById(int idPermissao)
        {
            try
            {
                return await _ctx.PerfilUsuarioBotoes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == idPermissao);
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao obter a permissao do usuário", ex);
            }           
        }

        public async Task<PerfilUsuarioBotoes> GetPermissaoUsuario(int idPermissao, int idPerfilUsuario)
        {
            try
            {
               return  await _ctx.PerfilUsuarioBotoes.AsNoTracking().FirstOrDefaultAsync(
                                            a => a.IdPermissoes == idPermissao &&
                                            a.IdPerfilUsuario == idPerfilUsuario);
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao obter a permissao do Usuário", ex);
            }           
        }
    }
}
