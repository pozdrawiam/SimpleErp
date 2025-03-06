using MediatR;
using Se.Contracts.Shared.Cqs;

namespace Se.Application.Shared;

public interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
    where TQuery : IQuery<TQueryResult>;
