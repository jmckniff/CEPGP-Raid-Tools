namespace CEPGP.Domain
{
    public class PR
    {
        public decimal Value { get; }

        public PR(EP ep, GP gp)
        {
            if (ep.Value == 0)
            {
                Value = 0;
            }
            else
            {
                Value = ep.Value / gp.Value;
            }
        }
    }
}
