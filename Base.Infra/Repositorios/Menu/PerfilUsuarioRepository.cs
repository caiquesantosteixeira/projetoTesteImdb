using Base.Domain.DTOs.Usuario;
using Base.Domain.Entidades.Menu;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Repositorios.Menu;
using Base.Domain.Retornos;
using Base.Infra.Context;
using Base.Infra.Helpers;
using Base.Infra.Helpers.Paginacao;
using Base.Infra.Repositorios.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Infra.Repositorios.Menu
{
    public class PerfilUsuarioRepository : IPerfilUsuario
    {   
        private readonly DataContext _ctx;
        private readonly ILog _log;
        private readonly IHttpContextAccessor _accessor;
        public PerfilUsuarioRepository(DataContext dataContext, ILog log, IHttpContextAccessor accessor)
        {
            _ctx = dataContext;
            _log = log;
            _accessor = accessor;
        }

        public async Task<PerfilUsuario> Cadastrar(PerfilUsuario perfil)
        {
            try
            {            
                var _idModulo = await _ctx.ModuloMenuOpcoes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == perfil.IdModuloMenuOpc);
                var _idMenuOpc = await _ctx.MenuOpcoes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == _idModulo.IdMenuOpcoes);
                _idMenuOpc.VisivelMenu = perfil.VisivelMenu;
                _ctx.MenuOpcoes.Update(_idMenuOpc);
                _ctx.SaveChanges();

                PerfilUsuario pf = new PerfilUsuario();                
                pf.Id = perfil.Id;
                pf.IdModuloMenuOpc = perfil.IdModuloMenuOpc;
                pf.IdPerfil = perfil.IdPerfil;

                _ctx.PerfilUsuario.Add(pf);
                await _ctx.SaveChangesAsync();

                return pf;                
            }
            catch(Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Cadastrar o Perfil do Usuário", ex);
            }
        }

        public async Task<PerfilUsuario> Consultar(int IdPerfil, int IdModuloMenuOpcoes)
        {
            try
            {
                return await _ctx.PerfilUsuario.AsNoTracking().FirstOrDefaultAsync(a => a.IdPerfil == IdPerfil
                                                                            && a.IdModuloMenuOpc == IdModuloMenuOpcoes);
            }catch(Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao consultar o Perfil do Usuário", ex);
            }
        }

        public async Task<Retorno> Excluir(int IdPerfil)
        {
            try
            {               
                var objeto = await _ctx.PerfilUsuario.FirstOrDefaultAsync(a => a.Id == IdPerfil);
                if (objeto == null)
                {
                    return new Retorno (false, "Perfil do Usuario nao encontrado.", "Perfil do Usuario nao encontrado.");                    
                }

                // Exclui todas as permissoes atrelado ao perfil
                var ExitsPerUsuBtn = await _ctx.PerfilUsuarioBotoes.Where(a => a.IdPerfilUsuario == objeto.Id).ToListAsync();
                if (ExitsPerUsuBtn.Count() > 0)
                {
                    _ctx.RemoveRange(ExitsPerUsuBtn);
                    await _ctx.SaveChangesAsync();
                }

                _ctx.Remove(objeto);
                await _ctx.SaveChangesAsync();

                return new Retorno (true, "Perfil do Usuario excluido com successo.", "Perfil do Usuario excluido com successo.");
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao excluir o Perfil do Usuário", ex);
            }
        }

        public async Task<object> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null, int id = 0)
        {
            try
            {
                var lista = _ctx.PerfilUsuario
                            .Join(_ctx.ModuloMenuOpcoes, pu => pu.IdModuloMenuOpc, mmo => mmo.Id, (pu, mmo) => new { pu, mmo })
                            .Join(_ctx.MenuOpcoes, mmo => mmo.mmo.IdMenuOpcoes, mo => mo.Id, (mmo, mo) => new { mmo, mo })
                            .Select(a => new PerfilUsuario
                            {
                                Id = a.mmo.pu.Id,
                                IdPerfil = a.mmo.pu.IdPerfil,
                                Titulo = a.mo.Titulo,
                                IdMenuOpcoes = a.mo.Id,
                                VisivelMenu = a.mo.VisivelMenu
                            });

                //filtro
                if (Filtro != null && ValueFiltro != null)
                {
                    Filtro = Filtro.Trim().ToUpper();

                    switch (Filtro)
                    {
                        case "CODIGO":
                            lista = lista.Where(a => a.IdPerfil == id && a.Id == int.Parse(ValueFiltro));
                            break;
                        case "NOME":
                            lista = lista.Where(a => a.IdPerfil == id && a.Titulo.Contains(ValueFiltro))
                                         .OrderBy(a => a.Titulo);
                            break;
                    }
                }
                else
                {

                    lista = lista
                            .Where(a => a.IdPerfil == id)
                            .OrderBy(a => a.Titulo);
                }

                return Paginacao.GetPage<PerfilUsuario, PerfilUsuario>(_ctx.PerfilUsuario, lista, QtdPorPagina, PagAtual);
            }
            catch(Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new  Exception("Erro ao paginar o perfil do Usuario",ex);
            }
        }

        public async Task<object> PermissaoById(int idPerfil)
        {
            try
            {
               
                var usuAdm = new UserIdentity(_accessor).Administrador;

                var perfil = await _ctx.Perfil.Select(a => new Perfil
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).FirstOrDefaultAsync(a => a.Id == idPerfil);

                var pfUsu = await _ctx.PerfilUsuario
                            .Join(_ctx.ModuloMenuOpcoes, pu => pu.IdModuloMenuOpc, mmo => mmo.Id, (pu, mmo) => new { pu, mmo })
                            .Join(_ctx.MenuOpcoes, mmo => mmo.mmo.IdMenuOpcoes, mo => mo.Id, (mmo, mo) => new { mmo, mo })
                            .Select(a => new OpcoesDTO
                            {
                                Id = a.mo.Id,
                                Path = a.mo.PathUrl,
                                Title = a.mo.Titulo,
                                SubMenu = a.mo.SubMenu,
                                MenuDescricao = a.mo.SubMenu != 1 ? a.mo.Titulo : _ctx.Menu.FirstOrDefault(b => b.Id == a.mo.IdMenu).Menu1,
                                IdPerfil = a.mmo.pu.IdPerfil,
                                IdPerfilUsu = a.mmo.pu.Id,
                                Ativo = a.mo.Ativo,
                                VisivelMenu = a.mo.VisivelMenu                                
                            })
                            .Where(a => a.IdPerfil == perfil.Id && 
                                                      a.Ativo == true && 
                                                      a.VisivelMenu == true &&
                                                      (usuAdm ? true : a.Id != 3)
                                                      )
                            .OrderBy(a => a.Title)
                            .ToListAsync();

                var menuItens = AgruparPermissoes(pfUsu);


                return new { Perfil = perfil, MenuItens = menuItens };                

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Obter o perfil por Id", ex);
            }
        }

        private List<MenuGroupDTO> AgruparPermissoes(List<OpcoesDTO> objOpcoesDTO)
        {
            try
            {
                var ListMenu = new List<MenuGroupDTO>();
                if (objOpcoesDTO.Count > 0)
                {
                    // Percorre a lista selecionando apenas os menus
                    foreach (var item in objOpcoesDTO)
                    {
                        var menu = item.MenuDescricao;
                        var res = ListMenu.FirstOrDefault(a => a.Title.Equals(menu));
                        if (res == null)
                        {
                            var menuGroup = new MenuGroupDTO();
                            menuGroup.Title = menu;
                            var dadosMenu = _ctx.Menu.FirstOrDefault(a => a.Menu1.Equals(menu));
                            if (dadosMenu != null)
                            {
                                menuGroup.Icontype = dadosMenu.Icone;
                                menuGroup.Ordem = dadosMenu.Ordem;
                            }
                            ListMenu.Add(menuGroup);
                        }
                    }

                    // Percorre a lista de menus para adicionar as opções
                    foreach (var itemListMenu in ListMenu)
                    {
                        itemListMenu.Children = objOpcoesDTO.Where(a => a.MenuDescricao.Equals(itemListMenu.Title)).ToList();
                    }
                }

                return ListMenu.OrderBy(a => a.Ordem).ToList();
            }catch(Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao Agrupar o perfil por Id", ex);
            }
        }

        public async Task<IEnumerable<PerfilUsuario>> perfilUsuarioById(int idPerfil)
        {
            try
            {
                return await _ctx.PerfilUsuario.AsNoTracking().Where(a => a.IdPerfil == idPerfil).ToListAsync();
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao consultar o Perfil do Usuário", ex);
            }
        }
    }
}
