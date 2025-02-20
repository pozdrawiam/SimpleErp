using Se.Application.Shared;
using Se.Domain.Features.Orders;

namespace Se.Application.Features.Orders;

public interface IOrderRepo
    : ICrudRepo<OrderEntity>;
