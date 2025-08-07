using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Interfaces;
using MyWebApi.Models;
using MyWebApi.Data;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Dtos.Stock;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace MyWebApi.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreatedAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
            //throw new NotImplementedException();
        }

        public async Task<Stock> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
            //throw new NotImplementedException();
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).ThenInclude(a=>a.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.symbol))
            {
                stocks = stocks.Where(s => s.symbol.Contains(query.symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecending ? stocks.OrderByDescending(s => s.symbol) : stocks.OrderBy(s => s.symbol);
                }
            }
            var skipnumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipnumber).Take(query.PageSize).ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(i=>i.id==id);
            //throw new NotImplementedException();
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.symbol == symbol);
            //throw new NotImplementedException();
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(i=> i.id==id);
            //throw new NotImplementedException();
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockDto stockDto)
        {
            var existingstock = await _context.Stocks.FirstOrDefaultAsync(x => x.id == id);
            if (existingstock == null)
            {
                return null;
            }
            existingstock.symbol = stockDto.symbol;
            existingstock.CompanyName = stockDto.CompanyName;
            existingstock.Purchase = stockDto.Purchase;
            existingstock.MarketCap = stockDto.MarketCap;
            existingstock.Lastdiv = stockDto.Lastdiv;
            existingstock.Industry = stockDto.Industry;

            await _context.SaveChangesAsync();
            return existingstock;
            //throw new NotImplementedException();
        }

        public async Task<Stock> UpdatePatchAsync(int id, PatchUpdateDto patchDto)
        {
            var existingstock = await _context.Stocks.FirstOrDefaultAsync(x => x.id == id);
            if (existingstock == null)
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(patchDto.symbol))
                existingstock.symbol = patchDto.symbol;

            if (!string.IsNullOrWhiteSpace(patchDto.CompanyName))
                existingstock.CompanyName = patchDto.CompanyName;

            if (patchDto.Purchase.HasValue)
                existingstock.Purchase = patchDto.Purchase.Value;

            if (patchDto.Lastdiv.HasValue)
                existingstock.Lastdiv = patchDto.Lastdiv.Value;

            if (!string.IsNullOrWhiteSpace(patchDto.Industry))
                existingstock.Industry = patchDto.Industry;

            if (patchDto.MarketCap.HasValue)
                existingstock.MarketCap = patchDto.MarketCap.Value;

            await _context.SaveChangesAsync();
            return existingstock;
            //throw new NotImplementedException();
        }
    }
}