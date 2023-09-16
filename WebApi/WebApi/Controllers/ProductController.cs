using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Data;
using System.Xml.Linq;
using Core.Application.Repository;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("myapi/[controller]")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("GetProducts")]
        public JsonResult GetProducts()
        {
            var res = _productRepository.GetAll();
            return new JsonResult(res);
        }

        [HttpGet]
        [Route("GetProduct/{id}")]
        public JsonResult GetProduct(Guid id)
        {
            Product product = new Product();
            product.Id = id;
            var res = _productRepository.GetAll(product);
            var test = new JsonResult(res);
            return new JsonResult(res);
        }

        [HttpPost]
        [Route("AddProduct")]
        public JsonResult AddProduct(Product product)
        {
            var res = _productRepository.Add(product);
            return new JsonResult(res);
        }

        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public JsonResult UpdateProduct(Product product)
        {
            var res = _productRepository.Update(product);
            return new JsonResult(res);
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public JsonResult DeleteProduct(Guid id)
        {
            Product product = new Product();
            product.Id = id;

            var res = _productRepository.Delete(product);
            return new JsonResult(res);
        }

    }
}
