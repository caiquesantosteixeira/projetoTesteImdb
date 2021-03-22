using Base.Domain.Commands.Interfaces;

namespace Base.Domain.Retornos
{
    public class Retorno: ICommandResult
    {
        public Retorno()
        {

        }
        public Retorno(bool sucesso, string mensagem, object data)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Data = TipoMensagem(data);
        }

        public bool Sucesso { get; private set; }
        public string Mensagem { get; private set; }
        public object Data { get; private set; }

        private object TipoMensagem(object data)
        {
            if(Sucesso == true)
            {
                return data;
            }
            else
            {
                return new { erros = data};
            }
        }

    }

    
}
