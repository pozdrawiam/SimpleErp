using MediatR;
using Se.Contracts.Shared.Cqs;

namespace Se.Application.Shared;

public interface ICmdHandler<in TCmd> : IRequestHandler<TCmd, int>
    where TCmd : ICmd;
    
public interface ICmdHandler<in TCmd, TCmdResult> : IRequestHandler<TCmd, TCmdResult>
    where TCmd : ICmd<TCmdResult>;
