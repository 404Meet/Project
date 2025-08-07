using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Dtos.Stock;
using MyWebApi.Helpers;
using MyWebApi.Interfaces;
using MyWebApi.Mappers;

namespace MyWebApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockrepo;
        public StockController(ApplicationDBContext context, IStockRepository stockrepo)
        {
            _stockrepo = stockrepo;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        //async will make sure that whenever await is called the execution of the method stops there
        //and resume after the operation is complete
        //in the meantime thread continues to run outside the method synchronously and
        //new thread is given to awaited operation when completed
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var stocks = _context.Stocks.ToList();
            var stocks = await _stockrepo.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();
            return Ok(stockDto);
        }

        //In Get, we give results from models, passed through DTO's with help of Mapper.
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockrepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        //In Post, we take data in the form of DTO, and then give it to Model with help of mapper.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockrepo.CreatedAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.id }, stockModel.ToStockDto());
            //see definition of CreatedAtAction.
        }

        //In Put, we take id(or any identifier) and take the data in the form of DTO, and then give it to Model for update
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockrepo.UpdateAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }


        //In Patch, we take id(or any identifier) and take the data in the form of DTO, and then give it to Model for update
        // Only update if the field is provided (not null)
        // The best way is to use stored procedures
        // Patch will still update null to other values, if swagger attaches default value to all.
        [HttpPatch]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PatchUpdateDto patchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockrepo.UpdatePatchAsync(id, patchDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }

        //[HttpPatch("{id:int}")]
        // public IActionResult PatchWithSP(int id, [FromBody] UpdateStockDto updateDto)
        // {
        //     var parameters = new[]
        //     {
        //         new SqlParameter("@Id", id),
        //         new SqlParameter("@Symbol", (object?)updateDto.symbol ?? DBNull.Value),
        //         new SqlParameter("@CompanyName", (object?)updateDto.CompanyName ?? DBNull.Value),
        //         new SqlParameter("@Purchase", (object?)updateDto.Purchase ?? DBNull.Value),
        //         new SqlParameter("@Lastdiv", (object?)updateDto.Lastdiv ?? DBNull.Value),
        //         new SqlParameter("@Industry", (object?)updateDto.Industry ?? DBNull.Value),
        //         new SqlParameter("@MarketCap", (object?)updateDto.MarketCap ?? DBNull.Value)
        //     };

        //     _context.Database.ExecuteSqlRaw("EXEC UpdateStockPartial @Id, @Symbol, @CompanyName, @Purchase, @Lastdiv, @Industry, @MarketCap", parameters);

        //     var updated = _context.Stocks.FirstOrDefault(s => s.id == id);
        //     return Ok(updated?.ToStockDto());
        // }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockrepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}