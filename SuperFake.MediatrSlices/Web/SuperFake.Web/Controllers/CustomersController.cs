using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Customers.Data;
using SuperFake.Customers.Domain;
using SuperFake.Web.Domain;
using System.Threading.Tasks;

namespace SuperFake.Web.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new GetAllCustomersV1Query()));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _mediator.Send(new GetCustomerDetailsV1Query { CustomerID = id.GetValueOrDefault() });

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,FullName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (await ExecuteCommandSafe( _mediator.Send(new CreateCustomerV1Command { Customer = customer })))
                    return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _mediator.Send(new GetCustomerDetailsV1Query { CustomerID = id.GetValueOrDefault() });

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,FullName")] Customer customer)
        {
            if (id != customer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await ExecuteCommandSafe(_mediator.Send(new UpdateCustomerV1Command { Customer = customer })))
                    return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _mediator.Send(new GetCustomerDetailsV1Query { CustomerID = id.GetValueOrDefault() });

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await ExecuteCommandSafe(_mediator.Send(new DeleteCustomerV1Command { CustomerID = id })))
                return RedirectToAction(nameof(Index));

            var customer = await _mediator.Send(new GetCustomerDetailsV1Query { CustomerID = id });

            if (customer == null)
            {
                return NotFound();
            }

            return View("Delete", customer);
        }

        private Task<bool> CustomerExists(int id)
        {
            return _mediator.Send(new CustomerExistsV1Query { CustomerID = id });
        }
    }
}
