﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookFpt.Helpers;
using BookFpt.Models;
using Microsoft.AspNetCore.Identity;
using BookFpt.Areas.Identity.Data;

namespace FptBookOke.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        
    }
}
