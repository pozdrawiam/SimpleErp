using Se.Contracts.Shared.Crud.QueryAll;

namespace Se.Contracts.Features.Orders;

public class OrdersQueryAllRequest : QueryAllRequest, IQuery<QueryAllResponse>
{
}
