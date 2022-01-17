using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain;
using WebApplication.Domain.Entities;

namespace WebApplication.Services.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        Product? GetProductById(int Id);

        IEnumerable<Product> GetProducts(ProductFilter? Filter = null);
    }
}
