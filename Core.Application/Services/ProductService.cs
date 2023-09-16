using System;
using System.Text.Json;
using Core.Domain.Entities;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration configuration;
        private readonly System.Net.Http.IHttpClientFactory _httpClientFactory;

        public ProductService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<Product>> LoadProducts()
        {
            IEnumerable<Product> products = Array.Empty<Product>();
            var httpClient = _httpClientFactory.CreateClient("WebApi");
            HttpResponseMessage response = await httpClient.GetAsync($"myapi/Product/GetProducts");
            httpClient.Dispose();
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                products = (await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(responseStream))!;
            }
            return products;
        }

        public async Task<HttpResponseMessage> DeleteProduct(Guid id)
        {
            IEnumerable<Product> products = Array.Empty<Product>();
            var httpClient = _httpClientFactory.CreateClient("WebApi");
            HttpResponseMessage response = await httpClient.DeleteAsync($"myapi/Product/DeleteProduct/{id}");
            httpClient.Dispose();
            return response;
        }

        public async Task<Product> GetProduct(Guid id)
        {
            try
            {
                IEnumerable<Product> product = Array.Empty<Product>();
                var httpClient = _httpClientFactory.CreateClient("WebApi");
                HttpResponseMessage response = await httpClient.GetAsync($"myapi/Product/GetProduct/{id}");
                httpClient.Dispose();
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    product = (await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(responseStream))!;
                }
                return product.SingleOrDefault()!;
            }
            catch (Exception ex)
            {
                return new Product();
            }
        }

        public async Task<HttpResponseMessage> AddProduct(Product product)
        {
            try
            {
                var productJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                var httpClient = _httpClientFactory.CreateClient("WebApi");
                HttpResponseMessage response = await httpClient.PostAsync($"myapi/Product/AddProduct", productJson);
                httpClient.Dispose();
                return response;
            }
            catch (Exception ex) {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent(ex.Message);
                return response;
            }
        }

        public async Task<HttpResponseMessage> UpdateProduct(Product product)
        {
            try
            {
                var productJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                var httpClient = _httpClientFactory.CreateClient("WebApi");
                HttpResponseMessage response = await httpClient.PutAsync($"myapi/Product/UpdateProduct/{product.Id}", productJson);
                httpClient.Dispose();
                return response;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent(ex.Message);
                return response;
            }
        }
    }
}
