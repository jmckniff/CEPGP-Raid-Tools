using System.IO;
using System.Web.Mvc;

namespace CEPGP.RaidTools.Areas.Admin.Controllers
{
    public class StandingsController : Controller
    {
        // GET: Admin/Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));

                if (!cepgpDirectory.Exists)
                {
                    cepgpDirectory.Create();
                }
                
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = "CEPGP-Standings.txt";
                    var path = Path.Combine(cepgpDirectory.FullName, fileName);
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("UploadSuccess");
        }

        public ActionResult UploadSuccess()
        {
            return View();
        }

    }
}