using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.BusinessLayer;
using OrderService.API.DataLayer.DTOs;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;

        public OrdersController(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateOrder(AddOrderDTO addOrder)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderBusiness.CreateOrder(addOrder);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrderItems(List<AddItemsDTO> addItems)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderBusiness.AddOrderedItems(addItems);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderBusiness.DeleteOrder(id);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrder(AddOrderDTO updateOrder)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderBusiness.UpdateOrder(updateOrder);
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
