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
    public class OrdersController : BaseController
    {
        private readonly OrderBusiness _orderBusiness;
        private readonly CustomerBusiness _customerBusiness;
        private readonly ProductBusiness _productBusiness;

        public OrdersController(OrderBusiness orderBusiness, CustomerBusiness customerBusiness, ProductBusiness productBusiness)
        {
            _orderBusiness = orderBusiness;
            _customerBusiness = customerBusiness;
            _productBusiness = productBusiness;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _orderBusiness.GetAllOrders());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderBusiness.GetOrderDetails(id.GetValueOrDefault());

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(int? customerID)
        {
            ViewData["CustomerID"] = new SelectList(await _customerBusiness.GetAllCustomers(), "ID", "FullName");

            if (customerID.HasValue)
            {
                return View(new Order { CustomerID = customerID.Value });
            }

            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now.Date;
                order.OrderStatus = OrderStatuses.Received;

                if (await CallBusinessTaskSafe(_orderBusiness.CreateOrder(order)))
                    return RedirectToAction(nameof(Edit), new { id = order.ID });
            }

            ViewData["CustomerID"] = new SelectList(await _customerBusiness.GetAllCustomers(), "ID", "FullName");

            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderBusiness.GetOrderDetails(id.GetValueOrDefault());

            if (order == null)
            {
                return NotFound();
            }

            ViewData["CustomerID"] = new SelectList(await _customerBusiness.GetAllCustomers(), "ID", "FullName", order.CustomerID);

            var orderStatuses = new List<SelectListItem>
            {
                new SelectListItem(nameof(OrderStatuses.Received), ((int)OrderStatuses.Received).ToString()),
                new SelectListItem(nameof(OrderStatuses.Shipped), ((int)OrderStatuses.Shipped).ToString()),
                new SelectListItem(nameof(OrderStatuses.Cancelled), ((int)OrderStatuses.Cancelled).ToString())
            };

            ViewData["OrderStatus"] = new SelectList(orderStatuses, "Value", "Text", order.OrderStatus);

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustomerID,OrderDate,OrderStatus")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await CallBusinessTaskSafe(_orderBusiness.UpdateOrder(order)))
                    return RedirectToAction(nameof(Index));
            }

            order = await _orderBusiness.GetOrderDetails(id);

            var orderStatuses = new List<SelectListItem>
            {
                new SelectListItem(nameof(OrderStatuses.Received), ((int)OrderStatuses.Received).ToString()),
                new SelectListItem(nameof(OrderStatuses.Shipped), ((int)OrderStatuses.Shipped).ToString()),
                new SelectListItem(nameof(OrderStatuses.Cancelled), ((int)OrderStatuses.Cancelled).ToString())
            };

            ViewData["OrderStatus"] = new SelectList(orderStatuses, "Value", "Text", order.OrderStatus);

            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderBusiness.GetOrderDetails(id.GetValueOrDefault());

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await CallBusinessTaskSafe(_orderBusiness.DeleteOrder(id)))
                return RedirectToAction(nameof(Index));

            var order = await _orderBusiness.GetOrderDetails(id);

            if (order == null)
            {
                return NotFound();
            }

            return View("Delete", order);
        }

        private Task<bool> OrderExists(int id)
        {
            return _orderBusiness.OrderExists(id);
        }
    }
}
