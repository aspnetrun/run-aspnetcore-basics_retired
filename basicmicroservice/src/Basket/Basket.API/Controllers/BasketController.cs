using Basket.API.Entities;
using Basket.API.RabbitMq;
using Basket.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]    
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly EventBusRabbitMQPublisher _eventBus;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepository repository, EventBusRabbitMQPublisher eventBus, ILogger<BasketController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        {
            var basket = await _repository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody]CustomerBasket value)
        {
            return Ok(await _repository.UpdateBasketAsync(value));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasketByIdAsync(string id)
        {
            await _repository.DeleteBasketAsync(id);
        }

        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody]BasketCheckout basketCheckout)
        {
            // remove the basket 
            // send checkout event to rabbitMq

            var userId = "swn"; // _identityService.GetUserIdentity();
                        
            var basketRemoved = await _repository.DeleteBasketAsync(userId);
            if (!basketRemoved)
            {
                return BadRequest();
            }

            basketCheckout.RequestId = Guid.NewGuid();
            basketCheckout.Buyer = userId;
            basketCheckout.City = "asd";
            basketCheckout.Country = "asd";

            _eventBus.PublishBasketCheckout("basketCheckoutQueue", basketCheckout);

            // TODO : burayı alttaki gibi yapılacak -- rabbitMQ kısmı ayrı bir class library yapılıp BasketCheckoutAcceptedIntegrationEvent class ı yapılıp 2 tarafta onu kullanacak

            //var userName = this.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;

            //var eventMessage = new UserCheckoutAcceptedIntegrationEvent(userId, userName, basketCheckout.City, basketCheckout.Street,
            //    basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
            //    basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basketCheckout.RequestId, basket);

            //// Once basket is checkout, sends an integration event to
            //// ordering.api to convert basket to order and proceeds with
            //// order creation process
            //try
            //{
            //    _eventBus.Publish(eventMessage);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", eventMessage.Id, "asd");
            //    throw;
            //}

            return Accepted();
        }

    }
}
