using WebApplication.DAL.Context;
using Microsoft.EntityFrameworkCore;
using WebApplication.DAL.Context;
using WebApplication.Domain;
using WebApplication.Domain.Entities;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebApplicationDB _db;

        public SqlProductData(WebApplicationDB db) => _db = db;

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.SectionId != null)
                query = query.Where(p => p.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(p => p.BrandId == Filter.BrandId);

            return query;
        }

        public Product? GetProductById(int Id) => _db.Products
            .Include(p => p.Brand)
            .Include(p => p.Section)
            .FirstOrDefault(p => p.Id == Id);

        public IEnumerable<Section> GetSections() => _db.Sections;
    }
}
