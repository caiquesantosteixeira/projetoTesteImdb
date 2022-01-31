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
    public class EscritorService:IEscritorService
    {
        private readonly IEscritor _repository;
        private readonly ILog _log;
        public EscritorService(IEscritor repository,ILog log)
        {
            _repository = repository;
            _log = log;
        }
        public async Task<Retorno> Cadastrar(EscritorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var escritor = new Escritor
            {
                Nome = command.Nome
            };

            return await _repository.Cadastrar(escritor);
        }

        public async Task<Retorno> Atualizar(EscritorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "Escritor não existente", "Escritor não existente");
            }
            var escritor = new Escritor
            {
                Id = command.Id,
                Nome = command.Nome
            };

            return _repository.Atualizar(escritor);
        }

        public async Task<Retorno> Excluir(EscritorDTO command)
        {
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "Escritor não existente", "Escritor não existente");
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
