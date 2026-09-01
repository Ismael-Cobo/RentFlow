using RentFlow.Common.Domain;
using MediatR;

namespace RentFlow.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
