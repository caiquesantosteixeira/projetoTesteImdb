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

        public async Task<Retorno> Cadastrar(AtorInsertDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var ator = new Ator
            {
                Nome = command.Nome
            };
            var cadastrado = await _repository.Cadastrar(ator);

            if (cadastrado.Sucesso)
            {
                var cadastradoConvertido = (Ator)cadastrado.Data;

                var ret = new AtorDTO
                {
                    Id = cadastradoConvertido.Id,
                    Nome = cadastradoConvertido.Nome
                };
                return new Retorno(true, "Cadastrado com sucesso.", ret);
            }
            else {
                return cadastrado;
            }
        }

        public async Task<Retorno> Atualizar(AtorUpdateDTO command)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);


            var existente = await _repository.GetById(command.Id);

            if (existente.Data == null) 
            {
                return new Retorno(false, "Ator não existente", "Ator não existente");
            }

            var Ator = new Ator
            {
                Id = command.Id,
                Nome = command.Nome
            };

            var atualizado = _repository.Atualizar(Ator);

            if (atualizado.Sucesso)
            {
                var atualizadoConvertido = (Ator)atualizado.Data;

                var ret = new AtorDTO
                {
                    Id = atualizadoConvertido.Id,
                    Nome = atualizadoConvertido.Nome
                };

                return new Retorno(true, "Atualizado com sucesso.", ret);
            }
            else {
                return atualizado;
            }
        }

        public async Task<Retorno> Excluir(AtorUpdateDTO command)
        {
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);
            var existente = await _repository.GetById(command.Id);
            if (existente.Data == null)
            {
                return new Retorno(false, "Ator não existente", "Ator não existente");
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
