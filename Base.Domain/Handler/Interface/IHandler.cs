using Base.Domain.Commands.Interfaces;
using System.Threading.Tasks;

namespace Base.Domain.Handler.Interface
{
    interface IHandler<T,A> where T: ICommand
    {
        Task<ICommandResult> Handle(T command, A acoes);
    }
}
