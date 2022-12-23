using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using BookFpt.Models;

namespace BookFpt.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the SampleAppUser class
    public class SampleAppUser : IdentityUser
    {
        public SampleAppUser()
        {
            Orders = new HashSet<Order>();
        }
        public ICollection<Order> Orders { get; set; }
    }
}
