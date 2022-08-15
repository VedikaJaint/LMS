using LMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;





namespace LibraryManagementSystem.Controllers
{





    public class AdminController : Controller
    {
        private readonly LMSContext _context;
        private readonly IAccountrepo _accountRepo;
        public AdminController(LMSContext context, IAccountrepo accountRepo)
        {
            _context = context;
            _accountRepo = accountRepo;
        }





        public IActionResult Index()
        {
            var lendData = from b in _context.LendRequests.Include(x => x.User).Include(y => y.Book) select b;
            return View(lendData.ToList());
        }
        public IActionResult LentBooks()
        {
            var lendData = from b in _context.LendRequests.Include(x => x.User).Include(y => y.Book) select b;
            return View(lendData.ToList());
        }
        public ActionResult requestApproved(int lendId)
        {
            var lendedBook = _context.LendRequests.FirstOrDefault(b => b.LendId == lendId);
            lendedBook.LendStatus = "Approved";
            lendedBook.ReturnDate = lendedBook.LendDate.AddDays(14);
            _context.SaveChanges();
            return RedirectToAction("Index", "Admin");                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
        }
        public ActionResult requestDeclined(int lendId)
        {
            var lendedBook = _context.LendRequests.FirstOrDefault(b => b.LendId == lendId);
            lendedBook.LendStatus = "Declined";
            _context.Books.SingleOrDefault(b => b.BookId == lendedBook.BookId).NoOfCopies++;
            _context.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }





    }
}