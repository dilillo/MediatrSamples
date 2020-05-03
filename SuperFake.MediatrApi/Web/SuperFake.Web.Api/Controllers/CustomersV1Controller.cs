using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Web.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperFake.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersV1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/CustomersV1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllCustomersV1QueryResult>>> GetCustomers()
        {
            return await _mediator.Send(new GetAllCustomersV1Query());
        }

        // GET: api/CustomersV1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDetailsV1QueryResult>> GetCustomer(int id)
        {
            var customerData = await _mediator.Send(new GetCustomerDetailsV1Query { CustomerID = id });

            if (customerData == null)
            {
                return NotFound();
            }

            return customerData;
        }
    }
}
