using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using moment3_mvc_entity.Models;

namespace moment3_mvc_entity.Controllers
{
    public class RentalController : Controller
    {
        private readonly AppDbContext _context;

        public RentalController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Rental
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Rental.Include(r => r.Book);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Rental/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rental/Create
        // [Route("/Rental/Rentabook")]
        public IActionResult Create(int? Id)
        {
            //retive the BookId from detals.cshtml
            if (Id != null)
            {
                ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Title", Id);
                return View();
            }
            else
            {
                ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Title");
                return View();
            }
        }

        // POST: Rental/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalId,RentDate,ReturnDate,IsReturned,RenterName,RenterIdNumber,BookId,Email")] Rental rental)
        {

            //check availablity
            var isRented = _context.Rental.Any(r => r.BookId == rental.BookId && r.RentDate <= rental.ReturnDate && r.ReturnDate >= rental.RentDate);
            if (isRented)
            {
                ModelState.AddModelError(nameof(Rental.BookId), "The book is already rented out during your requested rental period");

            }
            //check so rental date is not past date
            if (rental.RentDate < DateTime.Now.AddDays(-1))
            {
                ModelState.AddModelError(nameof(Rental.RentDate), "Rental date cannot be a past date.");
            }
            //check so rental period is not more than one month
            if (rental.ReturnDate.HasValue && rental.ReturnDate.Value > rental.RentDate.AddMonths(1))
            {
                ModelState.AddModelError(nameof(Rental.ReturnDate), "Your rental period can maximum be of one month");
            }



            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Title", rental.BookId);
            return View(rental);
        }

        // GET: Rental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "BookId", rental.BookId);
            return View(rental);
        }

        // POST: Rental/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalId,RentDate,ReturnDate,IsReturned,RenterName,RenterIdNumber,BookId")] Rental rental)
        {
            if (id != rental.RentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalId))
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
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "BookId", rental.BookId);
            return View(rental);
        }

        // GET: Rental/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rental.FindAsync(id);
            if (rental != null)
            {
                _context.Rental.Remove(rental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rental.Any(e => e.RentalId == id);
        }
    }
}
