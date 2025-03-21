﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.NetCore_Inventory_Order_Management_System.Data;
using Asp.NetCore_Inventory_Order_Management_System.Models;
using Asp.NetCore_Inventory_Order_Management_System.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Asp.NetCore_Inventory_Order_Management_System.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;

        public ProductController(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            List<Product> Items = await _applicationContext.Product.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }



        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Product> payload)
        {
            Product product = payload.value;
            _applicationContext.Product.Add(product);
            _applicationContext.SaveChanges();
            return Ok(product);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Product> payload)
        {
            Product product = payload.value;
            _applicationContext.Product.Update(product);
            _applicationContext.SaveChanges();
            return Ok(product);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Product> payload)
        {
            Product product = _applicationContext.Product
                .Where(x => x.ProductId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.Product.Remove(product);
            _applicationContext.SaveChanges();
            return Ok(product);
        }
    }
}