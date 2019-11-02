using System.IO;
using System.Web.Mvc;
using CEPGP.Persistence.File;

namespace CEPGP.RaidTools.Controllers
{
    public class StandingsController : Controller
    {
        // GET: Standings
        public ActionResult Index()
        {
            var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));
            var repository = new FileStandingsRepository(cepgpDirectory);
            var members = repository.GetMembers();

            return View(members);
        }
    }
}
