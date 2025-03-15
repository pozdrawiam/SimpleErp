using Se.Contracts.Shared.Cqs;
using Se.Contracts.Shared.Crud.GetDetails;

namespace Se.Contracts.Features.Orders;

public record OrderGetDetailsRequest(int Id) : GetDetailsRequest(Id), IQuery<OrderGetDetailsResponse>
{
}
