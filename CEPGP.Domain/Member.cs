namespace CEPGP.Domain
{
    public class Member
    {
        public string Name { get; set; }
        public EP EP { get; set; }
        public GP GP { get; set;  }

        public PR CalculatePR()
        {
            return new PR(EP, GP);
        }
    }
}