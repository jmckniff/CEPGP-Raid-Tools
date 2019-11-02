using System.Collections.Generic;

namespace CEPGP.Domain.Ports
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetTransactions();
    }
}
