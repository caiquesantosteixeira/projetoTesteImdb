

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
    public class GeneroService:IGeneroService
    {
        private readonly IGenero _repository;
        private readonly ILog _log;
        public GeneroService(IGenero repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public async Task<Retorno> Cadastrar(GeneroInsertDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var Genero = new Genero
            {
                Nome = command.Nome
            };

            return await _repository.Cadastrar(Genero);
        }

        public async Task<Retorno> Atualizar(GeneroUpdateDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "Genero não existente", "Genero não existente");
            }

            var Genero = new Genero
            {
                Id = command.Id,
                Nome = command.Nome
            };

            return _repository.Atualizar(Genero);
        }

        public async Task<Retorno> Excluir(GeneroDTO command)
        {
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "Genero não existente", "Genero não existente");
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
    }
}
