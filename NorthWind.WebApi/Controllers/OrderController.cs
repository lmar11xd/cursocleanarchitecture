using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.UseCases.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IMediator mediator;
        public OrderController(IMediator mediator) => this.mediator = mediator;

        [HttpPost("create-order")]
        public async Task<ActionResult<int>> CreateOrder(CreateOrderInputPort orderParams)
        {
            return await mediator.Send(orderParams);
        }
    }
}
