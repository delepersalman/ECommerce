using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
  public class ProductProvider : IProductProvider
  {
    private readonly ProductDBContext dbContext;
    private readonly ILogger<ProductProvider> logger;
    private readonly IMapper mapper;
    public ProductProvider(ProductDBContext dBContext, ILogger<ProductProvider> logger, IMapper mapper)
    {
      this.dbContext = dBContext;
      this.logger = logger;
      this.mapper = mapper;
      SeedData();
    }
    private void SeedData()
    {
      if (!dbContext.Products.Any())
      {
        dbContext.Products.Add(new DB.Product() { Id = 1, Name = "KeyBoard", Price = 100, Inventory = 100 });
        dbContext.Products.Add(new DB.Product() { Id = 2, Name = "Mouse", Price = 100, Inventory = 100 });
        dbContext.Products.Add(new DB.Product() { Id = 3, Name = "Handset", Price = 100, Inventory = 100 });
        dbContext.Products.Add(new DB.Product() { Id = 4, Name = "Wifi Adaptor", Price = 100, Inventory = 100 });
        dbContext.Products.Add(new DB.Product() { Id = 5, Name = "Donggle", Price = 100, Inventory = 100 });
        dbContext.SaveChanges();
      }
    }
    public async Task<(bool IsSuccess, IEnumerable<ECommerce.Api.Products.Models.Product> products, string ErrorMessage)> GetProductAsync()
    {
      try
      {
        var products = await dbContext.Products.ToListAsync();
        if (products != null && products.Any())
        {
          var result = mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
          return (true, result, null);
        }
        return (true, null, "Not Found");
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (true, null, ex.Message);
      }
      
    }
  }
}
