using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatluongcomputer.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }

        public string PaymentMethod { get; set; } // COD, Momo, ZaloPay, VNPay, etc.
        public bool IsPaid { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual Order Order { get; set; }
    }

}