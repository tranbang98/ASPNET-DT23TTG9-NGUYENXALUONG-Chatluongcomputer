﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatluongcomputer.Models
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

}