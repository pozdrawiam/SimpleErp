using MediatR;

namespace Se.Contracts.Shared.Cqs;

public interface ICmd : IRequest<int>;

public interface ICmd<out TCmdResult> : IRequest<TCmdResult>;
