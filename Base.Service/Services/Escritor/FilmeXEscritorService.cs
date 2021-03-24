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

        public async Task<ICommandResult> Persistir(FilmeXEscritorDTO command, ELogin acoes)
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



        private async Task<Retorno> Cadastrar(FilmeXEscritorDTO command)
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

        private async Task<Retorno> Atualizar(FilmeXEscritorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var FilmeXEscritor = new FilmeXescritor
            {
                Id = command.Id,
                IdEscritor = command.IdEscritor,
                IdFilme = command.IdFilme
            };

            return await _repository.Atualizar(FilmeXEscritor);
        }

        private async Task<Retorno> Excluir(FilmeXEscritorDTO command)
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
