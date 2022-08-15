using Microsoft.AspNetCore.Mvc;
using LMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using LMS.Controllers;
namespace LMS.Controllers
{
    public class UserController : Controller
    {
        private readonly LMSContext _context;
        private readonly IAccountrepo _accountrepo;

        public UserController(LMSContext context, IAccountrepo accountrepo)
        {
            _context = context;
            _accountrepo = accountrepo;
        }
        public ViewResult Index(string searchString)
        {
            var bookCard = from b in _context.Books select b;
            if (!string.IsNullOrEmpty(searchString))
            {
                bookCard = bookCard.Where(s =>
                    s.BookTitle.Contains(searchString) ||
                    s.NoOfCopies.ToString().Contains(searchString) ||
                    s.Author.AuthorName.Contains(searchString) ||
                    s.Category.Contains(searchString)
                );
            }
            return View(bookCard.ToList());
        }

public ViewResult RequestToLend(int bookId) { var username = HttpContext.Session.GetString("userName"); 
            var user = _accountrepo.getUserByName(username); 
            var noofcopies = _context.Books.SingleOrDefault(b => b.BookId == bookId).NoOfCopies;
            if (noofcopies <= 0) 
            { 
                return View("RequestedError"); 
            }
            _context.Books.SingleOrDefault(b => b.BookId == bookId).NoOfCopies--;
            LendRequest lendRequest = new LendRequest()
            { LendStatus = "Requested", 
               LendDate = System.DateTime.Now, 
                BookId = bookId, 
                UserId = user.UserId,
                Book = _context.Books.SingleOrDefault(b => b.BookId == bookId), 
                
                User = _context.Accounts.SingleOrDefault(u => u.UserId == user.UserId), };
            _context.LendRequests.Add(lendRequest); 
            _context.SaveChanges();
            return View("Requested"); }


    }
}
