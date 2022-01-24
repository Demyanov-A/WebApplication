﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Domain;
using WebApplication.Domain.DTO;
using WebApplication.Interfaces.Services;

namespace WebApplication.WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : Controller
    {
        private readonly IProductData _ProductData;

        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")] // GET -> http://localhost:5001/api/products/sections
        public IActionResult GetSections()
        {
            var sections = _ProductData.GetSections();
            return Ok(sections.ToDTO());
        }

        [HttpGet("sections/{Id}")] // GET -> http://localhost:5001/api/products/sections/5
        public IActionResult GetSectionById(int Id)
        {
            var section = _ProductData.GetSectionById(Id);
            if (section is null)
                return NotFound();

            return Ok(section.ToDTO());
        }

        [HttpGet("brands")] // GET -> http://localhost:5001/api/products/brands
        public IActionResult GetBrands()
        {
            var brands = _ProductData.GetBrands();
            return Ok(brands.ToDTO());
        }

        [HttpGet("brands/{Id}")] // GET -> http://localhost:5001/api/products/brands/5
        public IActionResult GetBrandById(int Id)
        {
            var brand = _ProductData.GetBrandById(Id);
            if (brand is null)
                return NotFound();

            return Ok(brand.ToDTO());
        }

        [HttpPost]
        public IActionResult GetProducts(ProductFilter? Filter = null)
        {
            var products = _ProductData.GetProducts(Filter);
            return Ok(products.ToDTO());
        }

        [HttpGet("{Id}")]
        public IActionResult GetProductGyId(int Id)
        {
            var product = _ProductData.GetSectionById(Id);
            if (product is null)
                return NotFound();

            return Ok(product.ToDTO());
        }

        [HttpPost("new/{Name}")]
        public IActionResult CreateProduct(CreateProductDTO Model)
        {
            var product = _ProductData.CreateProduct(Model.Name, Model.Order, Model.Price, Model.ImageUrl, Model.Section, Model.Brand);
            return CreatedAtAction(nameof(GetProductGyId), new {product.Id}, product.ToDTO());
        }
    }
}
