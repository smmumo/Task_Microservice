using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application.Abstractions.Messaging
{
    /// <summary>
    /// This interface is responsible for dispatching 
    /// queries to their respective query handlers. 
    /// It has a generic Dispatch method that 
    /// takes a query object and a cancellation token, and 
    /// returns a Task representing the 
    /// result of the dispatched query
    /// </summary>
    public interface IQueryDispatcher
    {
        Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation=default);
    }
    
  
}