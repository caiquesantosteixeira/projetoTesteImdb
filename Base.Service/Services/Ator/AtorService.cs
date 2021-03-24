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
    public class AtorService:IAtorService
    {
        private readonly IAtor _repository;
        private readonly ILog _log;
        public AtorService(IAtor repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public async Task<ICommandResult> Persistir(AtorDTO command, ELogin acoes)
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



        private async Task<Retorno> Cadastrar(AtorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var Ator = new Ator
            {
                Nome = command.Nome
            };

            return await _repository.Cadastrar(Ator);
        }

        private async Task<Retorno> Atualizar(AtorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var Ator = new Ator
            {
                Id = command.Id,
                Nome = command.Nome
            };

            return await _repository.Atualizar(Ator);
        }

        private async Task<Retorno> Excluir(AtorDTO command)
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
