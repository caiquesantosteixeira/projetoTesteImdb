using Base.Domain.Repositorios.Logging;
using System;
using System.Diagnostics;

namespace Base.Repository.Repositorios.Log
{
    public class LogRepository : ILog
    {
        public void GerarLogDisc(string msg, string pasta = "Log", Exception ex = null)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string versao = fvi.FileVersion;

            Util.Log.Log.FazLog(msg, pasta, versao, ex);
        }
    }
}
