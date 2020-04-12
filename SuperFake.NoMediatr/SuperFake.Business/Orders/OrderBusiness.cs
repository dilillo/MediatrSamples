using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperFake.Business
{
    public class OrderBusiness
    {
        private readonly SuperFakeDbContext _dbContext;

        public OrderBusiness(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Order>> GetAllOrders()
        {
            return _dbContext.Orders
                .Include(i => i.Customer)
                .Include(i => i.OrderItems)
                    .ThenInclude(i => i.Product)
                .ToListAsync();
        }

        public Task<Order> GetOrderDetails(int orderID)
        {
            return _dbContext.Orders
                .Include(i => i.Customer)
                .Include(i => i.OrderItems)
                    .ThenInclude(i2 => i2.Product)
                .FirstOrDefaultAsync(i => i.ID == orderID);
        }

        public Task<OrderItem> GetOrderItemDetails(int orderItemID)
        {
            return _dbContext.OrderItems
                .Include(i => i.Product)
                .Include(i => i.Order)
                .FirstOrDefaultAsync(i => i.ID == orderItemID);
        }

        public Task<bool> OrderExists(int orderID)
        {
            return _dbContext.Orders.AnyAsync(e => e.ID == orderID);
        }

        public async Task CreateOrder(Order order)
        {
            await VerifyCustomerExists(order.CustomerID);

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            await VerifyOrderExists(order.ID);

            await VerifyOrderHasNotShipped(order.ID);

            _dbContext.Update(order);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrder(int orderID)
        {
            await VerifyOrderExists(orderID);

            await VerifyOrderHasNotShipped(orderID);

            var order = await _dbContext.Orders.FindAsync(orderID);

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync();
        }

        private async Task VerifyOrderHasNotShipped(int orderID)
        {
            var orderIsShipped = await _dbContext.Orders.AnyAsync(i => i.ID == orderID && i.OrderStatus == OrderStatuses.Shipped);

            if (orderIsShipped)
                throw new OrderIsShippedAndCannotBeChangedException();
        }

        public async Task AddOrderItem(OrderItem orderItem)
        {
            await VerifyOrderExists(orderItem.OrderID);

            await VerifyOrderHasNotShipped(orderItem.OrderID);

            await VerifyProductExists(orderItem.ProductID);

            await VerifyOrderItemProductIsUnique(orderItem);

            _dbContext.OrderItems.Add(orderItem);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderItem(OrderItem orderItem)
        {
            await VerifyOrderItemExists(orderItem.ID);

            await VerifyOrderExists(orderItem.OrderID);

            await VerifyOrderHasNotShipped(orderItem.OrderID);

            await VerifyProductExists(orderItem.ProductID);

            _dbContext.Update(orderItem);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveOrderItem(int orderItemID)
        {
            await VerifyOrderItemExists(orderItemID);

            var orderItem = await _dbContext.OrderItems.FindAsync(orderItemID);

            await VerifyOrderExists(orderItem.OrderID);

            await VerifyOrderHasNotShipped(orderItem.OrderID);

            _dbContext.OrderItems.Remove(orderItem);

            await _dbContext.SaveChangesAsync();
        }

        private async Task VerifyOrderExists(int orderID)
        {
            var orderExists = await _dbContext.Orders.AnyAsync(e => e.ID == orderID);

            if (!orderExists)
                throw new OrderDoesNotExistException();
        }

        private async Task VerifyCustomerExists(int customerID)
        {
            var customerExists = await _dbContext.Customers.AnyAsync(e => e.ID == customerID);

            if (!customerExists)
                throw new CustomerDoesNotExistException();
        }

        private async Task VerifyProductExists(int productID)
        {
            var productExists = await _dbContext.Products.AnyAsync(e => e.ID == productID);

            if (!productExists)
                throw new ProductDoesNotExistException();
        }

        private async Task VerifyOrderItemExists(int orderItemID)
        {
            var orderItemExists = await _dbContext.OrderItems.AnyAsync(e => e.ID == orderItemID);

            if (!orderItemExists)
                throw new OrderItemDoesNotExistException();
        }

        private async Task VerifyOrderItemProductIsUnique(OrderItem orderItem)
        {
            var exstingOrderItemForSameProductInOrder = await _dbContext.OrderItems.AnyAsync(i => i.ID != orderItem.ID && i.OrderID == orderItem.OrderID && i.ProductID == orderItem.ProductID);

            if (exstingOrderItemForSameProductInOrder)
                throw new OrderItemAlreadyExistsForThatProductException();
        }

    }
}
