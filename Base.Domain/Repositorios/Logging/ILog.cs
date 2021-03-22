using System;

namespace Base.Domain.Repositorios.Logging
{
    public interface ILog
    {
        void GerarLogDisc(string msg, string pasta = "Log", Exception ex = null);
    }
}
