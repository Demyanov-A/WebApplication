﻿using System;
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

        Section? GetSectionById(int Id);

        IEnumerable<Brand> GetBrands();

        Brand? GetBrandById(int Id);

        IEnumerable<Product> GetProducts(ProductFilter? Filter = null);

        Product? GetProductById(int Id);

        Product CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null);
    }
}
