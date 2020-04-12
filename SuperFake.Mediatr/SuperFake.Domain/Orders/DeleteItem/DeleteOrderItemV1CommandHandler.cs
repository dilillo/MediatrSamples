using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class DeleteOrderItemV1CommandHandler : IRequestHandler<DeleteOrderItemV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public DeleteOrderItemV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteOrderItemV1Command request, CancellationToken cancellationToken)
        {
            await VerifyOrderItemExists(request.OrderItemID);

            var orderItem = await _dbContext.OrderItems.FindAsync(request.OrderItemID);

            await VerifyOrderExists(orderItem.OrderID);

            await VerifyOrderHasNotShipped(orderItem.OrderID);

            _dbContext.OrderItems.Remove(orderItem);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyOrderItemExists(int orderItemID)
        {
            var orderItemExists = await _dbContext.OrderItems.AnyAsync(e => e.ID == orderItemID);

            if (!orderItemExists)
                throw new DeleteOrderItemDoesNotExistException();
        }

        private async Task VerifyOrderExists(int orderID)
        {
            var orderExists = await _dbContext.Orders.AnyAsync(e => e.ID == orderID);

            if (!orderExists)
                throw new DeleteOrderItemOrderDoesNotExistException();
        }

        private async Task VerifyOrderHasNotShipped(int orderID)
        {
            var orderIsShipped = await _dbContext.Orders.AnyAsync(i => i.ID == orderID && i.OrderStatus == OrderStatuses.Shipped);

            if (orderIsShipped)
                throw new DeleteOrderItemOrderIsShippedAndCannotBeChangedException();
        }
    }
}
