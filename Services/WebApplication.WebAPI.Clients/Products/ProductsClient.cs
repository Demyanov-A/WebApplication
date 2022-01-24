﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain;
using WebApplication.Domain.DTO;
using WebApplication.Domain.Entities;
using WebApplication.Interfaces.Services;
using WebApplication.WebAPI.Clients.Base;

namespace WebApplication.WebAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(HttpClient Client) : base(Client, "api/products")
        {
        }

        public IEnumerable<Section> GetSections()
        {
            var sections = Get<IEnumerable<SectionDTO>>($"{Address}/sections");
            return sections!.FromDTO()!;
        }
        public Section? GetSectionById(int Id)
        {
            var section = Get<SectionDTO>($"{Address}/sections/{Id}");
            return section.FromDTO();
        }

        public IEnumerable<Brand> GetBrands()
        {
            var brands = Get<IEnumerable<BrandDTO>>($"{Address}/brands");
            return brands!.FromDTO()!;
        }

        public Brand? GetBrandById(int Id)
        {
            var brand = Get<BrandDTO>($"{Address}/brands/{Id}");
            return brand.FromDTO();
        }


        public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
        {
            var response = Post(Address, Filter ?? new());
            var products = response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>().Result;
            return products!.FromDTO()!;
        }

        public Product? GetProductById(int Id)
        {
            var product = Get<ProductDTO>($"{Address}/{Id}");
            return product.FromDTO();
        }

        public Product CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null)
        {
            var response = Post($"{Address}/new", new CreateProductDTO
            {
                Name = Name,
                Order = Order,
                Price = Price,
                ImageUrl = ImageUrl,
                Section = Section,
                Brand = Brand,
            });

            var product = response.Content.ReadFromJsonAsync<ProductDTO>().Result;
            return product!.FromDTO()!;
        }
    }
}