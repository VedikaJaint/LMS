using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Models;
using Microsoft.AspNetCore.Http;

namespace LMS.Controllers
{
    public class LendRequestsController : Controller
    {
        private readonly LMSContext _context;

        public LendRequestsController(LMSContext context)
        {
            _context = context;
        }

        // GET: LendRequests
        public async Task<IActionResult> Index()
        {
            var lMSContext = _context.LendRequests.Include(l => l.Book).Include(l => l.User);
            return View(await lMSContext.ToListAsync());
        }

        public async Task<IActionResult> Indexuser()
        {
            var username = HttpContext.Session.GetString("userName");
            var user = _context.Accounts.Where(b => b.UserName == username).FirstOrDefault();
            var lMSContext = _context.LendRequests.Where(b => b.UserId == user.UserId&&b.LendStatus.Equals("Approved")).Include(l => l.Book).Include(l => l.User);
            return View(await lMSContext.ToListAsync());
        }



        public async Task<IActionResult> LentBook()
        {

            var lMSContext = _context.LendRequests.Where(b => b.LendStatus.Equals("Approved")).Include(l => l.Book).Include(l => l.User);
            return View(await lMSContext.ToListAsync());
        }


        // GET: LendRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendRequest = await _context.LendRequests
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LendId == id);
            if (lendRequest == null)
            {
                return NotFound();
            }

            return View(lendRequest);
        }

        // GET: LendRequests/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "Password");
            return View();
        }

        // POST: LendRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LendId,LendStatus,LendDate,ReturnDate,UserId,BookId,FineAmount")] LendRequest lendRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lendRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", lendRequest.BookId);
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "Password", lendRequest.UserId);
            return View(lendRequest);
        }

        // GET: LendRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendRequest = await _context.LendRequests.FindAsync(id);
            if (lendRequest == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", lendRequest.BookId);
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "Password", lendRequest.UserId);
            return View(lendRequest);
        }

        // POST: LendRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LendId,LendStatus,LendDate,ReturnDate,UserId,BookId,FineAmount")] LendRequest lendRequest)
        {
            if (id != lendRequest.LendId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lendRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LendRequestExists(lendRequest.LendId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", lendRequest.BookId);
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "Password", lendRequest.UserId);
            return View(lendRequest);
        }

        // GET: LendRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendRequest = await _context.LendRequests
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LendId == id);
            if (lendRequest == null)
            {
                return NotFound();
            }

            return View(lendRequest);
        }

        // POST: LendRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lendRequest = await _context.LendRequests.FindAsync(id);
            _context.LendRequests.Remove(lendRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Return(int id)
        {
            var lendedBook = _context.LendRequests.FirstOrDefault(b => b.LendId == id);
            lendedBook.LendStatus = "Returned";
            TimeSpan t = System.DateTime.Now - lendedBook.ReturnDate;
            lendedBook.FineAmount = t.Days - 14 > 0 ? (t.Days - 14) * 20 : 0;


            _context.Books.SingleOrDefault(b => b.BookId == lendedBook.BookId).NoOfCopies++;
            _context.SaveChanges();
            return RedirectToAction("Indexuser", "LendRequests");
        }

        public async Task<IActionResult> userRecord()
        {
            var username = HttpContext.Session.GetString("userName");
            var user = _context.Accounts.Where(b => b.UserName == username).FirstOrDefault();
            var lMSContext = _context.LendRequests.Where(b => b.UserId == user.UserId).Include(l => l.Book).Include(l => l.User);
            return View(await lMSContext.ToListAsync());
        }


        private bool LendRequestExists(int id)
        {
            return _context.LendRequests.Any(e => e.LendId == id);
        }
    }
}
