﻿using System.Web;
using System.Web.Mvc;

namespace Proyecto_Elecciones2021
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
