﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_Inventory_Order_Management_System.Models
{
    public class CashBank
    {
        public int CashBankId { get; set; }
        [Display(Name = "Cash / Bank Name")]
        public string CashBankName { get; set; }
        public string Description { get; set; }
    }
}
