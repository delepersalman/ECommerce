using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
  [Route("api/products")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly IProductProvider productProvider;
    public ProductController(IProductProvider productProvider)
    {
      this.productProvider = productProvider;
    }
    [HttpGet]
    public async  Task<IActionResult> GetProductsAsync()
    {
      var result = await productProvider.GetProductAsync();
      if (result.IsSuccess)
      {
        return Ok(result.products);
      }
      return NotFound();
    }
  }
}
