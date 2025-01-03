using Se.Application.Base.Database;
using Se.Domain.Features.Products;

namespace Se.Application.Features.Products;

public interface IProductRepo 
    : ICrudRepo<ProductEntity>;
