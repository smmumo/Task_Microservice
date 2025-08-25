using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Abstractions.Messaging
{
    /// <summary>
    /// This interface is responsible for dispatching
    ///  commands to their respective command handlers
    /// </summary>
    public interface ICommandDispatcher 
    {
        Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation=default);
    }
}