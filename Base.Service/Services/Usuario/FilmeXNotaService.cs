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
    public class FilmeXNotaService:IFilmeXNotaService
    {
        private readonly IFilmeXNota _repository;
        private readonly ILog _log;
        public FilmeXNotaService(IFilmeXNota repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public async Task<ICommandResult> Persistir(FilmeXNotaDTO command, ELogin acoes)
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

        private async Task<Retorno> Cadastrar(FilmeXNotaDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var FilmeXNota = new FilmeXnota
            {
                IdUsuario = command.IdUsuario,

                IdFilme = command.IdFilme,

                 Nota = command.Nota
            };

            return await _repository.Cadastrar(FilmeXNota);
        }

        private async Task<Retorno> Atualizar(FilmeXNotaDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var FilmeXNota = new FilmeXnota
            {
                Id = command.Id,

                IdUsuario = command.IdUsuario,

                IdFilme = command.IdFilme,

                Nota = command.Nota
            };

            return await _repository.Atualizar(FilmeXNota);
        }

        private async Task<Retorno> Excluir(FilmeXNotaDTO command)
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
