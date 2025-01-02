using Se.Application.Base.Database;

namespace Se.Application.Features.Products;

public interface IProductRepo 
    : ICrudRepo<ProductEntity, int>;
