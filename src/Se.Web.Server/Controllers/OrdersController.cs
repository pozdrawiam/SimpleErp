using MediatR;
using Se.Contracts.Features.Orders;
using Se.Web.Server.Shared;

namespace Se.Web.Server.Controllers;

public class OrdersController : CrudApiController2<
    OrdersQueryAllRequest,
    OrderGetDetailsRequest,
    OrderGetDetailsResponse,
    OrderCreateRequest,
    OrderUpdateRequest,
    OrderDeleteManyRequest
>
{
    public OrdersController(IMediator mediator) : base(mediator)
    {
    }
}
