using Microsoft.EntityFrameworkCore;
using WebApplication.DAL.Context;
using WebApplication.Domain;
using WebApplication.Domain.Entities;
using WebApplication.Interfaces.Services;

namespace WebApplication.Services.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebApplicationDB _db;

        public SqlProductData(WebApplicationDB db) => _db = db;

        public IEnumerable<Section> GetSections(int Skip = 0, int? Take = null)
        {
            IQueryable<Section> query = _db.Sections;
            if (Skip > 0) query = query.Skip(Skip);
            if (Take > 0) query = query.Take((int)Take);
            return query.AsEnumerable();
        }
        public Section? GetSectionById(int Id) => _db.Sections
           .Include(s => s.Products)
           .FirstOrDefault(s => s.Id == Id);

        public int GetSectionsCount() => _db.Sections.Count();

        public IEnumerable<Brand> GetBrands(int Skip = 0, int? Take = null)
        {
            IQueryable<Brand> query = _db.Brands;
            if (Skip > 0) query = query.Skip(Skip);
            if (Take > 0) query = query.Take((int)Take);
            return query.AsEnumerable();
        }

        public Brand? GetBrandById(int Id) => _db.Brands
           .Include(b => b.Products)
           .FirstOrDefault(b => b.Id == Id);

        public int GetBrandsCount() => _db.Brands.Count();

        public ProductsPage GetProducts(ProductFilter? Filter = null)
        {
            IQueryable<Product> query = _db.Products
               .Include(p => p.Brand)
               .Include(p => p.Section);

            if (Filter?.Ids?.Length > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            else
            {
                if (Filter?.SectionId is { } section_id)
                    query = query.Where(p => p.SectionId == section_id);

                if (Filter?.BrandId is { } brand_id)
                    query = query.Where(p => p.BrandId == brand_id);
            }

            var count = query.Count();

            //if (Filter != null && Filter.PageSize > 0 && Filter.Page > 0)
            //{
            //    var page_size = (int)Filter.PageSize;
            //    var page = Filter.Page;

            //    query = query
            //       .Skip((page - 1) * page_size)
            //       .Take(page_size);
            //}

            if (Filter is { PageSize: > 0 and var page_size, Page: > 0 and var page })
                query = query
                    .Skip((page - 1) * page_size)
                    .Take(page_size);

            return new(query.AsEnumerable(), count);
        }

        public Product? GetProductById(int Id) => _db.Products
           .Include(p => p.Brand)
           .Include(p => p.Section)
           .FirstOrDefault(p => p.Id == Id);

        public Product CreateProduct(
            string Name,
            int Order,
            decimal Price,
            string ImageURL,
            string Section,
            string? Brand = null)
        {
            //var section = _db.Sections.FirstOrDefault(s => s.Name == Section);
            //if (section is null)
            //{
            //    section = new Section { Name = Section };
            //}
            var section = _db.Sections.FirstOrDefault(s => s.Name == Section)
                ?? new Section { Name = Section };

            var brand = Brand is { Length: > 0 }
                ? _db.Brands.FirstOrDefault(b => b.Name == Brand) ?? new Brand { Name = Brand }
                : null;

            var product = new Product
            {
                Name = Name,
                Price = Price,
                Order = Order,
                ImageURL = ImageURL,
                Section = section,
                Brand = brand,
            };

            _db.Products.Add(product);
            _db.SaveChanges();

            return product;
        }
    }
}
