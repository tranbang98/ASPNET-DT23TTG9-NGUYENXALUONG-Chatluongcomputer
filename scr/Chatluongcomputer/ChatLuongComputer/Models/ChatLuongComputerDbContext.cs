﻿namespace ChatLuongComputer.Models
{
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
    public class ChatLuongComputerDbContext : DbContext
    {
        public ChatLuongComputerDbContext() : base("name=StrC")
        { }
        public virtual DbSet<MProduct> Products { get; set; }
        public virtual DbSet<MCategory> Categorys { get; set; }
        public virtual DbSet<MContact> Contacts { get; set; }
        public virtual DbSet<MMenu> Menus { get; set; }
        public virtual DbSet<MOrder> Orders { get; set; }
        public virtual DbSet<MOrderdetail> Orderdetails { get; set; }
        public virtual DbSet<MPost> Posts { get; set; }
        public virtual DbSet<MSlider> Sliders { get; set; }
        public virtual DbSet<MTopic> Topics { get; set; }
        public virtual DbSet<MUser> Users { get; set; }
        public virtual DbSet<MLink> Links { get; set; }
		public DbSet<ProductData> ProductData { get; set; }
        public virtual DbSet<MBrand> Brands { get; set; }
    }
}