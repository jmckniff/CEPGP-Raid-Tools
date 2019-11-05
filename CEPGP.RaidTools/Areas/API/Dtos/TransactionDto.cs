namespace CEPGP.RaidTools.Areas.API.Dtos
{
    public class TransactionDto
    {
        public string Target { get; set; }
        public string IssuedBy { get; set; }

        public string Action { get; set; }

        public decimal EPBefore { get; set; }
        public decimal EPAfter { get; set; }

        public decimal GPBefore { get; set; }
        public decimal GPAfter { get; set; }
        public int Item { get; set; }
    }
}