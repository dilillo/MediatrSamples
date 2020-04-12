using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperFake.Business;
using SuperFake.Data;
using System.Threading.Tasks;

namespace SuperFake.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ProductBusiness _productBusiness;

        public ProductsController(ProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _productBusiness.GetAllProducts());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productBusiness.GetProductDetails(id.GetValueOrDefault());

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Category,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (await CallBusinessTaskSafe(_productBusiness.CreateProduct(product)))
                    return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productBusiness.GetProductDetails(id.GetValueOrDefault());

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Category,Description,Price")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await CallBusinessTaskSafe(_productBusiness.UpdateProduct(product)))
                    return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productBusiness.GetProductDetails(id.GetValueOrDefault());

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
            if (await CallBusinessTaskSafe(_productBusiness.DeleteProduct(id)))
                return RedirectToAction(nameof(Index));

            var product = await _productBusiness.GetProductDetails(id);

            if (product == null)
            {
                return NotFound();
            }

            return View("Delete", product);
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _productBusiness.ProductExists(id);
        }
    }
}
