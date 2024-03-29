﻿using ECommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interfaces
{
  public interface IProductProvider
  {
    Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductAsync();
  }
}
