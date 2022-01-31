

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
    public class DiretorService:IDiretorService
    {
        private readonly IDiretor _repository;
        private readonly ILog _log;
        public DiretorService(IDiretor repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public async Task<Retorno> Cadastrar(DiretorInsertDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var Diretor = new Diretor
            {
                Nome = command.Nome
            };

            return await _repository.Cadastrar(Diretor);
        }

        public async Task<Retorno> Atualizar(DiretorUpdateDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var existente = await _repository.GetById(command.Id);

            if (existente.Data == null)
            {
                return new Retorno(false, "Diretor não existente", "Diretor não existente");
            }

            var Diretor = new Diretor
            {
                Id = command.Id,
                Nome = command.Nome
            };

            return _repository.Atualizar(Diretor);
        }

        public async Task<Retorno> Excluir(DiretorDTO command)
        {
            var existente = await _repository.GetById(command.Id);

            if (existente.Data == null)
            {
                return new Retorno(false, "Diretor não existente", "Diretor não existente");
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
