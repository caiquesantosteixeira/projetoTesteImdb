using Base.Domain.Retornos;
using System.Threading.Tasks;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Repositorios.Logging;
using Base.Repository.Contracts;
using Base.Domain.DTOs;
using Base.Domain.Entidades;
using Base.Service.Contracts;

namespace Base.Service.Usuario
{
    public class FilmeService:IFilmeService
    {
        private readonly IFilme _repository;
        private readonly ILog _log;
        public FilmeService(IFilme repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }


        public async Task<Retorno> Cadastrar(FilmeInputDTO command)
        {
            //command.Validate();
            //if (command.Invalid)
            //    return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var Filme = new Filme
            {
                Nome = command.Nome,
                Resumo = command.Resumo,
                Tempo = command.Tempo,
                Ano = command.Ano,
                Foto = command.Foto
            };

            return await _repository.Cadastrar(Filme);
        }

        public async Task<Retorno> Atualizar(FilmeInputDTO command)
        {
            //command.Validate();
            //if (command.Invalid)
            //    return new Retorno(false, "Dados Inválidos!", command.Notifications);
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "Filme não existente", "Filme não existente");
            }
            var Filme = new Filme
            {
                Id = command.Id,
                Nome = command.Nome,
                Resumo = command.Resumo,
                Tempo = command.Tempo,
                Ano = command.Ano,
                Foto = command.Foto
            };

            return _repository.Atualizar(Filme);
        }

        public async Task<Retorno> Excluir(FilmeInputDTO command)
        {
            //if (command.Invalid)
            //    return new Retorno(false, "Dados Inválidos!", command.Notifications);
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "Filme não existente", "Filme não existente");
            }
            return await _repository.Excluir(command.Id);
        }

        public async Task<Retorno> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Retorno> Get(int id)
        {
            var retorno = await _repository.GetById(id);
            return retorno;
        }

        public async Task<Retorno> GetAllPaginado(int QtdPorPagina, int PagAtual, string TipoOrdenação, string Filtro = null, string ValueFiltro = null)
        {
            return await _repository.DadosPaginado( QtdPorPagina,  PagAtual,  TipoOrdenação,  Filtro,  ValueFiltro);
        }
    }
}
