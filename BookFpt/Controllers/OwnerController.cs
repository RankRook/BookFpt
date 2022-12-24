using BookFpt.Data;
using BookFpt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookFpt.Controllers
{
    public class OwnerController : Controller
    {
        private readonly SampleAppContext context;

        public OwnerController(SampleAppContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Request()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "roleOwner")]
        public IActionResult Request(CategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                context.Add(request);
                context.SaveChanges();
                TempData["Message"] = "Request new Category";
                return RedirectToAction("MakeRequest");
            }
            else
            {
                return View(request);
            }
        }
    }
}
