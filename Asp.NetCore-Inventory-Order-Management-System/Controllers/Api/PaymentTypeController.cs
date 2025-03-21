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
    [Route("api/PaymentType")]
    public class PaymentTypeController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;

        public PaymentTypeController(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        // GET: api/PaymentType
        [HttpGet]
        public async Task<IActionResult> GetPaymentType()
        {
            List<PaymentType> Items = await _applicationContext.PaymentType.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }


        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<PaymentType> payload)
        {
            PaymentType paymentType = payload.value;
            _applicationContext.PaymentType.Add(paymentType);
            _applicationContext.SaveChanges();
            return Ok(paymentType);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<PaymentType> payload)
        {
            PaymentType paymentType = payload.value;
            _applicationContext.PaymentType.Update(paymentType);
            _applicationContext.SaveChanges();
            return Ok(paymentType);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<PaymentType> payload)
        {
            PaymentType paymentType = _applicationContext.PaymentType
                .Where(x => x.PaymentTypeId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.PaymentType.Remove(paymentType);
            _applicationContext.SaveChanges();
            return Ok(paymentType);
        }
    }
}