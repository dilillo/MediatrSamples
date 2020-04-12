using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperFake.Domains;
using SuperFake.Data;
using System.Threading.Tasks;

namespace SuperFake.Web.Controllers
{
    public class OrderItemsController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Create(int orderID)
        {
            ViewData["ProductID"] = new SelectList(await _mediator.Send(new GetAllProductsV1Query()), "ID", "Name");

            var model = new OrderItem { OrderID = orderID, Quantity = 1 };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,ProductID,Quantity")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                if (await ExecuteCommandSafe(_mediator.Send(new CreateOrderItemV1Command { OrderItem = orderItem })))
                    return RedirectToAction("Edit", "Orders", new { id = orderItem.OrderID });
            }

            ViewData["ProductID"] = new SelectList(await _mediator.Send(new GetAllProductsV1Query()), "ID", "Name");

            return View(orderItem);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var orderItem = await  _mediator.Send(new GetOrderItemDetailsV1Query { OrderItemID = id.GetValueOrDefault() });

            if (orderItem == null)
            {
                return NotFound();
            }

            ViewData["ProductID"] = new SelectList(await _mediator.Send(new GetAllProductsV1Query()), "ID", "Name");

            return View(orderItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OrderID,ProductID,Quantity")] OrderItem orderItem)
        {
            if (id != orderItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await ExecuteCommandSafe(_mediator.Send(new UpdateOrderItemV1Command { OrderItem = orderItem })))
                    return RedirectToAction("Edit", "Orders", new { id = orderItem.OrderID });
            }

            ViewData["ProductID"] = new SelectList(await _mediator.Send(new GetAllProductsV1Query()), "ID", "Name");

            return View(orderItem);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int id, int orderID)
        {
            if (await ExecuteCommandSafe(_mediator.Send(new DeleteOrderItemV1Command { OrderItemID = id })))
                return RedirectToAction("Edit", "Orders", new { id = orderID });

            var orderItem = await _mediator.Send(new GetOrderItemDetailsV1Query { OrderItemID = id });

            if (orderItem == null)
            {
                return NotFound();
            }

            ViewData["ProductID"] = new SelectList(await _mediator.Send(new GetAllProductsV1Query()), "ID", "Name");

            return View("Delete", orderItem);
        }
    }
}