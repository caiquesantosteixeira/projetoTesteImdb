using Base.Domain.Entidades.Menu;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Repositorios.Menu;
using Base.Domain.Retornos;
using Base.Infra.Context;
using Base.Infra.Helpers.Paginacao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Infra.Repositorios.Menu
{
    public class MenuOpcoesRepository : IMenuOpcoes
    {
        private readonly DataContext _ctx;
        private readonly ILog _log;
        public MenuOpcoesRepository(DataContext dataContext, ILog log)
        {
            _ctx = dataContext;
            _log = log;
        }

        public async Task<MenuOpcoes> Atualizar(MenuOpcoes menuopcoes)
        {
            try
            {                
                _ctx.MenuOpcoes.Update(menuopcoes);
                await _ctx.SaveChangesAsync();
                return menuopcoes;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Atualizar o menu da empresa", ex);
            }            
        }

        public async Task<MenuOpcoes> Cadastrar(MenuOpcoes menuopcoes)
        {
            try
            {
                _ctx.MenuOpcoes.Add(menuopcoes);
                await _ctx.SaveChangesAsync();

                // insere automaticamente no modulo
                var modulo = new ModuloMenuOpcoes();
                modulo.IdModulo = 1;
                modulo.IdMenuOpcoes = menuopcoes.Id;

                _ctx.ModuloMenuOpcoes.Add(modulo);
                await _ctx.SaveChangesAsync();

                return menuopcoes;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Cadastrar o menu da empresa", ex);
            }            
        }

        public async Task<IEnumerable<object>> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null)
        {
            try
            {
                var lista = _ctx.MenuOpcoes.Select(a => new MenuOpcoes
                {
                    Id = a.Id,
                    IdMenu = a.IdMenu,
                    PathUrl = a.PathUrl,
                    SubMenu = a.SubMenu,
                    Titulo = a.Titulo,
                    Ativo = a.Ativo,
                    VisivelMenu = a.VisivelMenu,
                    SlugPermissao = a.SlugPermissao
                });

                //filtro
                if (Filtro != null && ValueFiltro != null)
                {
                    Filtro = Filtro.Trim().ToUpper();

                    switch (Filtro)
                    {
                        case "CODIGO":
                            lista = lista.Where(a => a.Id == int.Parse(ValueFiltro));
                            break;
                        case "NOME":
                            lista = lista.Where(a => a.Titulo.Contains(ValueFiltro))
                                         .OrderBy(a => a.Titulo);
                            break;
                    }
                }
                else
                {

                    lista = lista
                            .OrderByDescending(a => a.Id);
                }

               return Paginacao.GetPage<MenuOpcoes, MenuOpcoes>(_ctx.MenuOpcoes, lista, QtdPorPagina, PagAtual);                
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao listar o menuopcoes paginado", ex);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var verificaModuloMenuOpcoes = _ctx.ModuloMenuOpcoes.FirstOrDefault(a => a.IdMenuOpcoes == id);
                if (verificaModuloMenuOpcoes != null)
                {
                    var verificaPerfilUsuario = _ctx.PerfilUsuario.FirstOrDefault(a => a.IdModuloMenuOpc == verificaModuloMenuOpcoes.Id);
                    if (verificaPerfilUsuario != null)
                    {
                        return new Retorno(false, "Não autorizado!", "Já existe um verificaPerfilUsuario com esse MenuOpcoes.");
                    }

                    _ctx.ModuloMenuOpcoes.Remove(verificaModuloMenuOpcoes);
                    await _ctx.SaveChangesAsync();
                    //return new Retorno(false, "Não autorizado!", "Já existe um Modulo_Menu_Opcoes com esse Menu.");                   
                }

                var verificaMenuOpcoesBotoes = _ctx.MenuOpcoesBotoes.FirstOrDefault(a => a.IdMenuOpcoes == id);
                if (verificaMenuOpcoesBotoes != null)
                {
                    return new Retorno(false, "Não autorizado!", "Já existe um MenuOpcoesBotoes com esse MenuOpcoes.");                   
                }               

                var objeto = await MenuOpcoesById(id);
                if (objeto == null)
                {                    
                    return new Retorno(false, "Menu de Opções não existe!", "Menu de Opções não existe!");
                }

                _ctx.MenuOpcoes.Remove(objeto);
                _ctx.SaveChanges();

                return new Retorno(true, "Registro Excluido.", "Registro Excluido.");
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Excluir o menu da empresa", ex);
            }
        }

        public async Task<IEnumerable<MenuOpcoes>> GetAll()
        {
            try
            {
                var lista = await _ctx.MenuOpcoes
                                .Select(a => new MenuOpcoes
                                {
                                    Id = a.Id,
                                    Titulo = a.Titulo
                                })
                                .OrderBy(n => n.Titulo)
                                .ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao listar o menuopcoes GetAll", ex);
            }
        }

        public async Task<MenuOpcoes> MenuOpcoesById(int id)
        {
            try
            {
                return await _ctx.MenuOpcoes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);               
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao consultar o menuopcoes por Id", ex);
            }
        }

        public async Task<IEnumerable<object>> OpcoesEmpresa()
        {
            try
            {
                var lista = await _ctx.MenuOpcoes
                                .Join(_ctx.ModuloMenuOpcoes, mo => mo.Id, mmo => mmo.IdMenuOpcoes, (mo, mmo) => new { mo, mmo })                                
                                .Select(a => new {
                                    Id = a.mo.Id,
                                    IdModuloMenuOpc = a.mmo.Id,
                                    Titulo = a.mo.Titulo,
                                    IdEmpresa = 1
                                })
                                .Where(a => a.IdEmpresa == 1)
                                .ToListAsync();
                return lista;
            }
            catch(Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao listar o menu da empresa", ex);
            }
        }
    }
}
