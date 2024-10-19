using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Stock;
using api.Interfaces;
using api.Helpers;

namespace api.Controllers
{
    [Route("/api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQueryObject query)
        {
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDtos = stocks.Select((stock) => stock.ToStockDto());
            return Ok(stockDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            return stock == null
                ? NotFound()
                : Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto newStock)
        {
            // Validate Model State
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Stock stock = await _stockRepo.CreateAsync(newStock.ToStockFromCreateDTO());

            return CreatedAtAction(
                nameof(GetStockById),
                new { id = stock.Id },
                stock.ToStockDto()
            );
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStockById([FromRoute] int id, [FromBody] UpdateStockRequestDto updatedStock)
        {
            // Validate Model State
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Stock? stock = await _stockRepo.UpdateByIdAsync(id, updatedStock);

            return stock == null
                ? NotFound()
                : NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStockById([FromRoute] int id)
        {
            Stock? stock = await _stockRepo.DeleteByIdAsync(id);

            return stock == null
                ? NotFound()
                : NoContent();
        }
    }
}