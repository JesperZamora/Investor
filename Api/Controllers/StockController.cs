using Api.Dtos.StockDtos;
using Api.Helpers;
using Api.Interfaces;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQuery query)
        {
            var stocks = await _stockRepo.GetAllAsync(query);

            var stockDtos = stocks.Select(s => s.ToStockDto());

            return Ok(stockDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            return stock is null ? NotFound() : Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {
            var stock = stockDto.ToStock();

            var createdStock = await _stockRepo.CreateAsync(stock);

            return CreatedAtAction(nameof(GetById), new { id = createdStock.Id }, createdStock.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto stockDto)
        {   
            var updatedStock = await _stockRepo.UpdateAsync(id, stockDto);

            if (updatedStock is null) return BadRequest();

            return updatedStock is null ? NotFound() : Ok(updatedStock.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedStock = await _stockRepo.DeleteAsync(id);

            return deletedStock is null ? NotFound() : NoContent();
        }

    }
}
