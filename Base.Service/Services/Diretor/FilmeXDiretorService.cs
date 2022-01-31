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
    public class FilmeXDiretorService:IFilmeXDiretorService
    {
        private readonly IFilmeXDiretor _repository;
        private readonly ILog _log;
        public FilmeXDiretorService(IFilmeXDiretor repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public async Task<Retorno> Cadastrar(FilmeXDiretorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var FilmeXDiretor = new FilmeXdiretor
            {
                IdDiretor = command.IdDiretor,
                IdFilme = command.IdFilme
            };

            return await _repository.Cadastrar(FilmeXDiretor);
        }

        public async Task<Retorno> Atualizar(FilmeXDiretorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXDiretor não existente", "FilmeXDiretor não existente");
            }

            var FilmeXDiretor = new FilmeXdiretor
            {
                Id = command.Id,
                IdDiretor = command.IdDiretor,
                IdFilme = command.IdFilme
            };

            return _repository.Atualizar(FilmeXDiretor);
        }

        public async Task<Retorno> Excluir(FilmeXDiretorDTO command)
        {
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXDiretor não existente", "FilmeXDiretor não existente");
            }

            return await _repository.Excluir(command.Id);
        }

        public async Task<Retorno> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
