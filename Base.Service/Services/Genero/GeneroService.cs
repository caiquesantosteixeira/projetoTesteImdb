

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

        public async Task<ICommandResult> Persistir(GeneroDTO command, ELogin acoes)
        {
            var retorno = new Retorno();
            switch (acoes)
            {
                case ELogin.CADASTRAR:
                    retorno = await Cadastrar(command);
                    break;
                case ELogin.ATUALIZAR:
                    retorno = await Atualizar(command);
                    break;
                case ELogin.EXCLUIR:
                    retorno = await Excluir(command);
                    break;
            }

            return retorno;
        }



        private async Task<Retorno> Cadastrar(GeneroDTO command)
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

        private async Task<Retorno> Atualizar(GeneroDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var Genero = new Genero
            {
                Id = command.Id,
                Nome = command.Nome
            };

            return await _repository.Atualizar(Genero);
        }

        private async Task<Retorno> Excluir(GeneroDTO command)
        {
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.Excluir(command.Id);
        }

        public async Task<Retorno> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Retorno> Get(string id)
        {
            var retorno = await _repository.GetById(id);
            return retorno;
        }
    }
}
