using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rozetka.Data;
using Rozetka.Data.Entity;
using Rozetka.Models.ViewModels.Products;

namespace Rozetka.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Brand).Include(p => p.Childcategory).ToListAsync();
            return View(products);
            //var dataContext = _context.Products.Include(p => p.Childcategory);
            //return View(await dataContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Childcategory)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create       
        public async Task<IActionResult> Create()
        {
            var brands = await _context.Brands.ToListAsync();
            var childcategories = await _context.Childcategories.ToListAsync();

            var viewModel = new CreateProductVM
            {
                Brands = new SelectList(brands, "Id", "Title"),
                Childcategories = new SelectList(childcategories, "Id", "Name")
                //Product = new Product()
            };

            return View(viewModel);
        }
        //public IActionResult Create()
        //{
        //    ViewData["ChildcategoryId"] = new SelectList(_context.Childcategories, "Id", "Id");
        //    return View();

        //}


        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductVM viewModel)
        {
            
            if (ModelState.IsValid)
            {
                _context.Products.Add(viewModel.Product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }           

            var brands = await _context.Brands.ToListAsync();
            var childcategories = await _context.Childcategories.ToListAsync();

            viewModel.Brands = new SelectList(brands, "Id", "Title");
            viewModel.Childcategories = new SelectList(childcategories, "Id", "Name");

            return View(viewModel);
        }
        

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var brands = await _context.Brands.ToListAsync();
            var childcategories = await _context.Childcategories.ToListAsync();
            var viewModel = new CreateProductVM
            {
                Product = product,
                Brands = new SelectList(brands, "Id", "Title"),
                Childcategories = new SelectList(childcategories, "Id", "Name")
            };
            //ViewData["ChildcategoryId"] = new SelectList(_context.Childcategories, "Id", "Id", product.ChildcategoryId);
            return View(viewModel);
            
            //return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateProductVM viewModel)
        {
            if (id != viewModel.Product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(viewModel.Product.Id))
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

            var brands = await _context.Brands.ToListAsync();
            var childcategories = await _context.Childcategories.ToListAsync();

            viewModel.Brands = new SelectList(brands, "Id", "Title");
            viewModel.Childcategories = new SelectList(childcategories, "Id", "Name");

            return View(viewModel);
        }   

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Childcategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }   

        public async Task<IActionResult> GetProduct(int id)
        {           
            if (id <= 0)
            {
                return BadRequest();
            }

            // Найти продукт по Id и загрузить связанные данные
            var product = await _context.Products
                                        .Include(p => p.Brand)
                                        .Include(p => p.Childcategory)
                                        .Include(p => p.ProductImages)
                                        .Include(p => p.Reviews)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }
    }
}
