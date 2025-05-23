﻿using ChatLuongComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatLuongComputer
{
    public class CheckSlug
    {
        ChatLuongComputerDbContext db = new ChatLuongComputerDbContext();
        public bool KiemTraSlug(String Table, String Slug, int? id)
        {
            switch (Table)
            {
                case "Category":
                    if (id != null)
                    {
                        if (db.Categorys.Where(m => m.Slug == Slug && m.Id != id).Count() > 0)
                            return false;
                    }
                    else
                    {
                        if (db.Categorys.Where(m => m.Slug == Slug).Count() > 0)
                            return false;
                    }
                    break;
                case "Topic":
                    break;
                case "Post":
                    break;
                case "Product":
                    break;
            }
            return true;


        }

    }
}