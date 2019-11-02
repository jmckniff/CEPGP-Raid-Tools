using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using CEPGP.Persistence.File;
using Transaction = CEPGP.RaidTools.Areas.API.Models.Transaction;

namespace CEPGP.RaidTools.Areas.API.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public ActionResult Index()
        {
            var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));
            var repository = new FileTransactionRepository(cepgpDirectory);
            var transactions = repository.GetTransactions();

            var transactionDtos = new List<Transaction>();

            foreach (var transaction in transactions)
            {
                transactionDtos.Add(new Transaction
                {
                    Target = transaction.Target,
                    IssuedBy = transaction.IssuedBy,
                    EPBefore = transaction.EPBefore?.Value ?? 0,
                    EPAfter = transaction.EPAfter?.Value ?? 0,
                    GPBefore = transaction.GPBefore?.Value ?? 0,
                    GPAfter = transaction.GPAfter?.Value ?? 0,
                    Action = transaction.Action,
                    Item = transaction.Item?.Id ?? 0
                });
            }

            return Json(transactionDtos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Member(string username)
        {
            var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));
            var repository = new FileTransactionRepository(cepgpDirectory);
            var transactions = repository.GetTransactionsForMember(username);

            var transactionDtos = new List<Transaction>();

            foreach (var transaction in transactions)
            {
                transactionDtos.Add(new Transaction
                {
                    Target = transaction.Target,
                    IssuedBy = transaction.IssuedBy,
                    EPBefore = transaction.EPBefore?.Value ?? 0,
                    EPAfter = transaction.EPAfter?.Value ?? 0,
                    GPBefore = transaction.GPBefore?.Value ?? 0,
                    GPAfter = transaction.GPAfter?.Value ?? 0,
                    Action = transaction.Action,
                    Item = transaction.Item?.Id ?? 0
                });
            }

            return Json(transactionDtos, JsonRequestBehavior.AllowGet);
        }

    }
}
