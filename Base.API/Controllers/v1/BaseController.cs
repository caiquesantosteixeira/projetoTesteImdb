using Base.Domain.Repositorios.Logging;
using Base.Infra.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Base.API.Controllers.v1
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {     
        protected void GerarLog(string msg, string pasta="Log", Exception ex = null)
        {
             var _log = LocalizarService.Get<ILog>("ILog");
            _log.GerarLogDisc(msg, pasta, ex);
            // Log.FazLog(msg, pasta, versao, ex);
        }
    }
}
