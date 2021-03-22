using Flunt.Notifications;
using Base.Domain.Commands.Interfaces;
using Base.Domain.Commands.Usuario;
using Base.Domain.Commands.Usuario.Enums;
using Base.Domain.Entidades.Menu;
using Base.Domain.Handler.Interface;
using Base.Domain.Repositorios.Menu;
using Base.Domain.Retornos;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Base.Domain.Handler.Menu
{
    public class PerfilHandler : Notifiable,
        IHandler<PerfilCommand, EPerfil>
    {
        private readonly IPerfil _repository;
        private readonly IPerfilUsuario _perfilUsuarioRepo;
        private readonly IPerfilUsuarioBotoes _perfilUsuarioBotoesRepo;
        public PerfilHandler(IPerfil repository, IPerfilUsuario perfilUsuario, IPerfilUsuarioBotoes perfilUsuarioBotoes)
        {
            _repository = repository;
            _perfilUsuarioRepo = perfilUsuario;
            _perfilUsuarioBotoesRepo = perfilUsuarioBotoes;
        }

        public async Task<ICommandResult> Handle(PerfilCommand command, EPerfil acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var retorno = new Retorno();
            switch (acoes)
            {
                case EPerfil.CADASTRAR:
                    retorno = await Adicionar(command);
                    break;
                case EPerfil.ATUALIZAR:
                    retorno = await Atualizar(command);
                    break;
                case EPerfil.DUPLICARPERFIL:
                    retorno = await DuplicarPerfil(command);
                    break;               
            }

            return retorno;
        }

        private async Task<Retorno> Adicionar(PerfilCommand command)
        {
            var obj = new Perfil();
            obj.Id = command.Id;
            obj.Nome = command.Nome;

            var perfil = await _repository.Cadastrar(obj);
            return  new Retorno (true, "Perfil Cadastrado com Sucesso.", perfil);
        }

        private async Task<Retorno> Atualizar(PerfilCommand command)
        {
            command.ValidateUpdate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var ExistPerfil = await _repository.PerfilById(command.Id);
            if (ExistPerfil == null)
            {
                return new Retorno (false, "Não autorizado!", "Não foi encontrado um perfil para ser atualizado");
            }

            ExistPerfil.Id = command.Id;
            ExistPerfil.Nome = command.Nome;

            var perfil =  await _repository.Atualizar(ExistPerfil);
            return new Retorno (true, "Perfil Atualizado com Sucesso.", perfil);
        }

        private async Task<Retorno> DuplicarPerfil(PerfilCommand command)
        {
            command.ValidateUpdate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            var perfilClonado = await _repository.PerfilById(command.Id);
            if (perfilClonado == null)
            {
                return new Retorno(false, "Não autorizado!", "Não foi encontrado um perfil para ser duplicado");
            }

            var perfilSplit = perfilClonado.Nome.Split("_");

            var perfil = new Perfil();
            perfil.Nome = $"{perfilSplit[0]?.Trim()}_{Guid.NewGuid().ToString().Replace("-","").Substring(0,5)}";            

            // cadastra o novo perfil
            var perfilCad = await _repository.Cadastrar(perfil);           

            // Consulta os dados da tabela perfil_usuario
            var perfilUsuClone = await _perfilUsuarioRepo.perfilUsuarioById(perfilClonado.Id);
            if(perfilUsuClone.Count() > 0)
            {
                // inicializa uma lista de permissoes
                List<PerfilUsuarioBotoes> _perfisUsuBotoes = new List<PerfilUsuarioBotoes>();
                
                foreach (var item in perfilUsuClone)
                {
                    var perfilUsuario = new PerfilUsuario();
                    perfilUsuario.IdModuloMenuOpc = item.IdModuloMenuOpc;
                    perfilUsuario.IdPerfil = perfilCad.Id;

                    // insere os dados da tabela perfil Usuario
                    var perfilUsuarioCad = await _perfilUsuarioRepo.Cadastrar(perfilUsuario);

                    // Consulta as permissoes
                    var perfilUsuaBotoesClone = await _perfilUsuarioBotoesRepo.GetAllPermissaoPerfil(item.Id);
                    if(perfilUsuaBotoesClone.Count() > 0)
                    {
                        
                        foreach (var botao in perfilUsuaBotoesClone)
                        {
                            var perfilUsuarioBotoes = new PerfilUsuarioBotoes();
                            perfilUsuarioBotoes.IdPermissoes = botao.IdPermissoes;
                            perfilUsuarioBotoes.IdPerfilUsuario = perfilUsuarioCad.Id;
                            _perfisUsuBotoes.Add(perfilUsuarioBotoes);
                        }                      
                       
                    }
                }

                // persiste a lista de permissoes
                if (_perfisUsuBotoes.Count > 0)
                {
                    await _perfilUsuarioBotoesRepo.Cadastrar(_perfisUsuBotoes);
                }
            }

            // Zera a loop da lista
            perfilCad.PerfilUsuario = null;
            return new Retorno(true, "Perfil Duplicado com Sucesso.", perfilCad);
        }

    }
}
