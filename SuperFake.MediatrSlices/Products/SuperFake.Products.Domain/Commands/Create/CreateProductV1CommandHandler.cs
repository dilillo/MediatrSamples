﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Products.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Products.Domain
{
    public class CreateProductV1CommandHandler : IRequestHandler<CreateProductV1Command>
    {
        private readonly SuperFakeProductsDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateProductV1CommandHandler(SuperFakeProductsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateProductV1Command request, CancellationToken cancellationToken)
        {
            await VerifyProductNameIsUnique(request.Product.Name, cancellationToken);

            await CreateProduct(request.Product, cancellationToken);

            await PublishProuductCreatedNotification(request.Product, cancellationToken);

            return Unit.Value;
        }

        private async Task CreateProduct(Product product, CancellationToken cancellationToken)
        {
            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishProuductCreatedNotification(Product product, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new ProductCreatedV1Notification
            {
                ID = product.ID,
                Category = product.Category,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price

            }, cancellationToken);
        }

        private async Task VerifyProductNameIsUnique(string productName, CancellationToken cancellationToken)
        {
            var nameExists = await _dbContext.Products.AnyAsync(i => i.Name == productName, cancellationToken);

            if (nameExists)
                throw new CreateProductNameMustBeUniqueException();
        }
    }
}
