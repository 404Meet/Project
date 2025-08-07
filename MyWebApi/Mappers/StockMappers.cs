using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos.Stock;
using MyWebApi.Models;

namespace MyWebApi.Mappers
{
    public static class StockMappers
    {
        //In Get Mapper, we map Model with DTO's
        //parameter with return type
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                id = stockModel.id,
                symbol = stockModel.symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                Lastdiv = stockModel.Lastdiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments= stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        //In Post Mapper, we map DTO's with Model
        //parameter with return type
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                symbol = stockDto.symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                Lastdiv = stockDto.Lastdiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}