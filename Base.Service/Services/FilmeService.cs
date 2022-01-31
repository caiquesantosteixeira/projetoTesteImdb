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


        public async Task<Retorno> Cadastrar(FilmeInsertDTO command)
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

            var cadastrado = await _repository.Cadastrar(Filme);

            if (cadastrado.Sucesso)
            {
                var cadastradoConvertido = (Filme)cadastrado.Data;

                var ret = new FilmeDTO
                {
                    Id = cadastradoConvertido.Id,
                    Nome = cadastradoConvertido.Nome,
                    Resumo = cadastradoConvertido.Resumo,
                    Tempo = cadastradoConvertido.Tempo,
                    Ano = cadastradoConvertido.Ano,
                    Foto = cadastradoConvertido.Foto
                };
                return new Retorno(true, "Cadastrado com sucesso.", ret);
            }
            else
            {
                return cadastrado;
            }
        }

        public async Task<Retorno> Atualizar(FilmeUpdateDTO command)
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

            var cadastrado = _repository.Atualizar(Filme);

            if (cadastrado.Sucesso)
            {
                var cadastradoConvertido = (Filme)cadastrado.Data;

                var ret = new FilmeDTO
                {
                    Id = cadastradoConvertido.Id,
                    Nome = cadastradoConvertido.Nome,
                    Resumo = cadastradoConvertido.Resumo,
                    Tempo = cadastradoConvertido.Tempo,
                    Ano = cadastradoConvertido.Ano,
                    Foto = cadastradoConvertido.Foto
                };
                return new Retorno(true, "Atualizado com sucesso.", ret);
            }
            else
            {
                return cadastrado;
            }
        }

        public async Task<Retorno> Excluir(FilmeDTO command)
        {
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
