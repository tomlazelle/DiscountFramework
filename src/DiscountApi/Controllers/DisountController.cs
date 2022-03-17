using System;
using System.Threading.Tasks;
using AutoMapper;
using CommandQuery.Framing;
using DiscountApi.Models;
using DiscountFramework.Handlers;
using Microsoft.AspNetCore.Mvc;


namespace DiscountApi.Controllers
{
    [ApiController]
    public class DisountController : ControllerBase
    {
        private readonly IBroker _broker;
        private readonly IMapper _mapper;

        public DisountController(
            IBroker broker,
            IMapper mapper)
        {
            _broker = broker;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("discount")]
        public async Task<IActionResult> AddDiscount([FromBody] Discount discountRequest)
        {
            var discount = _mapper.Map<DiscountFramework.Discount>(discountRequest);

            var result = await _broker.HandleAsync<CreateDiscountMessage, CommandResponse<DiscountFramework.Discount>>(new CreateDiscountMessage
            {
                Discount = discount
            }
                                                                                                   );

            return Created("/discount", result.Data.Id);
        }
    }
}
