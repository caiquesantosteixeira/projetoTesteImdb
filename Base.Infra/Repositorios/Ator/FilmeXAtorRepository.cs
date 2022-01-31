using Base.Domain.DTOs;
using Base.Domain.Entidades;
using Base.Domain.Repositorios.Logging;
using Base.Domain.Retornos;
using Base.Repository.Contracts;
using Base.Repository.Helpers.Paginacao;
using Base.Rpepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Repository.Repositorios.Usuario
{
    public class FilmeXAtorRepository: BaseRepository<FilmeXator>, IFilmeXAtor
    {

        private readonly testeimdbContext _ctx;
        private readonly ILog _log;
        private readonly IUserIdentity _userIdentity;
        public FilmeXAtorRepository(testeimdbContext context, ILog log, IUserIdentity userIdentity) : base(context, log, userIdentity)
        {
            _ctx = context;
            _log = log;
            _userIdentity = userIdentity;
        }
    }
}
