using System;
using System.Collections.Generic;

namespace CEPGP.Domain
{
    public class MemberList : List<Member>
    {
        public DateTime LastUpdateDate { get; private set; }

        public void SetLastUpdatedDate(DateTime lastUpdateDate)
        {
            LastUpdateDate = lastUpdateDate;
        }
    }
}
