using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperFake.Business;
using SuperFake.Data;
using System.Threading.Tasks;

namespace SuperFake.Web.Controllers
{
    public class OrderItemsController : BaseController
    {
        private readonly OrderBusiness _orderBusiness;
        private readonly ProductBusiness _productBusiness;

        public OrderItemsController(OrderBusiness orderBusiness, ProductBusiness productBusiness)
        {
            _orderBusiness = orderBusiness;
            _productBusiness = productBusiness;
        }

        public async Task<IActionResult> Create(int orderID)
        {
            ViewData["ProductID"] = new SelectList(await _productBusiness.GetAllProducts(), "ID", "Name");

            var model = new OrderItem { OrderID = orderID, Quantity = 1 };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,ProductID,Quantity")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                if (await CallBusinessTaskSafe(_orderBusiness.AddOrderItem(orderItem)))
                    return RedirectToAction("Edit", "Orders", new { id = orderItem.OrderID });
            }

            ViewData["ProductID"] = new SelectList(await _productBusiness.GetAllProducts(), "ID", "Name");

            return View(orderItem);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var orderItem = await _orderBusiness.GetOrderItemDetails(id.GetValueOrDefault());

            if (orderItem == null)
            {
                return NotFound();
            }

            ViewData["ProductID"] = new SelectList(await _productBusiness.GetAllProducts(), "ID", "Name");

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
                if (await CallBusinessTaskSafe(_orderBusiness.UpdateOrderItem(orderItem)))
                    return RedirectToAction("Edit", "Orders", new { id = orderItem.OrderID });
            }

            ViewData["ProductID"] = new SelectList(await _productBusiness.GetAllProducts(), "ID", "Name");

            return View(orderItem);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int id, int orderID)
        {
            if (await CallBusinessTaskSafe(_orderBusiness.RemoveOrderItem(id)))
                return RedirectToAction("Edit", "Orders", new { id = orderID });

            var orderItem = await _orderBusiness.GetOrderItemDetails(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            ViewData["ProductID"] = new SelectList(await _productBusiness.GetAllProducts(), "ID", "Name");

            return View("Delete", orderItem);
        }
    }
}