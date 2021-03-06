﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Products.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Products.Domain
{
    public class UpdateProductV1CommandHandler : IRequestHandler<UpdateProductV1Command>
    {
        private readonly SuperFakeProductsDbContext _dbContext;
        private readonly IMediator _mediator;

        public UpdateProductV1CommandHandler(SuperFakeProductsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateProductV1Command request, CancellationToken cancellationToken)
        {
            await VerifyProductExists(request.Product.ID, cancellationToken);

            await VerifyProductNameIsUnique(request.Product.ID, request.Product.Name, cancellationToken);

            await UpdateProduct(request.Product, cancellationToken);

            await PublishProductUpdatedNotification(request.Product, cancellationToken);

            return Unit.Value;
        }

        private async Task UpdateProduct(Product product, CancellationToken cancellationToken)
        {
            _dbContext.Update(product);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishProductUpdatedNotification(Product product, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new ProductUpdatedV1Notification
            {
                ID = product.ID,
                Category = product.Category,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price

            }, cancellationToken);
        }

        private async Task VerifyProductNameIsUnique(int productID, string productName, CancellationToken cancellationToken)
        {
            var nameExists = await _dbContext.Products.AnyAsync(i => i.ID != productID && i.Name == productName, cancellationToken);

            if (nameExists)
                throw new UpdateProductNameMustBeUniqueException();
        }

        private async Task VerifyProductExists(int productID, CancellationToken cancellationToken)
        {
            var productExists = await _dbContext.Products.AnyAsync(e => e.ID == productID, cancellationToken);

            if (!productExists)
                throw new UpdateProductDoesNotExistException();
        }
    }
}
