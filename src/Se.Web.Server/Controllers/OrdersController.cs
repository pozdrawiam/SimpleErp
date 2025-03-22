using MediatR;
using Se.Contracts.Features.Orders;

namespace Se.Web.Server.Controllers;

public class OrdersController : CrudCqsController<
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
