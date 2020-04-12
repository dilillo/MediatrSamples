using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Data;
using SuperFake.Domains;
using System.Threading.Tasks;

namespace SuperFake.Web.Controllers
{
    public class ProductsController : BaseController
    {
        //private readonly ProductBusiness _productBusiness;
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
            //_productBusiness = productBusiness;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //return View(await _productBusiness.GetAllProducts());
            return View(await _mediator.Send(new GetAllProductsV1Query()));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _productBusiness.GetProductDetails(id.GetValueOrDefault());
            var product = await _mediator.Send(new GetProductDetailsV1Query { ProductID = id.GetValueOrDefault() });

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
                //if (await CallBusinessTaskSafe(_productBusiness.CreateProduct(product)))
                if (await ExecuteCommandSafe(_mediator.Send(new CreateProductV1Command { Product = product })))
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

            var product = await _mediator.Send(new GetProductDetailsV1Query { ProductID = id.GetValueOrDefault() });

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
                //if (await CallBusinessTaskSafe(_productBusiness.UpdateProduct(product)))
                if (await ExecuteCommandSafe(_mediator.Send(new UpdateProductV1Command { Product = product } )))
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

            var product = await _mediator.Send(new GetProductDetailsV1Query { ProductID = id.GetValueOrDefault() });

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
            if (await ExecuteCommandSafe(_mediator.Send(new DeleteProductV1Command { ProductID = id })))
                return RedirectToAction(nameof(Index));

            var product = await _mediator.Send(new GetProductDetailsV1Query { ProductID = id });

            if (product == null)
            {
                return NotFound();
            }

            return View("Delete", product);
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _mediator.Send(new ProductExistsV1Query { ProductID = id });
        }
    }
}
