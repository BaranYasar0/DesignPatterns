using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StrategyPattern.WebApp.Models;
using StrategyPattern.WebApp.Repositories;

namespace StrategyPattern.WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly UserManager<AppUser> userManager;
        public ProductController(IProductRepository productRepository, UserManager<AppUser> userManager)
        {
            this.productRepository = productRepository;
            this.userManager = userManager;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return View(await productRepository.GetAllByUserId(user.Id));
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock,UserId,CreatedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid().ToString();
                product.UserId = (await userManager.FindByNameAsync(HttpContext.User.Identity.Name)).Id;
                product.CreatedDate = DateTime.Now;
                await productRepository.Save(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Price,Stock,UserId,CreatedDate")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await productRepository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var product = await productRepository.GetById(id);
            if (product != null)
            {
                await productRepository.Delete(product);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return productRepository.GetById(id) != null;
        }
    }
}
