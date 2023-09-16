using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> LoadProducts();
        public Task<HttpResponseMessage> DeleteProduct(Guid id);
        public Task<Product> GetProduct(Guid id);
        public Task<HttpResponseMessage> AddProduct(Product product);

        public Task<HttpResponseMessage> UpdateProduct(Product product);

    }
}
