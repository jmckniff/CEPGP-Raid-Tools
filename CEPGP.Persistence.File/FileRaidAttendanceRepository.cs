using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CEPGP.Domain;
using LsonLib;

namespace CEPGP.Persistence.File
{
    public class FileRaidAttendanceRepository
    {
        private readonly DirectoryInfo _cepgpDirectory;

        public FileRaidAttendanceRepository(DirectoryInfo cepgpDirectory)
        {
            _cepgpDirectory = cepgpDirectory;
        }

        public IEnumerable<Raid> GetRaids()
        {
            var raids = new List<Raid>();

            var cepgpFilePath = Path.Combine(_cepgpDirectory.FullName, "CEPGP.lua");

            if (!System.IO.File.Exists(cepgpFilePath))
            {
                return raids;
            }

            var lson = LsonVars.Parse(System.IO.File.ReadAllText(cepgpFilePath));

            var raidsraidSnapshots = lson["CEPGP_raid_logs"];

            if (raidsraidSnapshots == null)
            {
                return raids;
            }

            foreach (var raidsSnapshot in raidsraidSnapshots.Values)
            {
                var raid = new Raid("");

                for (var i = 0; i < raidsSnapshot.Values.Count; i++)
                {
                    var entry = raidsSnapshot.Values.ElementAt(i);

                    if (i == 0)
                    {
                        var raidUnixDate = entry.GetDouble();
                        var raidDate = UnixTimeStampToDateTime(raidUnixDate);
                        raid.SetDate(raidDate);
                    }
                    else
                    {
                        raid.Members.Add(entry.GetStringSafe());
                    }
                }

                raids.Add(raid);
            }

            return raids;
        }

        public IEnumerable<Attendance> GetRaidAttendance(IEnumerable<Member> members, Period period)
        {
            var attendance = new List<Attendance>();

            var raids = GetRaids();

            var raidsInPeriod = new List<Raid>();

            if (period != null)
            {
                raidsInPeriod = raids
                    .Where(raid =>
                        raid.Date.ToUniversalTime() >= period.DateFrom &&
                        raid.Date.ToUniversalTime() <= period.DateTo)
                    .ToList();
            }
            else
            {
                raidsInPeriod = raids.ToList();
            }

            foreach (var member in members)
            {
                var raidsAttendedInPeriod = 0;

                if (period != null)
                {
                    raidsAttendedInPeriod = raids
                        .Count(raid =>
                            raid.Members.Contains(member.Name) &&
                            raid.Date.ToUniversalTime() >= period.DateFrom &&
                            raid.Date.ToUniversalTime() <= period.DateTo);
                }
                else
                {
                    raidsAttendedInPeriod = raids.Count(raid => raid.Members.Contains(member.Name));
                }

                var attendanceLastSevenDays = new Attendance(member, period, raidsInPeriod.Count, raidsAttendedInPeriod);

                attendance.Add(attendanceLastSevenDays);
            }

            return attendance;
        }


        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

    }
}
