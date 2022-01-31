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
    public class FilmeXEscritorService:IFilmeXEscritorService
    {
        private readonly IFilmeXEscritor _repository;
        private readonly ILog _log;
        public FilmeXEscritorService(IFilmeXEscritor repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public async Task<Retorno> Cadastrar(FilmeXEscritorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var FilmeXEscritor = new FilmeXescritor
            {
                IdEscritor = command.IdEscritor,

                IdFilme = command.IdFilme
            };

            return await _repository.Cadastrar(FilmeXEscritor);
        }

        public async Task<Retorno> Atualizar(FilmeXEscritorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXEscritor não existente", "FilmeXEscritor não existente");
            }

            var FilmeXEscritor = new FilmeXescritor
            {
                Id = command.Id,
                IdEscritor = command.IdEscritor,
                IdFilme = command.IdFilme
            };

            return _repository.Atualizar(FilmeXEscritor);
        }

        public async Task<Retorno> Excluir(FilmeXEscritorDTO command)
        {
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXEscritor não existente", "FilmeXEscritor não existente");
            }
            return await _repository.Excluir(command.Id);
        }

        public async Task<Retorno> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
