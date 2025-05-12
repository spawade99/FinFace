using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Stock;
using api.Models;

namespace api.Mapper
{
    public static class StockMapper
    {

        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Industry = stockModel.Industry,
                LastDiv = stockModel.LastDiv,
                MarketCap = stockModel.MarketCap,
                Purchase = stockModel.Purchase
            };
        }

        public static Stock ToStockModelFromCreateStockDto(this CreateStockDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Industry = stockDto.Industry,
                LastDiv = stockDto.LastDiv,
                MarketCap = stockDto.MarketCap,
                Purchase = stockDto.Purchase
            };
        }
    }
}