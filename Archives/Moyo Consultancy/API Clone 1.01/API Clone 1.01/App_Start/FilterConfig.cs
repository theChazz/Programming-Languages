﻿using System.Web;
using System.Web.Mvc;

namespace API_Clone_1._01
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
