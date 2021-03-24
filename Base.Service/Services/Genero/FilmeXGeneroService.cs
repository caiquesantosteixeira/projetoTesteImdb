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

        public async Task<ICommandResult> Persistir(FilmeXGeneroDTO command, ELogin acoes)
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

        private async Task<Retorno> Cadastrar(FilmeXGeneroDTO command)
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

        private async Task<Retorno> Atualizar(FilmeXGeneroDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var FilmeXGenero = new FilmeXgenero
            {
                Id = command.Id,
                IdGenero = command.IdGenero,
                IdFilme = command.IdFilme
            };

            return await _repository.Atualizar(FilmeXGenero);
        }

        private async Task<Retorno> Excluir(FilmeXGeneroDTO command)
        {
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.Excluir(command.Id);
        }

        public async Task<Retorno> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
