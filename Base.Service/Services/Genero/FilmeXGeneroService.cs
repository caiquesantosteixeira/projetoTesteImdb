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
    public class FilmeXGeneroService:IFilmeXGeneroService
    {
        private readonly IFilmeXGenero _repository;
        private readonly ILog _log;
        public FilmeXGeneroService(IFilmeXGenero repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public async Task<Retorno> Cadastrar(FilmeXGeneroDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var FilmeXGenero = new FilmeXgenero
            {
                IdGenero = command.IdGenero,

                IdFilme = command.IdFilme
            };

            return await _repository.Cadastrar(FilmeXGenero);
        }

        public async Task<Retorno> Atualizar(FilmeXGeneroDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXGeneroDTO não existente", "FilmeXGeneroDTO não existente");
            }

            var FilmeXGenero = new FilmeXgenero
            {
                Id = command.Id,
                IdGenero = command.IdGenero,
                IdFilme = command.IdFilme
            };

            return _repository.Atualizar(FilmeXGenero);
        }

        public async Task<Retorno> Excluir(FilmeXGeneroDTO command)
        {
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXGeneroDTO não existente", "FilmeXGeneroDTO não existente");
            }
            return await _repository.Excluir(command.Id);
        }

        public async Task<Retorno> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
