using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace SuperFake.Domains
{
    public class UpdateOrderItemV1CommandHandler : IRequestHandler<UpdateOrderItemV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public UpdateOrderItemV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateOrderItemV1Command request, CancellationToken cancellationToken)
        {
            await VerifyOrderItemExists(request.OrderItem.ID);

            await VerifyOrderExists(request.OrderItem.OrderID);

            await VerifyOrderHasNotShipped(request.OrderItem.OrderID);

            await VerifyProductExists(request.OrderItem.ProductID);

            _dbContext.Update(request.OrderItem);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyOrderExists(int orderID)
        {
            var orderExists = await _dbContext.Orders.AnyAsync(e => e.ID == orderID);

            if (!orderExists)
                throw new UpdateOrderItemOrderDoesNotExistException();
        }

        private async Task VerifyProductExists(int productID)
        {
            var productExists = await _dbContext.Products.AnyAsync(e => e.ID == productID);

            if (!productExists)
                throw new UpdateOrderItemProductDoesNotExistException();
        }

        private async Task VerifyOrderItemExists(int orderItemID)
        {
            var orderItemExists = await _dbContext.OrderItems.AnyAsync(e => e.ID == orderItemID);

            if (!orderItemExists)
                throw new UpdateOrderItemDoesNotExistException();
        }

        private async Task VerifyOrderHasNotShipped(int orderID)
        {
            var orderIsShipped = await _dbContext.Orders.AnyAsync(i => i.ID == orderID && i.OrderStatus == OrderStatuses.Shipped);

            if (orderIsShipped)
                throw new UpdateOrderItemOrderIsShippedAndCannotBeChangedException();
        }
    }
}
