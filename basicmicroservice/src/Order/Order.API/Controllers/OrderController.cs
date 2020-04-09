using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        //private readonly IBasketRepository _repository;        
        private readonly ILogger<OrderController> _logger;

        //public BasketController(IBasketRepository repository, EventBusRabbitMQPublisher eventBus, ILogger<BasketController> logger)
        //{
        //    _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        //    _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //}

        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        //{
        //    var basket = await _repository.GetBasketAsync(id);
        //    return Ok(basket ?? new CustomerBasket(id));
        //}



    }
}
