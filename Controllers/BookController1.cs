// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using moment3_mvc_entity.Models;

// namespace moment3_mvc_entity.Controllers
// {
//     public class BookController : Controller
//     {
//         private readonly AppDbContext _context;
//         private readonly IWebHostEnvironment _hostEnvironment;
//         private readonly string wwwRootPath;

//         public BookController(AppDbContext context, IWebHostEnvironment hostEnvironment)
//         {
//             _context = context;
//             _hostEnvironment = hostEnvironment;
//             wwwRootPath = hostEnvironment.WebRootPath;
//         }

//         // GET: Book
//         public async Task<IActionResult> Index()
//         {
//             var appDbContext = _context.Book.Include(b => b.Author);
//             return View(await appDbContext.ToListAsync());
//         }

//         // GET: Book/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var book = await _context.Book
//                 .Include(b => b.Author)
//                 .FirstOrDefaultAsync(m => m.BookId == id);
//             if (book == null)
//             {
//                 return NotFound();
//             }

//             return View(book);
//         }

//         // GET: Book/Create
//         public IActionResult Create()
//         {
//             ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "AuthorId", "AuthorId");
//             return View();
//         }

//         // POST: Book/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("BookId,Title,AuthorId,ImageFile")] Book book)
//         {
//             if (ModelState.IsValid)
//             {
//                 //check for image
//                 if (book.ImageFile != null){
//                     //generate new file name
//                     string fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
//                     string extension = Path.GetExtension(book.ImageFile.FileName);

//                     book.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssff") + extension;

//                     string path = Path.Combine(wwwRootPath + "/imgupload", fileName);

//                     //store in file system
//                     using(var fileStream = new FileStream(path, FileMode.Create)) {
//                         await book.ImageFile.CopyToAsync(fileStream);
//                     }
//                 } else{
//                     book.ImageName = "empty.jpg";
//                 }

//                 _context.Add(book);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "AuthorId", "AuthorId", book.AuthorId);
//             return View(book);
//         }

//         // GET: Book/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var book = await _context.Book.FindAsync(id);
//             if (book == null)
//             {
//                 return NotFound();
//             }
//             ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "AuthorId", "AuthorId", book.AuthorId);
//             return View(book);
//         }

//         // POST: Book/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,AuthorId,ImageName")] Book book)
//         {
//             if (id != book.BookId)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(book);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!BookExists(book.BookId))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "AuthorId", "AuthorId", book.AuthorId);
//             return View(book);
//         }

//         // GET: Book/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var book = await _context.Book
//                 .Include(b => b.Author)
//                 .FirstOrDefaultAsync(m => m.BookId == id);
//             if (book == null)
//             {
//                 return NotFound();
//             }

//             return View(book);
//         }

//         // POST: Book/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var book = await _context.Book.FindAsync(id);
//             if (book != null)
//             {
//                 _context.Book.Remove(book);
//             }

//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool BookExists(int id)
//         {
//             return _context.Book.Any(e => e.BookId == id);
//         }
//     }
// }
