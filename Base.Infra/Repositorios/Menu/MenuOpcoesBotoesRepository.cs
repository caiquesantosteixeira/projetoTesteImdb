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
    public class MenuOpcoesBotoesRepository: IMenuOpcoesBotoes
    {
        public readonly DataContext _ctx;
        private readonly ILog _log;
        public MenuOpcoesBotoesRepository(DataContext context, ILog log)
        {
            _ctx = context;
            _log = log;
        }

        public async Task<MenuOpcoesBotoes> Cadastrar(MenuOpcoesBotoes menuOpcoesBotoes)
        {
            try
            {
                _ctx.MenuOpcoesBotoes.Add(menuOpcoesBotoes);
                await _ctx.SaveChangesAsync();
                return menuOpcoesBotoes;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Cadastrar MenuOpcoesBotoes", ex);
            }            
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                if (id <= 0)
                    return new Retorno (false, "Não Autorizado!", "Código do MenuOpcoesBotoes não fornecido.");

                var menuOpcoesBotoes = await GetById(id);
                if (menuOpcoesBotoes == null)
                    return new Retorno (false, "Não Autorizado!", "MenuOpcoesBotoes não encontrado.");

                _ctx.MenuOpcoesBotoes.Remove(menuOpcoesBotoes);
                await _ctx.SaveChangesAsync();

                return new Retorno (true, "Sucesso", "Operação executada com sucesso.");
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Excluir MenuOpcoesBotoes", ex);
            }            
        }

        public async Task<MenuOpcoesBotoes> Get(int idPermissoes, int idMenuOpcoes)
        {
            try
            {
                return await _ctx.MenuOpcoesBotoes.AsNoTracking().FirstOrDefaultAsync(a => a.IdPermissoes == idPermissoes
                                                                            && a.IdMenuOpcoes == idMenuOpcoes);
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro Get MenuOpcoesBotoes", ex);
            }
        }

        public async Task<IEnumerable<MenuOpcoesBotoes>> GetAll()
        {
            try
            {
                return await _ctx.MenuOpcoesBotoes.Select(a => new MenuOpcoesBotoes
                {
                    Id = a.Id,
                    IdPermissoes = a.IdPermissoes,
                    IdMenuOpcoes = a.IdMenuOpcoes
                })
               .OrderBy(n => n.Id)
               .AsNoTracking()
               .ToListAsync();                
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro no GetAll do MenuOpcoesBotoes", ex);
            }
        }

        public async Task<MenuOpcoesBotoes> GetById(int id)
        {
            try
            {
               return await _ctx.MenuOpcoesBotoes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao buscar por Id MenuOpcoesBotoes", ex);
            }
        }

        public async Task<IEnumerable<MenuOpcoesBotoes>> GetPermissoesMenu(int idMenuOpcoes)
        {
            try
            {
                var lista = await _ctx.MenuOpcoesBotoes
                                    .Join(_ctx.Permissoes, mob => mob.IdPermissoes, p => p.Id, (mob, p) => new { mob, p })
                                    .Select(a => new MenuOpcoesBotoes
                                    {
                                        Id = a.mob.Id,
                                        IdMenuOpcoes = a.mob.IdMenuOpcoes,
                                        IdPermissoes = a.mob.IdPermissoes,
                                        DescPermissao = a.p.Nome
                                    }).Where(a => a.IdMenuOpcoes == idMenuOpcoes)
                                    .ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro no GetPermissoesMenu do MenuOpcoesBotoes", ex);
            }
        }
    }
}
