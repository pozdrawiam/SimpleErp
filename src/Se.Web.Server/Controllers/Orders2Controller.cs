using MediatR;
using Se.Contracts.Features.Orders;
using Se.Web.Server.Shared;

namespace Se.Web.Server.Controllers;

public class Orders2Controller : CrudApiController2<
    OrdersQueryAllRequest,
    OrderGetDetailsRequest,
    OrderGetDetailsResponse,
    OrderCreateRequest,
    OrderUpdateRequest,
    OrderDeleteManyRequest
>
{
    public Orders2Controller(IMediator mediator) : base(mediator)
    {
    }
}
