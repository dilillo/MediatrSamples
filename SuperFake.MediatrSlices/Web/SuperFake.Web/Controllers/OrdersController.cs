using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrdersData = SuperFake.Orders.Data;
using SuperFake.Web.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperFake.Orders.Domain;
using SuperFake.Shared.Data;

namespace SuperFake.Web.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new GetAllOrdersV1Query()));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _mediator.Send(new GetOrderDetailsV1Query { OrderID = id.GetValueOrDefault() });

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(int? customerID)
        {
            ViewData["CustomerID"] = new SelectList(await _mediator.Send(new GetAllCustomersV1Query()), "ID", "FullName");

            if (customerID.HasValue)
            {
                return View(new OrdersData.Order { CustomerID = customerID.Value });
            }

            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID")] OrdersData.Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now.Date;
                order.OrderStatus = OrderStatuses.Received;

                if (await ExecuteCommandSafe(_mediator.Send(new CreateOrderV1Command { Order = order })))
                    return RedirectToAction(nameof(Edit), new { id = order.ID });
            }

            ViewData["CustomerID"] = new SelectList(await _mediator.Send(new GetAllCustomersV1Query()), "ID", "FullName");

            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _mediator.Send(new GetOrderDetailsV1Query { OrderID = id.GetValueOrDefault() });

            if (order == null)
            {
                return NotFound();
            }

            ViewData["CustomerID"] = new SelectList(await _mediator.Send(new GetAllCustomersV1Query()), "ID", "FullName", order.CustomerID);

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
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustomerID,OrderDate,OrderStatus")] OrdersData.Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await ExecuteCommandSafe(_mediator.Send(new UpdateOrderV1Command { Order = order })))
                    return RedirectToAction(nameof(Index));
            }

            var webOrder = await _mediator.Send(new GetOrderDetailsV1Query { OrderID = id });

            var orderStatuses = new List<SelectListItem>
            {
                new SelectListItem(nameof(OrderStatuses.Received), ((int)OrderStatuses.Received).ToString()),
                new SelectListItem(nameof(OrderStatuses.Shipped), ((int)OrderStatuses.Shipped).ToString()),
                new SelectListItem(nameof(OrderStatuses.Cancelled), ((int)OrderStatuses.Cancelled).ToString())
            };

            ViewData["OrderStatus"] = new SelectList(orderStatuses, "Value", "Text", webOrder.OrderStatus);

            return View(webOrder);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _mediator.Send(new GetOrderDetailsV1Query { OrderID = id.GetValueOrDefault() });

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
            if (await ExecuteCommandSafe(_mediator.Send(new DeleteOrderV1Command { OrderID = id })))
                return RedirectToAction(nameof(Index));

            var order = await _mediator.Send(new GetOrderDetailsV1Query { OrderID = id });

            if (order == null)
            {
                return NotFound();
            }

            return View("Delete", order);
        }

        private Task<bool> OrderExists(int id)
        {
            return _mediator.Send(new OrderExistsV1Query { OrderID = id });
        }
    }
}
