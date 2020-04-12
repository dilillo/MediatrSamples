using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class UpdateOrderV1CommandHandler : IRequestHandler<UpdateOrderV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public UpdateOrderV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateOrderV1Command request, CancellationToken cancellationToken)
        {
            await VerifyOrderExists(request.Order.ID);

            await VerifyOrderHasNotShipped(request.Order.ID);

            _dbContext.Update(request.Order);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyOrderExists(int orderID)
        {
            var orderExists = await _dbContext.Orders.AnyAsync(e => e.ID == orderID);

            if (!orderExists)
                throw new UpdateOrderDoesNotExistException();
        }

        private async Task VerifyOrderHasNotShipped(int orderID)
        {
            var orderIsShipped = await _dbContext.Orders.AnyAsync(i => i.ID == orderID && i.OrderStatus == OrderStatuses.Shipped);

            if (orderIsShipped)
                throw new UpdateOrderIsShippedAndCannotBeChangedException();
        }
    }
}
