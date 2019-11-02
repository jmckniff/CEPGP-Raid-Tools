using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using CEPGP.Persistence.File;
using CEPGP.RaidTools.Areas.API.Models;

namespace CEPGP.RaidTools.Areas.API.Controllers
{
    public class StandingsController : Controller
    {
        public ActionResult Index()
        {
            var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));
            var repository = new FileStandingsRepository(cepgpDirectory);
            var members = repository.GetMembers();

            var standings = new List<Standing>();

            foreach (var member in members)
            {
                standings.Add(new Standing
                {
                    Name = member.Name,
                    EP = member.EP.Value,
                    GP = member.GP.Value,
                    PR = member.CalculatePR().Value
                });
            }

            return Json(standings, JsonRequestBehavior.AllowGet);
        }
    }
}
