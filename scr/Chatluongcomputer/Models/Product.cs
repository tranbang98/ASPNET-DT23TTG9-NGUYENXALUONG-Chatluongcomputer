﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatluongcomputer.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
    }

}