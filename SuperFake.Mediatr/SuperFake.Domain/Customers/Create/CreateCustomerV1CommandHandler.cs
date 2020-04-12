using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class CreateCustomerV1CommandHandler : IRequestHandler<CreateCustomerV1Command>
    {
        private readonly ICreateCustomerV1CommandHandlerData _data;

        public CreateCustomerV1CommandHandler(ICreateCustomerV1CommandHandlerData data)
        {
            _data = data;
        }

        public async Task<Unit> Handle(CreateCustomerV1Command request, CancellationToken cancellationToken)
        {
            await VerifyCustomerNameIsUnique(request.Customer.FirstName, request.Customer.LastName);

            _data.AddCustomer(request.Customer);

            await _data.SaveChanges();

            return Unit.Value;
        }

        private async Task VerifyCustomerNameIsUnique(string customerFirstName, string customerLastName)
        {
            var nameExists = await _data.CustomerNameExists(customerFirstName, customerLastName);

            if (nameExists)
                throw new CreateCustomerNameMustBeUniqueException();
        }
    }
}
