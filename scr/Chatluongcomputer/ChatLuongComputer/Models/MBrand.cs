using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace ChatLuongComputer.Models
{
    public class MBrand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; } 

        public string Link { get; set; } 
    }
}
