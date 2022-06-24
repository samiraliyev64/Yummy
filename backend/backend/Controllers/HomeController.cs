using backend.DAL;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel
            {
                FoodSummary = _context.FoodSummary.FirstOrDefault(),
                Products = _context.Products.ToList(),
                BookTable = _context.BookTable.FirstOrDefault()
            };
            return View(home);
        }
    }
}
