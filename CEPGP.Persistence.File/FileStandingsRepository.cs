using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CEPGP.Domain;
using CEPGP.Persistence.File.Dto;
using CsvHelper;
using CsvHelper.Configuration;

namespace CEPGP.Persistence.File
{
    public class FileStandingsRepository
    {
        private readonly DirectoryInfo _cepgpDirectory;

        public FileStandingsRepository(DirectoryInfo cepgpDirectory)
        {
            _cepgpDirectory = cepgpDirectory;
        }

        public List<Member> GetMembers()
        {
            var members = new List<Member>();

            var standingsFilePath = Path.Combine(_cepgpDirectory.FullName, "CEPGP-Standings.txt");

            var fileContent = System.IO.File.ReadAllText(standingsFilePath);

            if (!fileContent.StartsWith("Name"))
            {
                var header = "Name,Class,Rank,EP,GP,PR" + Environment.NewLine;
                fileContent = header + fileContent;
                System.IO.File.WriteAllText(standingsFilePath, fileContent);
            }

            var csvConfiguration = new Configuration
            {
                IgnoreBlankLines = true,
                MissingFieldFound = null
            };

           
            if (!System.IO.File.Exists(standingsFilePath))
            {
                return members;
            }

            using (var reader = new StreamReader(standingsFilePath))
            using (var csv = new CsvReader(reader, csvConfiguration))
            {
                var standings = csv.GetRecords<MemberStanding>();

                standings = standings
                    .OrderBy(s => s.Name)
                    .ToList();

                foreach (var standing in standings)
                {
                    var member = new Member
                    {
                        Name = standing.Name,
                        EP = new EP(0),
                        GP = new GP(0)
                    };

                    if (!string.IsNullOrEmpty(standing.EP))
                    {
                        member.EP = new EP(Convert.ToDecimal(standing.EP));
                    }

                    if (!string.IsNullOrEmpty(standing.GP))
                    {
                        member.GP = new GP(Convert.ToDecimal(standing.GP));
                    }

                    members.Add(member);
                }

            }

            return members;
        }
    }
}
