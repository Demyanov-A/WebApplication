using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain;
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

        public IEnumerable<Section> GetSections() { throw new NotImplementedException(); }

        public Section? GetSectionById(int Id) { throw new NotImplementedException(); }

        public IEnumerable<Brand> GetBrands() { throw new NotImplementedException(); }

        public Brand? GetBrandById(int Id) { throw new NotImplementedException(); }

        public IEnumerable<Product> GetProducts(ProductFilter? Filter = null) { throw new NotImplementedException(); }

        public Product? GetProductById(int Id) { throw new NotImplementedException(); }

        public Product CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null) { throw new NotImplementedException(); }
    }
}
