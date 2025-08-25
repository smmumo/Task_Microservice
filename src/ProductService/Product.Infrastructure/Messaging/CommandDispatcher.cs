using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Product.Application.Abstractions.Messaging;

namespace Infrastructure.Messaging
{
    //https://thecodeman.net/posts/how-to-implement-cqrs-without-mediatr
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        /// <summary>
        /// the appropriate ICommandHandler<TCommand, TCommandResult> is obtained from the _serviceProvider field using 
        /// the GetRequiredService method, which returns a new instance of the service
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TCommandResult"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>      

        public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
            return handler.Handle(command, cancellation);
        }


    }
}