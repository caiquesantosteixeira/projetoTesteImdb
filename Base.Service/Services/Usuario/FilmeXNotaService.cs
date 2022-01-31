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

        public async Task<Retorno> Cadastrar(FilmeXNotaInsertDTO command)
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

        public async Task<Retorno> Atualizar(FilmeXNotaUpdateDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXNotaDTO não existente", "FilmeXNotaDTO não existente");
            }
            var FilmeXNota = new FilmeXnota
            {
                Id = command.Id,

                IdUsuario = command.IdUsuario,

                IdFilme = command.IdFilme,

                Nota = command.Nota
            };

            return _repository.Atualizar(FilmeXNota);
        }

        public async Task<Retorno> Excluir(FilmeXNotaDTO command)
        {
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "FilmeXNotaDTO não existente", "FilmeXNotaDTO não existente");
            }
            return await _repository.Excluir(command.Id);
        }

        public async Task<Retorno> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
