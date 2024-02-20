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
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string wwwRootPath;

        public BookController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            wwwRootPath = hostEnvironment.WebRootPath;
        }



        // GET: Book
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Book.ToListAsync());
        // }



        // GET: All books
        public async Task<IActionResult> Allbooks(string searchString)
        {
            //if A search in interface is made
            if (!string.IsNullOrEmpty(searchString))
            {
                var searchResults = _context.Book
                    .Include(b => b.Author)
                    .Where(b => (b.Title != null && b.Title.ToLower().Contains(searchString.ToLower()))
                 || (b.Author != null && b.Author.Name != null && b.Author.Name.ToLower().Contains(searchString.ToLower()))
                 || (b.Genre != null && b.Genre.ToLower().Contains(searchString.ToLower())))
                    .ToList();


                ViewBag.searchString = searchString;

                return View(searchResults);
            }

            //if NO search in interface is made
            var allBooks = _context.Book
                .Include(b => b.Author)
                .ToList();
            return View(allBooks);
        }




        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id, int? AuthorId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            //send some data from Rental table
            var rentalDates = await _context.Rental
                .Where(r => r.BookId == id && r.ReturnDate.HasValue && r.ReturnDate.Value >= DateTime.Today)
                .Select(r => new { r.RentDate, r.ReturnDate })
                .ToListAsync();
            ViewBag.RentalDates = rentalDates;


            //send some data from Author table
            var author = await _context.Book
             .Include(b => b.Author)
             .FirstOrDefaultAsync(m => m.BookId == id);
            ViewBag.AuthorName = author.Author?.Name;
            ViewBag.AuthorId = author.Author?.AuthorId;

            //send some data from Rental table

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "Name");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,AuthorName,ImageFile,Description,Grade,Genre,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {



                // Check if the selected author exists
                Author? author = _context.Author.FirstOrDefault(a => a.AuthorId == book.AuthorId);

                if (author == null)
                {
                    // Handle the case where the selected author doesn't exist 
                    ModelState.AddModelError("AuthorId", "Selected author does not exist.");
                    ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "Name", book.AuthorId);
                    return View(book);
                }

                // Set the Author property of the book
                book.Author = author;




                //check for image
                if (book.ImageFile != null)
                {
                    //generate new file name
                    string fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
                    string extension = Path.GetExtension(book.ImageFile.FileName);

                    book.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssff") + extension;

                    string path = Path.Combine(wwwRootPath + "/imgupload", fileName);

                    //store in file system
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await book.ImageFile.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    book.ImageName = "empty.jpg";
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "Name");

            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,AuthorName,ImageFile,Description,Grade,Genre, AuthorId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {




                // Check if the selected author exists
                Author? author = _context.Author.FirstOrDefault(a => a.AuthorId == book.AuthorId);

                if (author == null)
                {
                    // Handle the case where the selected author doesn't exist 
                    ModelState.AddModelError("AuthorId", "Selected author does not exist.");
                    ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "Name", book.AuthorId);
                    return View(book);
                }

                // Set the Author property of the book
                book.Author = author;






                //check for image
                if (book.ImageFile != null)
                {
                    //generate new file name
                    string fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
                    string extension = Path.GetExtension(book.ImageFile.FileName);

                    book.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssff") + extension;

                    string path = Path.Combine(wwwRootPath + "/imgupload", fileName);

                    //store in file system
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await book.ImageFile.CopyToAsync(fileStream);
                    }
                }

                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            return View(book);
        }




        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }




        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }
    }
}
