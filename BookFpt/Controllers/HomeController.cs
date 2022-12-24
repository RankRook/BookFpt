﻿using BookFpt.Areas.Identity.Data;
using BookFpt.Data;
using BookFpt.Models;
using BookFpt.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookFpt.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<SampleAppUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SampleAppContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            SampleAppContext context)
        {
            _logger = logger;
            this._context = context;
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        public async Task<IActionResult> Search(string Search)
        {


            if (_context.Book == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var books = from m in _context.Book
                        select m;

            if (!String.IsNullOrEmpty(Search))
            {
                books = books.Where(s => s.Name!.Contains(Search));
            }

            return View(await books.ToListAsync());
            //if (!String.IsNullOrEmpty(Search))
            //{
            //    var books = _context.Book.;
            //    return View(books.ToList());
            //}
            ///*return View(await _context.Movie.ToListAsync());*/
            //return RedirectToAction(nameof(Index));

        }

        public IActionResult Index()
        {
            var _books = _context.Book
                .Select(
                    p => new BookVM
                    {
                        BookId = p.Id,
                        BookName = p.Name,
                        Author = p.Author,
                        Category = p.Genre,
                        Price = p.Price,
                        ProfilePicture = p.BookFileName
                    }
                );
            return View(_books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}