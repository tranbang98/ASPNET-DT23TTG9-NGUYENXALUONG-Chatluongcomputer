﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatluongcomputer.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // Pending, Shipped, Delivered

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }

}