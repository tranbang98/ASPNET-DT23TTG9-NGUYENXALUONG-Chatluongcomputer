﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatLuongComputer
{
    public class Notification
    {
        public static bool has_flash()
        {
            var notification = System.Web.HttpContext.Current.Session["Notification"];
            return notification != null && !string.IsNullOrWhiteSpace(notification.ToString());
        }

        public static void set_flash(String mgs, String mgs_type)
        {
            ModelNotification tb = new ModelNotification();
            tb.mgs = mgs;
            tb.mgs_type = mgs_type;

            System.Web.HttpContext.Current.Session["Notification"] = tb;
        }
        public static ModelNotification get_flash()
        {

            ModelNotification Notifi = (ModelNotification)System.Web.HttpContext.Current.Session["Notification"];
            System.Web.HttpContext.Current.Session["Notification"] = "";
            return Notifi;
        }
    }
}