using backend.DAL;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class UserController : Controller
    {
        private AppDbContext _context { get; }
        public UserController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Users);
        }
    }
}
