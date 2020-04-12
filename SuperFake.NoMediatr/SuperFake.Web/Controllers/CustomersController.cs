using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperFake.Business;
using SuperFake.Data;

namespace SuperFake.Web.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly CustomerBusiness _customerBusiness;

        public CustomersController(CustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _customerBusiness.GetAllCustomers());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerBusiness.GetCustomerDetails(id.GetValueOrDefault());

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
            try
            {
                if (ModelState.IsValid)
                {
                    if (await CallBusinessTaskSafe( _customerBusiness.CreateCustomer(customer)))
                        return RedirectToAction(nameof(Index));
                }
            }
            catch (BusinessException bx)
            {
                ModelState.AddModelError("", bx.Message);
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

            var customer = await _customerBusiness.GetCustomerDetails(id.GetValueOrDefault());

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
                if (await CallBusinessTaskSafe(_customerBusiness.UpdateCustomer(customer)))
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

            var customer = await _customerBusiness.GetCustomerDetails(id.GetValueOrDefault());

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
            if (await CallBusinessTaskSafe(_customerBusiness.DeleteCustomer(id)))
                return RedirectToAction(nameof(Index));

            var customer = await _customerBusiness.GetCustomerDetails(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View("Delete", customer);
        }

        private Task<bool> CustomerExists(int id)
        {
            return _customerBusiness.CustomerExists(id);
        }
    }
}
