using System.Collections.Generic;
using System.IO;
using System.Linq;
using CEPGP.Domain;
using CEPGP.Domain.Ports;
using LsonLib;

namespace CEPGP.Persistence.File
{
    public class FileTransactionRepository : ITransactionRepository
    {
        private readonly DirectoryInfo _cepgpDirectory;

        public FileTransactionRepository(DirectoryInfo cepgpDirectory)
        {
            _cepgpDirectory = cepgpDirectory;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            var transactions = new List<Transaction>();

            var cepgpFilePath = Path.Combine(_cepgpDirectory.FullName, "CEPGP.lua");

            if (!System.IO.File.Exists(cepgpFilePath))
            {
                return transactions;
            }

            var lson = LsonVars.Parse(System.IO.File.ReadAllText(cepgpFilePath));

            var trafficItem = lson["TRAFFIC"];

            if (trafficItem == null)
            {
                return transactions;
            }

            foreach (var item in trafficItem.Values)
            {
                var transaction = new Transaction();

                transaction.Target = item.Values.ElementAt(0).GetString();
                transaction.IssuedBy = item.Values.ElementAt(1).GetString();
                transaction.Action = item.Values.ElementAt(2).GetString();
                transaction.EPBefore = new EP(item.Values.ElementAtOrDefault(3)?.GetDecimalSafe(NumericConversionOptions.AllowConversionFromString) ?? 0);
                transaction.EPAfter = new EP(item.Values.ElementAtOrDefault(4)?.GetDecimalSafe(NumericConversionOptions.AllowConversionFromString) ?? 0);
                transaction.GPBefore = new GP(item.Values.ElementAtOrDefault(5)?.GetDecimalSafe(NumericConversionOptions.AllowConversionFromString) ?? 0);
                transaction.GPAfter = new GP(item.Values.ElementAtOrDefault(6)?.GetDecimalSafe(NumericConversionOptions.AllowConversionFromString) ?? 0);
                transaction.Item = Item.Parse(item.Values.ElementAtOrDefault(7)?.GetStringSafe());
                
                transactions.Add(transaction);
            }

            transactions.Reverse();

            return transactions;
        }

        public IEnumerable<Transaction> GetTransactionsForMember(string memberUsername)
        {
            var transactions = GetTransactions();

            return transactions.Where(t => 
                t.Target.ToLower() == memberUsername.ToLower() ||
                t.Target.ToLower() == "guild" ||
                t.Target.ToLower() == "raid");
        }
    }
}
