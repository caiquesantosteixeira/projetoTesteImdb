using Flunt.Notifications;


using Base.Domain.Retornos;
using System.Threading.Tasks;
using Base.Domain.DTOS.Interfaces;
using Base.Repository.Repositorios.Usuario;
using Base.Domain.DTOS.Usuario;
using Base.Domain.Enums.Usuario.Enums;
using Base.Service.Contracts.Usuario;
using Base.Domain.DTOs.Usuario;
using System;
using Microsoft.AspNetCore.Identity;
using Base.Domain.Shared.Entidades.Usuario;
using Base.Domain.ValueObject.Config;
using Base.Domain.Repositorios.Logging;
using Microsoft.Extensions.Options;

namespace Base.Service.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuario _repository;
        private readonly SignInManager<Usuarios> _signInManager;
        private readonly UserManager<Usuarios> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ILog _log;
        public UsuarioService(IUsuario repository,
              SignInManager<Usuarios> signInManager,
            UserManager<Usuarios> userManager,
            IOptions<AppSettings> appSettings,
            ILog log)
        {
            _repository = repository;

            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _log = log;
        }
        
        public async Task<ICommandResult> Persistir(UsuarioDTO command, ELogin acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

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

        public async Task<ICommandResult> AlterarSenha(AlterarSenhaDTO command, ELogin acoes)
        {
            command.Validate();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.AlterarSenha(command);
        }

        private async Task<Retorno> Cadastrar(UsuarioDTO command)
        {
            command.ValidateCadastrar();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.Cadastrar(command);
        }

        private async Task<Retorno> Atualizar(UsuarioDTO command)
        {
            command.ValidAtualizarExcluir();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.Atualizar(command);

        }

        private async Task<Retorno> Excluir(UsuarioDTO command)
        {
            command.ValidAtualizarExcluir();
            if (command.Invalid)
                return new Retorno(false, "Dados Inválidos!", command.Notifications);

            return await _repository.Excluir(command.Id);
        }

        //filtro por NOME
        public async Task<Retorno> GetAll(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null)
        {
            return  await _repository.DadosPaginado( QtdPorPagina,  PagAtual,  Filtro , ValueFiltro );
        }

        public async Task<Retorno> Logar(LoginDTO login)
        {
            try
            {
                var isLogado = await _signInManager.PasswordSignInAsync(login.Usuario, login.Senha, false, true);
                if (!isLogado.Succeeded)
                    return new Retorno(false, "Não Logado", "Usuário ou Senha Invalido!");

                var user = await _userManager.FindByNameAsync(login.Usuario);

                var userToken = new TokenUsuario { Id = user.Id, Usuario = user.UserName, Email = user.Email, IdPerfil = user.IdPerfil, Administrador = (user.Administrador ?? false) };

                var userLogado = new UsuarioLogadoDTO { UserToken = userToken };
                var dadosToken = await _repository.GerarJwtAsync(userLogado);


                return new Retorno(true, "Login Efetuado com Sucesso!", dadosToken);
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Logar Usuario", ex: ex);
                throw new Exception("Erro ao Logar o Usuario", ex);
            }
        }

        public async Task<Retorno> Get(string id)
        {
                var retorno = await _repository.GetById(id);
                return retorno;
        }
    }
}
