using Base.Domain.Retornos;
using System.Threading.Tasks;
using Base.Domain.DTOS.Interfaces;
using Base.Domain.Enums.Usuario.Enums;
using Base.Domain.Repositorios.Logging;
using Base.Repository.Contracts;
using Base.Domain.DTOs;
using Base.Domain.Entidades;
using Base.Service.Contracts;
using Base.Domain.DTOs.Ator;

namespace Base.Service.Usuario
{
    public class FilmeXAtorService:IFilmeXAtorService
    {
        private readonly IFilmeXAtor _repository;
        private readonly ILog _log;
        public FilmeXAtorService(IFilmeXAtor repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public async Task<Retorno> Cadastrar(FilmeXatorInsertDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var filmeXAtor = new FilmeXator
            {
                IdAtor = command.IdAtor,

                IdFilme = command.IdFilme
            };

            var cadastrado = await _repository.Cadastrar(filmeXAtor);

            if (cadastrado.Sucesso)
            {
                var cadastradoConvertido = (FilmeXator)cadastrado.Data;

                var ret = new FilmeXatorDTO
                {
                    Id = cadastradoConvertido.Id,
                    IdFilme = cadastradoConvertido.IdFilme,
                    IdAtor = cadastradoConvertido.IdAtor
                };
                return new Retorno(true, "Cadastrado com sucesso.", ret);
            }
            else
            {
                return cadastrado;
            }
        }

        public async Task<Retorno> Atualizar(FilmeXatorUpdateDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var existente = await _repository.GetById(command.Id);

            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXator não existente", "FilmeXator não existente");
            }

            var filmeXAtor = new FilmeXator
            {
                Id = command.Id,
                IdAtor = command.IdAtor,
                IdFilme = command.IdFilme
            };

            var cadastrado = _repository.Atualizar(filmeXAtor);

            if (cadastrado.Sucesso)
            {
                var cadastradoConvertido = (FilmeXator)cadastrado.Data;

                var ret = new FilmeXatorDTO
                {
                    Id = cadastradoConvertido.Id,
                    IdFilme = cadastradoConvertido.IdFilme,
                    IdAtor = cadastradoConvertido.IdAtor
                };
                return new Retorno(true, "Atualizado com sucesso.", ret);
            }
            else
            {
                return cadastrado;
            }
        }

        public async Task<Retorno> Excluir(FilmeXatorDTO command)
        {
            var existente = await _repository.GetById(command.Id);

            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXator não existente", "FilmeXator não existente");
            }

            return await _repository.Excluir(command.Id);
        }

        public async Task<Retorno> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
