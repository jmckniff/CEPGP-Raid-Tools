using System.Collections.Generic;

namespace CEPGP.RaidTools.Areas.API.Dtos
{
    public class MemberListDto
    {
        public string LastUpdateDate { get; set; }
        public List<MemberDto> Members { get; set; }
    }
}
