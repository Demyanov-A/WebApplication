using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<Section> GetSections() => _db.Sections;
    }
}
