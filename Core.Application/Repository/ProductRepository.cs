using Core.Domain.Entities;
using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Core.Domain.ValueTypes;
using Infrastracture.Data;
using Microsoft.Extensions.Configuration;

namespace Core.Application.Repository
{
	public class ProductRepository : IProductRepository
	{
		IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public ProductRepository(IConfiguration configuration, AppDbContext appDbContext)
		{
			_configuration = configuration;
			_appDbContext = appDbContext;
		}

		public KeyValuePair<bool, string> Update(Product product)
		{
			_appDbContext.Products.Update(product);
			var updated = _appDbContext.SaveChanges();
			return new KeyValuePair<bool, string>(updated == 1, updated == 1 ? "Updated Successfully!" : "An Error Occurred.");
		}

		public KeyValuePair<bool, string> Add(Product product)
		{
            _appDbContext.Products.Add(product);
            var inserted = _appDbContext.SaveChanges();
            return new KeyValuePair<bool, string>(inserted == 1, inserted == 1 ? "Added Successfully!" : "An Error Occurred.");
		}

		public KeyValuePair<bool, string> Delete(Product product)
		{
            _appDbContext.Products.Remove(product);
            var deleted = _appDbContext.SaveChanges();
            return new KeyValuePair<bool, string>(deleted == 1, deleted == 1 ? "Deleted Successfully!" : "An Error Occurred.");
        }

		public IEnumerable<Product> GetAll(Product? searchProducts = null, bool OrderByDesc = false, PagerInfo? pagerInfo = null)
		{
			
			var productsQuery = _appDbContext.Products.AsQueryable();
            if (searchProducts is not null)
			{
				productsQuery = productsQuery.Where(x => (x.Id == searchProducts.Id || searchProducts.Id == default) && 
				(x.Name == searchProducts.Name || string.IsNullOrWhiteSpace(searchProducts.Name)) && 
				(x.Price == searchProducts.Price || searchProducts.Price == 0));
			}

			if (pagerInfo != null)
			{
                productsQuery = productsQuery.Skip(pagerInfo.PageSize * (pagerInfo.PageCount - 1));
                productsQuery = productsQuery.Take(pagerInfo.PageSize);
            }
			//var products = _appDbContext.Products.FromSql($"SELECT * FROM products");
			var products = productsQuery.AsEnumerable();
			return products;
		}
	}
}
