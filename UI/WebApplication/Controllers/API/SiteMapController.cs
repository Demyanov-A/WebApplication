using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Interfaces.Services;
using NuGet.Packaging;
using SimpleMvcSitemap;

namespace WebApplication.Controllers.API
{
    public class SiteMapController : ControllerBase // localhost/SiteMap
    {
        public IActionResult Index([FromServices] IProductData ProductData)
        {
            var nodes = new List<SitemapNode>
            {
                new(Url.Action("Index", "Home")),
                new(Url.Action("ConfiguredAction", "Home")),
                new(Url.Action("Index", "Blogs")),
                new(Url.Action("Blog", "Blogs")),
                new(Url.Action("Index", "WebAPI")),
                new(Url.Action("Index", "Catalog")),
            };

            nodes.AddRange(ProductData.GetSections().Select(s => new SitemapNode(Url.Action("Index", "Catalog", new { SectionId = s.Id }))));

            foreach (var brand in ProductData.GetBrands())
                nodes.Add(new SitemapNode(Url.Action("Index", "Catalog", new { BrandId = brand.Id })));

            foreach (var product in ProductData.GetProducts().Products)
                nodes.Add(new SitemapNode(Url.Action("Details", "Catalog", new { product.Id })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
