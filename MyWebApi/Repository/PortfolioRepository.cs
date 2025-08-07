using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Interfaces;
using MyWebApi.Models;

namespace MyWebApi.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
            //throw new NotImplementedException();
        }

        public async Task<Portfolio> DeleteAsync(AppUser appuser, string symbol)
        {
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appuser.Id && x.stock.symbol.ToLower() == symbol.ToLower());
            if (portfolioModel == null)
            {
                return null;
            }
            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
            //throw new NotImplementedException();
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                id = stock.StockID,
                symbol = stock.stock.symbol,
                CompanyName = stock.stock.CompanyName,
                Purchase = stock.stock.Purchase,
                Lastdiv = stock.stock.Lastdiv,
                Industry = stock.stock.Industry,
                MarketCap = stock.stock.MarketCap
            }).ToListAsync();
            //throw new NotImplementedException();
        }
    }
}