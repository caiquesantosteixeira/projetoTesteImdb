using Base.Domain.DTOs.Usuario;
using Base.Domain.DTOS.Usuario;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Retornos;
using Base.Domain.Shared.DTOs.Usuario;
using Base.Domain.Shared.Entidades.Usuario;
using Base.Domain.ValueObject.Basicos;
using Base.Domain.ValueObject.Config;
using Base.Repository.Context;
using Base.Repository.Helpers.Paginacao;
using Base.Rpepository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Base.Repository.Repositorios.Usuario
{
    public class LoginRepository : IUsuario
    {    
        private readonly SignInManager<Usuarios> _signInManager;
        private readonly UserManager<Usuarios> _userManager;
        private readonly testeimdbContext _ctx;
        private readonly AppSettings _appSettings; 
        private readonly ILog _log;

        public LoginRepository(
            SignInManager<Usuarios> signInManager,
            UserManager<Usuarios> userManager,
            testeimdbContext context,
            IOptions<AppSettings> appSettings,
            ILog log
            )
        {            
            _signInManager = signInManager;
            _userManager = userManager;
            _ctx = context;
            _appSettings = appSettings.Value;
            _log = log;
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                var lista = await _ctx.Usuarios.Select(a => new UsersDTO
                {
                    NomeDoUsuario = a.Nome,
                    Id = a.Id,
                    Login = a.UserName,
                    IdPerfil = a.IdPerfil,
                    Ativo = (a.Ativo?? false),
                    Email = a.Email,
                    Senha = ""
                }).AsNoTracking().ToListAsync();
                return new Retorno (true, "Perfil Cadastrado com Sucesso.", lista);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar Login", ex:ex);
                throw new Exception("Erro ao Consultar Login", ex);
            }
        }

        public async Task<Retorno> DadosPaginado(int QtdPorPagina, int PagAtual, string Filtro = null, string ValueFiltro = null)
        {
            try
            {
                var lista = _ctx.Usuarios.Select(a => new UsersDTO
                {
                    Id = a.Id,
                    Login = a.UserName,
                    NomeDoUsuario = a.Nome,
                    Email = a.Email,                   
                    IdPerfil = a.IdPerfil
                });

                //filtro
                if (Filtro != null && ValueFiltro != null)
                {
                    Filtro = Filtro.Trim().ToUpper();

                    switch (Filtro)
                    {
                        case "CODIGO":
                            lista = lista.Where(a => a.Id == ValueFiltro);
                            break;
                        case "NOME":
                            lista = lista.Where(a => 
                                                    a.Login.Contains(ValueFiltro) || 
                                                    a.NomeDoUsuario.Contains(ValueFiltro) ||
                                                    a.Id.Contains(ValueFiltro)
                                                    )
                                         .OrderBy(a => a.Login);
                            break;
                    }
                }
                else
                {

                    lista = lista
                            .OrderBy(a => a.Id);
                }

                var dados = Paginacao.GetPage<Usuarios, UsersDTO>(_ctx.Usuarios, lista, QtdPorPagina, PagAtual);
                return new Retorno (true, "Dados Paginados", dados);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Paginar Usuarios", ex: ex);
                throw new Exception("Erro ao Paginar Usuarios", ex);
            }
        }

        public async Task<Retorno> GetById(string id)
        {
            try
            {

                var usuario = await _ctx.Usuarios.Select(a => new UsuarioDTO
                {
                    Id = a.Id,
                    UserName = a.UserName,
                    Senha = a.PasswordHash,
                    Nome = a.Nome,
                    Ativo = (a.Ativo??false),
                    Email = a.Email,
                    IdPerfil = a.IdPerfil
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

                return new Retorno (true, "Usuario", usuario);

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Consultar Usuario", ex: ex);
                throw new Exception("Erro ao Consultar Usuario", ex);
            }
        }

        public async Task<Retorno> Cadastrar(UsuarioDTO command)
        {
            try
            {
                var usuExist = await GetUsuarioByUserName(command.UserName);
                if(usuExist != null)
                    return new Retorno (false, "Usuario já existe", "Usuario já existe");

                usuExist = await GetUsuarioByEmail(command.Email);
                if (usuExist != null)
                    return new Retorno (false, "Email já existe", "Email já existe");

                var user = new Usuarios
                {
                    UserName = command.UserName,
                    Email = command.Email,
                    EmailConfirmed = true,
                    IdPerfil = command.IdPerfil,
                    Ativo = command.Ativo,
                    Nome = command.Nome
                };

                var result = await _userManager.CreateAsync(user, command.Senha);

                if (!result.Succeeded)
                {
                    return new Retorno (false, "Não Cadastrado", result.Errors);
                }
                else
                {
                    await _signInManager.SignInAsync(user, false);
                    var usuario = await _userManager.FindByNameAsync(command.UserName);

                    return new Retorno (true, "Cadastro efetuado com sucesso", usuario.GetDTO());
                }

            }
            catch(Exception ex)
            {
                _log.GerarLogDisc("Erro ao Cadastrar o Usuario", ex: ex);
                throw new Exception("Erro ao Cadastrar o Usuario", ex);
            }            
        }

        public async Task<Retorno> Atualizar(UsuarioDTO command)
        {
            try
            {
                var usuario = await GetUsuarioById(command.Id);
                if (usuario == null)
                    return new Retorno (false, "Usuario não encontrado", "Usuario não encontrado");

                if (usuario.Id != command.Id)
                    return new Retorno (false, "Usuario já existe", "Usuario já existe");

                usuario = await GetUsuarioByEmail(command.Email);
                if (usuario.Id != command.Id)
                    return new Retorno (false, "E-mail já existe", "E-mail já existe");


                usuario.UserName = command.UserName;
                usuario.NormalizedUserName = command.UserName.ToUpper();
                usuario.Email = command.Email;
                usuario.NormalizedEmail = command.Email.ToUpper();
                usuario.IdPerfil = command.IdPerfil;
                usuario.Ativo = command.Ativo;
                usuario.Nome = command.Nome;               

                _ctx.Usuarios.Update(usuario);
                _ctx.SaveChanges();

                // Atualiza a Senha
                if (command.Senha.Length < 50)
                {
                    var email = new Email(command.Email);
                    if(email.Invalid)
                        return new Retorno(false, "Email Inválido.", "Email Inválido.");

                    var retorno = await AlterarSenha(email, command.Senha);
                    if (!retorno.Sucesso)
                        return retorno;
                }


                return new Retorno (true, "Dados Atualizados Com Sucesso.", "Dados Atualizados Com Sucesso.");

                }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Atualizar o Usuario", ex: ex);
                throw new Exception("Erro ao Atualizar o Usuario", ex);
            }
        }

        public async Task<Retorno> AlterarSenha(AlterarSenhaDTO command)
        {
            try
            {
                var User = await _userManager.FindByNameAsync(command.UserName);
                var usuario = await _signInManager.PasswordSignInAsync(User, command.SenhaAtual, false, true);
                if (!usuario.Succeeded)
                    return new Retorno (false, "Senha atual não confere.", "Senha atual não confere.");

                var token = await _userManager.GeneratePasswordResetTokenAsync(User);
                var result = await _userManager.ResetPasswordAsync(User, token, command.Password);
                if (!result.Succeeded)
                    return new Retorno (false, "Não foi possivel atualizar a senha. Tente novamente.", "Não foi possivel atualizar a senha. Tente novamente.");
                
                return new Retorno (true, "Senha Atualizados Com Sucesso.", "Senha Atualizados Com Sucesso.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Atualizar a senha do Usuario", ex: ex);
                throw new Exception("Erro ao Atualizar a senha do Usuario", ex);
            }
        }

        public async Task<Retorno> AlterarSenha(Email email, string novaSenha)
        {
            try
            {                
                var user = await _userManager.FindByEmailAsync(email.Endereco);
                if (user == null)
                    return new Retorno(false, "Email não encontrado.", "Email não encontrado.");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, novaSenha);
                if (!result.Succeeded)
                    return new Retorno(false, "Não foi possivel Alterar a senha.", "Não foi possivel Alterar a senha.");             

                return new Retorno(true, "Senha Atualizados Com Sucesso.", "Senha Atualizados Com Sucesso.");

            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Atualizar a senha do Usuario", ex: ex);
                throw new Exception("Erro ao Atualizar a senha do Usuario", ex);
            }
        }

        public async Task<Retorno> Excluir(string id)
        {
            try
            {              
                var usuario = await _ctx.Usuarios.FirstOrDefaultAsync(a => a.Id.Equals(id));
                if (usuario == null)
                {
                    return new Retorno (false, "Não Autorizado", "Usuário não encontrado.");
                }

                if(usuario.Administrador == true)
                {
                    return new Retorno(false, "Não Autorizado", "Não é permitido excluir usuário administrador.");
                }

                usuario.Ativo = false;
                _ctx.Usuarios.Update(usuario);
                _ctx.SaveChanges();
                return new Retorno (true, "Usuário Excluido.", "Usuário Excluido.");

                }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro ao Excluir Usuario", ex: ex);
                throw new Exception("Erro ao Excluir Usuario", ex);
            }
        }

        public async Task<Retorno> Logar(LoginDTO login)
        {
            try
            {                
                var isLogado = await _signInManager.PasswordSignInAsync(login.Usuario, login.Senha, false, true);
                if (!isLogado.Succeeded)
                    return new Retorno (false, "Não Logado", "Usuário ou Senha Invalido!");                

                var user = await _userManager.FindByNameAsync(login.Usuario);

                var userToken = new TokenUsuario { Id = user.Id, Usuario = user.UserName, Email = user.Email, IdPerfil = user.IdPerfil, Administrador = (user.Administrador??false) };

                var userLogado = new UsuarioLogadoDTO { UserToken = userToken };               
                var dadosToken = await GerarJwtAsync(userLogado);


                return new Retorno (true, "Login Efetuado com Sucesso!", dadosToken);
            }catch(Exception ex)
            {
                _log.GerarLogDisc("Erro ao Logar Usuario", ex: ex);                
                throw new Exception("Erro ao Logar o Usuario", ex);
            }         
        }
        public async Task<UsuarioLogadoDTO> GerarJwtAsync(UsuarioLogadoDTO login)
        {
            try
            {
                //var permissoes = await _ctx.ViewPerfilUsuario.AsNoTracking().Where(a => a.IdPerfil == 1).ToListAsync();

                List<Claim> claims = new List<Claim>();
                // claims.Add(new Claim(JwtRegisteredClaimNames.Sub, login.UserToken.Id));
                claims.Add(new Claim("idUsuario", login.UserToken.Id));
                claims.Add(new Claim("administrador", login.UserToken.Administrador.ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, login.UserToken.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.NameId, login.UserToken.Usuario));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

                //List<Claim> permClaim = new List<Claim>();
                //foreach (var item in permissoes)
                //{
                //    if (!string.IsNullOrEmpty(item.Permissao))
                //    {
                //        claims.Add(new Claim("Roles", item.Permissao));
                //    }
                //}

                var identityClaims = new ClaimsIdentity();
                identityClaims.AddClaims(claims);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _appSettings.Emissor,
                    Audience = _appSettings.ValidoEm,
                    Subject = identityClaims,
                    Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var encodedtoken = tokenHandler.WriteToken(token);

                login.AccesToken = encodedtoken;
                login.ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds;
                // login.UserToken.Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value });
                return login;
            }catch(Exception ex)
            {
                _log.GerarLogDisc("Erro ao Gerar Claims Usuario", ex: ex);
                throw new Exception("Erro",ex);
            }
        }
        
        private async Task<Usuarios> GetUsuarioByUserName(string username)
        {
            try
            {
                return await _ctx.Usuarios.FirstOrDefaultAsync(x => x.NormalizedUserName.Equals(username.ToUpper()));                 
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro", ex);
            }            
        }

        private async Task<Usuarios> GetUsuarioById(string id)
        {
            try
            {
                return await _ctx.Usuarios.FirstOrDefaultAsync(x => x.Id.Equals(id));
            }catch(Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro", ex);
            }            
        }

        private async Task<Usuarios> GetUsuarioByEmail(string email)
        {
            try
            {
                return await _ctx.Usuarios.FirstOrDefaultAsync(x => x.NormalizedEmail.Equals(email.ToUpper()));
            }
            catch (Exception ex)
            {
                _log.GerarLogDisc("Erro", ex: ex);
                throw new Exception("Erro", ex);
            }            
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
