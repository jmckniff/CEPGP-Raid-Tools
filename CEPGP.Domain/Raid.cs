using System;
using System.Collections.Generic;

namespace CEPGP.Domain
{
    public class Raid
    {
        public string Name { get; }
        public DateTime Date { get; private set; }
        public List<string> Members { get; }

        public Raid(string name)
        {
            Name = name;
            Members = new List<string>();
        }

        public void SetDate(DateTime date)
        {
            Date = date;
        }
    }
}
