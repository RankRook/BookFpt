using BookFpt.Data;
using BookFpt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FptBookOke.Controllers {
    [Authorize(Policy = "roleAdmin")]
    public class AdminController : Controller
{
    private readonly SampleAppContext context;

    public AdminController(SampleAppContext context)
    {
        this.context = context;
    }

    [Route("/admin/index")]
    public IActionResult CategoryRequest()
    {
        return View(context.CategoryRequest.ToList());
    }

    public IActionResult Accept(int id)
    {
        var request = context.CategoryRequest.Find(id);
        request.Status = 1;
        context.CategoryRequest.Update(request);
        context.SaveChanges();
        TempData["Genre"] = request.Name;
        return RedirectToAction("AddFromRequest");
    }

    public IActionResult Reject(int id)
    {
        var request = context.CategoryRequest.Find(id);
        request.Status = -1;
        context.CategoryRequest.Update(request);
        context.SaveChanges();
        TempData["Message"] = "Request is rejected";
        return RedirectToAction("CategoryRequest");
    }

    public IActionResult AddFromRequest()
    {
        Category category = new Category();
        if (ModelState.IsValid)
        {
            category.CategoryName = (string)TempData["Category"];
            context.Add(category);
            context.SaveChanges();
            TempData["Message"] = "Add new data successfully";
            return RedirectToAction("CategoryRequest");
        }
        else
        {
            return RedirectToAction("CategoryRequest");
        }
    }
}
}
