using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Abstractions.Messaging
{
    /// <summary>
    /// This interface is responsible for handling command operations.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TCommandResult"></typeparam>
    public interface ICommandHandler<in TCommand, TCommandResult> //where TCommand : ICommand<TCommandResult>
    {
        Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation = default);
    }
    
    public interface ICommandHandler<in TCommand>  //where TCommand : ICommand
    {
        Task Handle(TCommand command, CancellationToken cancellation=default);
    }

    // public interface ICommandHandler<in TCommand> where TCommand : ICommand
    // {
    //     Task Handle(TCommand command, CancellationToken cancellationToken = default);
    // }
}