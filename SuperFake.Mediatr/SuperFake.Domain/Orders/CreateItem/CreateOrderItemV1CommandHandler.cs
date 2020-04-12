using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class CreateOrderItemV1CommandHandler : IRequestHandler<CreateOrderItemV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public CreateOrderItemV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateOrderItemV1Command request, CancellationToken cancellationToken)
        {
            await VerifyOrderExists(request.OrderItem.OrderID);

            await VerifyOrderHasNotShipped(request.OrderItem.OrderID);

            await VerifyProductExists(request.OrderItem.ProductID);

            await VerifyOrderItemProductIsUnique(request.OrderItem);

            _dbContext.OrderItems.Add(request.OrderItem);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyOrderExists(int orderID)
        {
            var orderExists = await _dbContext.Orders.AnyAsync(e => e.ID == orderID);

            if (!orderExists)
                throw new CreateOrderItemOrderDoesNotExistException();
        }


        private async Task VerifyOrderHasNotShipped(int orderID)
        {
            var orderIsShipped = await _dbContext.Orders.AnyAsync(i => i.ID == orderID && i.OrderStatus == OrderStatuses.Shipped);

            if (orderIsShipped)
                throw new CreateOrderItemOrderIsShippedAndCannotBeChangedException();
        }

        private async Task VerifyProductExists(int productID)
        {
            var productExists = await _dbContext.Products.AnyAsync(e => e.ID == productID);

            if (!productExists)
                throw new CreateOrderItemProductDoesNotExistException();
        }

        private async Task VerifyOrderItemProductIsUnique(OrderItem orderItem)
        {
            var exstingOrderItemForSameProductInOrder = await _dbContext.OrderItems.AnyAsync(i => i.ID != orderItem.ID && i.OrderID == orderItem.OrderID && i.ProductID == orderItem.ProductID);

            if (exstingOrderItemForSameProductInOrder)
                throw new CreateOrderItemOrderItemAlreadyExistsForThatProductException();
        }

    }
}
