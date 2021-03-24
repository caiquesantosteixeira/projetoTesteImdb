

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

        public async Task<ICommandResult> Persistir(DiretorDTO command, ELogin acoes)
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



        private async Task<Retorno> Cadastrar(DiretorDTO command)
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

        private async Task<Retorno> Atualizar(DiretorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var Diretor = new Diretor
            {
                Id = command.Id,
                Nome = command.Nome
            };

            return await _repository.Atualizar(Diretor);
        }

        private async Task<Retorno> Excluir(DiretorDTO command)
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
