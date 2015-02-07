using System.Web.Mvc;
using DataAnalysis.Web.Admin.Framework;

namespace DataAnalysis.Web.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}