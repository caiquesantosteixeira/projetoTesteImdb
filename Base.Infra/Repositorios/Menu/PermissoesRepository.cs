using Base.Domain.Entidades.Menu;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Repositorios.Menu;
using Base.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Infra.Repositorios.Menu
{
    public class PermissoesRepository: IPermissoes
    {
        private readonly DataContext _ctx;
        private readonly ILog _log;
        public PermissoesRepository(DataContext dataContext, ILog log)
        {
            _ctx = dataContext;
            _log = log;
        }

        public async Task<IEnumerable<Permissoes>> GetAll()
        {
            try
            {
                return  await _ctx.Permissoes
                                      .OrderBy(n => n.Nome)
                                      .ToListAsync();               
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao listar Permissoes GetAll", ex);
            }
        }

        public async Task<IEnumerable<object>> GetPerMenuOpcoes(int idmenuopc)
        {
            try
            {
                var lista = await _ctx.MenuOpcoesBotoes
                            .Join(_ctx.MenuOpcoes, mob => mob.IdMenuOpcoes, mo => mo.Id, (mob, mo) => new { mob, mo })
                            .Join(_ctx.Permissoes, mob => mob.mob.IdPermissoes, p => p.Id, (mob, p) => new { mob, p })
                            .Select(a => new {
                                Id = a.p.Id,
                                Nome = a.p.Nome,
                                IdMenuOpcoes = a.mob.mo.Id
                            }).Where(a => a.IdMenuOpcoes == idmenuopc).ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro ao listar Permissoes GetPerMenuOpcoesAsync", ex);
            }
        }
    }
}
