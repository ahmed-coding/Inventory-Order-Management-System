﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.NetCore_Inventory_Order_Management_System.Data;
using Asp.NetCore_Inventory_Order_Management_System.Models;
using Asp.NetCore_Inventory_Order_Management_System.Services;
using Asp.NetCore_Inventory_Order_Management_System.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Asp.NetCore_Inventory_Order_Management_System.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/PaymentReceive")]
    public class PaymentReceiveController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly INumberSequence _numberSequence;

        public PaymentReceiveController(ApplicationDbContext applicationContext,
                        INumberSequence numberSequence)
        {
            _applicationContext = applicationContext;
            _numberSequence = numberSequence;
        }

        // GET: api/PaymentReceive
        [HttpGet]
        public async Task<IActionResult> GetPaymentReceive()
        {
            List<PaymentReceive> Items = await _applicationContext.PaymentReceive.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<PaymentReceive> payload)
        {
            PaymentReceive paymentReceive = payload.value;
            paymentReceive.PaymentReceiveName = _numberSequence.GetNumberSequence("PAYRCV");
            _applicationContext.PaymentReceive.Add(paymentReceive);
            _applicationContext.SaveChanges();
            return Ok(paymentReceive);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<PaymentReceive> payload)
        {
            PaymentReceive paymentReceive = payload.value;
            _applicationContext.PaymentReceive.Update(paymentReceive);
            _applicationContext.SaveChanges();
            return Ok(paymentReceive);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<PaymentReceive> payload)
        {
            PaymentReceive paymentReceive = _applicationContext.PaymentReceive
                .Where(x => x.PaymentReceiveId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.PaymentReceive.Remove(paymentReceive);
            _applicationContext.SaveChanges();
            return Ok(paymentReceive);
        }
    }
}