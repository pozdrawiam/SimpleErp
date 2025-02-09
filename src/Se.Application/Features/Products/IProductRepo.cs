using Se.Application.Shared;
using Se.Domain.Features.Products;

namespace Se.Application.Features.Products;

public interface IProductRepo 
    : ICrudRepo<ProductEntity>;
