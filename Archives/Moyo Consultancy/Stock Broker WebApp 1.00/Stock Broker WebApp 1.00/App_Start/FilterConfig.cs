﻿using System.Web;
using System.Web.Mvc;

namespace Stock_Broker_WebApp_1._00
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
