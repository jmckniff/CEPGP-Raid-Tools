using System.Web.Mvc;

namespace CEPGP.RaidTools.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

    }
}
