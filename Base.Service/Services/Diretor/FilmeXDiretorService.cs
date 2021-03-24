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

        public async Task<ICommandResult> Persistir(FilmeXDiretorDTO command, ELogin acoes)
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



        private async Task<Retorno> Cadastrar(FilmeXDiretorDTO command)
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

        private async Task<Retorno> Atualizar(FilmeXDiretorDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var FilmeXDiretor = new FilmeXdiretor
            {
                Id = command.Id,
                IdDiretor = command.IdDiretor,
                IdFilme = command.IdFilme
            };

            return await _repository.Atualizar(FilmeXDiretor);
        }

        private async Task<Retorno> Excluir(FilmeXDiretorDTO command)
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
