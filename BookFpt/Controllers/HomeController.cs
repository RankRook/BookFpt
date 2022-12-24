using BookFpt.Areas.Identity.Data;
using BookFpt.Data;
using BookFpt.Models;
using BookFpt.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                        ProfilePicture = p.BookImagePath
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}