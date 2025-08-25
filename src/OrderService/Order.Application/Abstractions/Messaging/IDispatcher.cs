using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// namespace Application.Abstractions.Messaging
// {
   
//         public interface IDispatcher
//         {
//             Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
//                 where TRequest : ICommandHandler<TResponse>;
//         }
    
// }

namespace Application.Abstractions.Messaging;
public interface IDispatcher
{
    Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : ICommand<TResponse>;
}