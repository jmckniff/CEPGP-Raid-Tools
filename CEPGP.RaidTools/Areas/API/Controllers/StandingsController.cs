using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using CEPGP.Persistence.File;
using CEPGP.RaidTools.Areas.API.Dtos;
using Newtonsoft.Json;

namespace CEPGP.RaidTools.Areas.API.Controllers
{
    public class StandingsController : Controller
    {
        public ActionResult Index()
        {
            var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));
            var repository = new FileStandingsRepository(cepgpDirectory);
            var members = repository.GetMembers();

            var memberList = new MemberListDto
            {
                LastUpdateDate = JsonConvert.SerializeObject(members.LastUpdateDate),
                Members = new List<MemberDto>()
            };

            foreach (var member in members)
            {
                memberList.Members.Add(new MemberDto
                {
                    Name = member.Name,
                    EP = member.EP.Value,
                    GP = member.GP.Value,
                    PR = member.CalculatePR().Value
                });
            }

            return Json(memberList, JsonRequestBehavior.AllowGet);
        }
    }
}
