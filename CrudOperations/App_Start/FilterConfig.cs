using CrudOperations.CustomFilters;
using System.Web;
using System.Web.Mvc;

namespace CrudOperations
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

        }
    }
}
