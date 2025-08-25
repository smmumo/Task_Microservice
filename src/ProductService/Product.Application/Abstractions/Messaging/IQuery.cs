//using MediatR;

namespace Product.Application.Abstractions.Messaging;

public interface IQuery<TResponse>;
// public interface IQuery<out TResponse>;
//public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull;
