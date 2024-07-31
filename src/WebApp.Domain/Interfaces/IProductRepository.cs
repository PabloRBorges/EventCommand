using WebApp.Domain.Entities;

namespace WebApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
    }
}
