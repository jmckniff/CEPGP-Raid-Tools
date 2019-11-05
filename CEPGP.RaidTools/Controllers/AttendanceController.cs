using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using CEPGP.Domain;
using CEPGP.Persistence.File;

namespace CEPGP.RaidTools.Controllers
{
    public class AttendanceController : Controller
    {
        public ActionResult Index()
        {
            var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));

            var standingsRepository = new FileStandingsRepository(cepgpDirectory);
            var members = standingsRepository.GetMembers();

            var lastSevenDays = new Period(DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);

            var raidAttendanceRepository = new FileRaidAttendanceRepository(cepgpDirectory);
            var raidAttendance = raidAttendanceRepository.GetRaidAttendance(members, lastSevenDays);

            return View(raidAttendance);
        }

        public ActionResult Search(int daysInPast)
        {
            var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));

            var standingsRepository = new FileStandingsRepository(cepgpDirectory);
            var members = standingsRepository.GetMembers();

            var raidAttendanceRepository = new FileRaidAttendanceRepository(cepgpDirectory);

            IEnumerable<Attendance> raidAttendance;

            if (daysInPast == 0)
            {
                raidAttendance = raidAttendanceRepository.GetRaidAttendance(members, null);
            }
            else
            {
                var period = new Period(DateTime.UtcNow.AddDays(-daysInPast), DateTime.UtcNow);
                raidAttendance = raidAttendanceRepository.GetRaidAttendance(members, period);
            }

            return View("Index", raidAttendance);
        }
    }
}
