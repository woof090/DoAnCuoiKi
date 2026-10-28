using System.Web;
using System.Web.Mvc;

namespace BaoCaoCuoiKy_Nhom08
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
