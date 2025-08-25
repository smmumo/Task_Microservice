using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application.Abstractions.Messaging
{
    //https://thecodeman.net/posts/how-to-implement-cqrs-without-mediatr
    /// <summary>
    /// This interface is responsible for 
    /// handling query operations.
    ///  It has a single Handle method 
    /// that takes a query object of type
    ///  TQuery and a cancellation token, and returns 
    /// a Task representing the result of the query operation
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TQueryResult"></typeparam>
    /// 
    public interface IQueryHandler<in TQuery, TResponse> //where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query, CancellationToken cancellation=default);
    }

    // public interface IQueryHandler<in TQuery, TResponse>
    //     where TQuery : IQuery<TResponse>
    // {
    //     Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
    // }
}