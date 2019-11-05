using System;

namespace CEPGP.Domain
{
    public class Period
    {
        public Period(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }
    }
}
