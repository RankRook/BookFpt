using BookFpt.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BookFpt.Areas.Identity.Pages.Users
{
    /*[Authorize (Roles="Admin")]*/
    public class UserModel : PageModel
    {
        private readonly UserManager<SampleAppUser> _userManager;

        public UserModel(UserManager<SampleAppUser> userManager)
        {
            _userManager = userManager;
        }
        public class UserAndRole : SampleAppUser
        {
            public string RoleNames { get; set; }
        }
        public List<UserAndRole> users { get; set; }

        public static int ITEMS_PER_PAGE => 10;
        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }
        public int countPages { get; set; }
        public int totalUser { get; set; }
        public async Task OnGet()
        {
            /* users = await _userManager.Users.OrderBy(u => u.UserName).ToListAsync();*/
            var qr = _userManager.Users.OrderBy(u => u.UserName);
            totalUser = await qr.CountAsync();
            countPages = (int)Math.Ceiling((double)totalUser / ITEMS_PER_PAGE);
            if (currentPage < 1)
                currentPage = 1;
            if (currentPage > countPages)
                currentPage = countPages;
            var qr1 = qr.Skip((currentPage - 1) * ITEMS_PER_PAGE)
                         .Take(ITEMS_PER_PAGE)
                         .Select(u => new UserAndRole()
                         {
                             Id = u.Id,
                             UserName = u.UserName,
                         });
            users = await qr1.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleNames = string.Join(",", roles);
            }
        }


    }
}
