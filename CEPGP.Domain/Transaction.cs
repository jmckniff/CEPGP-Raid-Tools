namespace CEPGP.Domain
{
    public class Transaction
    {
       public string Target { get; set; }
       public string IssuedBy { get; set; }

       public string Action { get; set; }

       public EP EPBefore { get; set; }
       public EP EPAfter { get; set; }

       public GP GPBefore { get; set; }
       public GP GPAfter { get; set; }
       public Item Item { get; set; }
    }
}
