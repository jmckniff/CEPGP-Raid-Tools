using System.IO;
using System.Web.Mvc;
using CEPGP.Persistence.File;

namespace CEPGP.RaidTools.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public ActionResult Index()
        {
            var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));
            var repository = new FileTransactionRepository(cepgpDirectory);
            var transactions = repository.GetTransactions();

            return View(transactions);
        }

        public ActionResult Member(string username)
        {
            var cepgpDirectory = new DirectoryInfo(Server.MapPath("~/CEPGP/"));
            var repository = new FileTransactionRepository(cepgpDirectory);
            var transactions = repository.GetTransactionsForMember(username);

            return View(transactions);
        }

    }
}
