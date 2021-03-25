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
    public class DiretorRepository:IDiretor
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;
        private readonly IUserIdentity _userIdentity;
        public DiretorRepository(testeimdbContext context, ILog log, IUserIdentity userIdentity)
        {
            _ctx = context;
            _log = log;
            _userIdentity = userIdentity;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.Diretor.Select(a => new DiretorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().ToListAsync();
                return new Retorno(true, "Perfil Cadastrado com Sucesso.", lista);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar Login", ex: ex);
                throw new Exception("Erro ao Consultar Login", ex);
            }
        }

        public async Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null)
        {
            try
            {
                var lista = _ctx.Diretor.Select(a => new DiretorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                });

                //filtro
                if (Filtro != null && ValueFiltro != null)
                {
                    Filtro = Filtro.Trim().ToUpper();

                    switch (Filtro)
                    {
                        case "CODIGO":
                            lista = lista.Where(a => a.Id.ToString() == ValueFiltro);
                            break;
                        case "NOME":
                            lista = lista.Where(a =>
                                                    a.Nome.Contains(ValueFiltro))
                                         .OrderBy(a => a.Nome);
                            break;
                    }
                }
                else
                {

                    lista = lista
                            .OrderBy(a => a.Id);
                }

                var dados = Paginacao.GetPage<Diretor, DiretorDTO>(_ctx.Diretor, lista, QtdPorPagina, PagAtual);
                return new Retorno(true, "Dados Paginados", dados);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Paginar Usuarios", ex: ex);
                throw new Exception("Erro ao Paginar Usuarios", ex);
            }
        }

        public async Task<Retorno> GetById(string id)
        {
            try
            {

                var diretor = await _ctx.Diretor.Select(a => new DiretorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

                return new Retorno(true, "Diretor", diretor);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar Usuario", ex: ex);
                throw new Exception("Erro ao Consultar Usuario", ex);
            }
        }


        public async Task<Retorno> Cadastrar(Diretor diretor)
        {
            try
            {

                if (!_userIdentity.ValidarUsuario())
                {
                    return new Retorno(false, "Só Administradores podem cadastrar.", "Só Administradores podem cadastrar."); ;
                }

                _ctx.Diretor.Add(diretor);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "Diretor cadastrado com sucesso.", "Diretor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Diretor", ex: ex);
                throw new Exception("Erro ao Cadastrar o Diretor", ex);
            }
        }

        public async Task<Retorno> Atualizar(Diretor diretor)
        {
            try
            {

                if (!_userIdentity.ValidarUsuario())
                {
                    return new Retorno(false, "Só Administradores podem cadastrar.", "Só Administradores podem cadastrar."); ;
                }

                var usuExist = await _ctx.Diretor.Select(a => new DiretorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == diretor.Id);


                if (usuExist == null)
                    return new Retorno(false, "Diretor Não existe", "Diretor Não existe");

                _ctx.Diretor.Update(diretor);
                await _ctx.SaveChangesAsync();

                return new Retorno(true, "Diretor atualizado com sucesso.", "Diretor cadastrado com sucesso."); ;
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Diretor", ex: ex);
                throw new Exception("Erro ao Cadastrar o Diretor", ex);
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
                var diretor = await _ctx.Diretor.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (diretor == null)
                {
                    return new Retorno(false, "Não Autorizado", "Diretor não encontrado.");
                }


                _ctx.Diretor.Remove(diretor);
                _ctx.SaveChanges();
                return new Retorno(true, "Diretor Excluido.", "Diretor Excluido.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir Diretor", ex: ex);
                throw new Exception("Erro ao Excluir Diretor", ex);
                throw new Exception("Erro ao Excluir Diretor", ex);
            }
        }
    }
}
