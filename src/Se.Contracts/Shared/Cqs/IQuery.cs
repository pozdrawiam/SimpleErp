using MediatR;

namespace Se.Contracts.Shared.Cqs;

public interface IQuery<out TResult> : IRequest<TResult>;
