using backend.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Helpers;
using backend.Models;

namespace backend.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //Read
        public IActionResult Index()
        {
            return View(_context.Products);
        }
        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }
            var path = Helper.GetPath(_env.WebRootPath, "images", product.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Create (GET)
        public IActionResult Create()
        {
            return View();
        }
        //Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!product.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "File size must be less than 200 KB");
                return View();
            }
            if (!product.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image");
                return View();
            }
            product.Image = await product.Photo.SaveFileAsync(_env.WebRootPath, "images");
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        //Update (GET)
        public IActionResult Update(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Product product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);

        }
        
        //Update (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Product productDb = _context.Products.Find(id);
            if(productDb == null)
            {
                return NotFound();
            }
            product.Image = await product.Photo.SaveFileAsync(_env.WebRootPath, "images");
            var pathDb = Helper.GetPath(_env.WebRootPath, "images", productDb.Image);
            if (System.IO.File.Exists(pathDb))
            {
                System.IO.File.Delete(pathDb);
            }
            productDb.Image = product.Image;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
