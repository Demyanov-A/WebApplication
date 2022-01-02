using WebApplication.Data;
using WebApplication.Domain;
using WebApplication.Domain.Entities;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }

        public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
        {
            IEnumerable<Product> query = TestData.Products;

            if (Filter?.SectionId != null)
                query = query.Where(p => p.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(p => p.BrandId == Filter.BrandId);

            //if (Filter?.SectionId is {} section_id})
            //    query = query.Where(p => p.SectionId == section_id);

            //if (Filter?.BrandId is {} brand_id})
            //    query = query.Where(p => p.BrandId == brand_id);

            return query;

        }
    }
}
