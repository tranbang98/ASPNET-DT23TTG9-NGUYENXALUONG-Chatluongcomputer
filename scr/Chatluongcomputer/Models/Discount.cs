//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chatluongcomputer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Discount
    {
        public int DiscountId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Percentage { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
    }
}
