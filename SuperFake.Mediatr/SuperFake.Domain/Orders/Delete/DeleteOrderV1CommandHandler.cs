using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class DeleteOrderV1CommandHandler : IRequestHandler<DeleteOrderV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public DeleteOrderV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteOrderV1Command request, CancellationToken cancellationToken)
        {
            await VerifyOrderExists(request.OrderID);

            await VerifyOrderHasNotShipped(request.OrderID);

            var order = await _dbContext.Orders.FindAsync(request.OrderID);

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyOrderExists(int orderID)
        {
            var orderExists = await _dbContext.Orders.AnyAsync(e => e.ID == orderID);

            if (!orderExists)
                throw new DeleteOrderDoesNotExistException();
        }

        private async Task VerifyOrderHasNotShipped(int orderID)
        {
            var orderIsShipped = await _dbContext.Orders.AnyAsync(i => i.ID == orderID && i.OrderStatus == OrderStatuses.Shipped);

            if (orderIsShipped)
                throw new DeleteOrderIsShippedAndCannotBeChangedException();
        }
    }
}
