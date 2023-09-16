using Core.Domain.Entities;
using Core.Domain.ValueTypes;

namespace Core.Application.Repository
{
	public interface IProductRepository
	{
		IEnumerable<Product> GetAll(Product? searchProducts = null, bool OrderByDesc = false, PagerInfo? pagerInfo = null);
		KeyValuePair<bool, string> Add(Product product);
		KeyValuePair<bool, string> Delete(Product product);
		KeyValuePair<bool, string> Update(Product product);

	}
}
