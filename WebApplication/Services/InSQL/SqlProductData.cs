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

            if (Filter?.Ids?.Length > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            else
            {
                if (Filter?.SectionId is { } section_id)
                    query = query.Where(p => p.SectionId == section_id);

                if (Filter?.BrandId is { } brand_id)
                    query = query.Where(p => p.BrandId == brand_id);
            }

            return query;
        }

        public Product? GetProductById(int Id) => _db.Products
            .Include(p => p.Brand)
            .Include(p => p.Section)
            .FirstOrDefault(p => p.Id == Id);

        public IEnumerable<Section> GetSections() => _db.Sections;
    }
}
