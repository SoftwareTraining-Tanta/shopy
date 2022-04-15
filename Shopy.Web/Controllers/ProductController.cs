using Microsoft.AspNetCore.Mvc;

using Shopy.Web.Models;
using Shopy.Web.Dtos;
using Shopy.Web.Shared;
namespace Northwind.WebApi.Controllers;

    [Route("api/products/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet("get/productid={productId}")]
        public ProductDto Get(int productId)
        {
            Product product = new ();
            return product.Get(productId).AsDto();
        }
        [HttpGet("limit/{limit}")]
        public List<ProductDto> AvailableProducts(int limit)
        {
            Product product = new();
            return product.AvailableProducts(limit).AsDto();
        }
        // GET api/<ValuesController>/5
        [HttpGet("getvendor/productid={productId}")]
        public VendorDto GetVendor(int productId)
        {
            Product product = new();
            VendorDto _vendor= product.GetVendor(productId).AsDto();
            return _vendor;
            
        }


        // POST api/<ValuesController>
        [HttpPost]
        public string Add(ProductDto _product)
        {
            Product product = new();
            return product.Add(_product.AsNormal());
        }

        [HttpPost("add/quantity={quantity}")]
        public string AddQuantity(ProductDto _product,int quantity)
        {
            Product product = new();
            return product.AddQuantity(_product.AsNormal(),quantity);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("update_rate/productid={productId}")]
        public string UpdateRate(int ProductId,  decimal rate )
        {
            Product product = new();
            return product.UpdateRate(ProductId,rate);


        }
        [HttpPut("update_model/productid={productId}")]
        public string UpdateModel(int ProductId, string model)
        {
            Product product = new();
            return product.UpdateModel(ProductId, model);


        }
        [HttpPut("update_client/productid={productId}")]
        public string UpdateClientUsername(int ProductId, string username)
        {
            Product product = new();
            return product.UpdateClientUsername(ProductId,username);


        }
        [HttpPut("update_CartId/productid={productId}/cartid={CartId}")]
        public string UpdateModel(int ProductId, int CartId)
        {
            Product product = new();
            return product.UpdateCartId(ProductId, CartId);


        }
        [HttpGet("exist/productid={productId}")]
        public bool existed(int productId)
        {
            Product product = new();
            return product.Exist(productId);

        }
        // DELETE api/<ValuesController>/5

    }

