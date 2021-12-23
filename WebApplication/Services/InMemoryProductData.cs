using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Domain;
using WebApplication.Domain.Entities;
using WebApplication.Domain.Entities.Base;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
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

            //if (Filter?.SectionId != null)
            //    query = query.Where(p => p.SectionId == Filter.SectionId);

            if (Filter is {SectionId: var section_id})
                query = query.Where(p => p.SectionId == section_id);

            if (Filter is {BrandId: var brand_id})
                query = query.Where(p => p.BrandId == brand_id);

            return query;

        }
    }
}
