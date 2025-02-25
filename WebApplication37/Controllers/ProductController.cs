using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication37.Data;
using WebApplication37.Models;

namespace WebApplication37.Controllers
{
    public class ProductController : Controller
    {
        private readonly string _uploadsFolderPath;
        private readonly Mycontext _context;

        public ProductController(IWebHostEnvironment env, Mycontext context)
        {
            _uploadsFolderPath = Path.Combine(env.WebRootPath, "images");
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _context.product.ToListAsync();
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.product = product;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> addTocart(AddTocartVM model)
        {

            if (ModelState.IsValid)
            {
                var addTocard = new AddTocart();
                addTocard.productId = model.productId;
                addTocard.UserId = model.UserId;
                addTocard.QTY = model.QTY;
                addTocard.UnitPrice = model.UnitPrice;

                _context.addtocard.Add(addTocard);
                _context.SaveChanges();
            }
            return RedirectToAction("cart");
        }
        public async Task<IActionResult> cart()
        {
           var userId= HttpContext.Session.GetInt32("UserId");

            var cart = _context.addtocard.Where(x=>x.UserId== userId).Include(x => x.product);

            return View(cart);
        }
        // GET: Product/Create
        public IActionResult Create()
        {
            var categories=_context.Cate.ToList();
            ViewBag.Categories = categories;

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.image != null && model.image.Length > 0)
                {
                    // Create the uploads folder if it doesn't exist
                    if (!Directory.Exists(_uploadsFolderPath))
                    { 
                        Directory.CreateDirectory(_uploadsFolderPath);
                    }

                    // Generate a unique filename
                    var fileName = Path.GetFileName(model.image.FileName);
                    var filePath = Path.Combine(_uploadsFolderPath, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.image.CopyToAsync(stream);
                    }

                    // Create and save the product
                    var product = new Product
                    {
                        Price = model.Price,
                        Name = model.Name,
                        Description = model.Description,
                        ImagePath = fileName,
                        CategoryId = model.CategoryId
                    };

                    _context.product.Add(product);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "File uploaded successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No file selected!");
                }
            }

            // If model state is invalid, redisplay the form with validation errors
            return View(model);
        }


        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ImagePath")] Product model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.product.FindAsync(id);
            if (product != null)
            {
                _context.product.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> hello()
        {
            return View();
        }

        private bool ProductExists(int id)
        {
            return _context.product.Any(e => e.Id == id);
        }
    }
}
