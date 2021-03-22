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
    public class MenuRepository: IMenu
    {
        public readonly DataContext _ctx;
        private readonly ILog _log;
        public MenuRepository(DataContext dataContext, ILog log)
        {
            _ctx = dataContext;
            _log = log;
        }

        public async Task<Domain.Entidades.Menu.Menu> Cadastrar(Domain.Entidades.Menu.Menu menu)
        {
            try
            {
                _ctx.Menu.Add(menu);
                await  _ctx.SaveChangesAsync();

                return menu;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Adicionar o menu", ex);
            }            
        }

        public async Task<Domain.Entidades.Menu.Menu> Atualizar(Domain.Entidades.Menu.Menu menu)
        {
            try
            {
                _ctx.Menu.Update(menu);
                await _ctx.SaveChangesAsync();
                return menu;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Atualizar o menu", ex);
            }           
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Retorno (false, "Não Autorizado", "Código do MenuOpcoesBotoes não fornecido.");
                }

                var menuOpcoesExists = await _ctx.MenuOpcoes.FirstOrDefaultAsync(a => a.IdMenu == id);
                if (menuOpcoesExists != null)
                {                   
                   return new Retorno (false, "Não Autorizado", "Já existe um menu de opções vinculado esse menu.");
                }

                var objeto = await GetById(id);
                if (objeto == null)
                {
                    return new Retorno (false, "Não Autorizado", "Menu não cadastrado.");                    
                }

                _ctx.Menu.Remove(objeto);
                _ctx.SaveChanges();

                return new Retorno (true, "Menu Excluido com sucesso.", "Menu Excluido com sucesso.");
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Excluir o menu", ex);
            }
        }

        public async Task<IEnumerable<Domain.Entidades.Menu.Menu>> GetAll()
        {
            try
            {
                return await _ctx.Menu.OrderBy(a => a.Menu1).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Listar todos os menu", ex);
            }
        }

        public async Task<Domain.Entidades.Menu.Menu> GetById(int id)
        {
            try
            {
                return await _ctx.Menu.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Buscar o menu por id.", ex);
            }
        }
    }
}
