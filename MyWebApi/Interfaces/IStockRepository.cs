using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos.Stock;
using MyWebApi.Helpers;
using MyWebApi.Models;

namespace MyWebApi.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id); //FindBy or FirstorDefault can be null, which is why Stock?
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreatedAsync(Stock stockModel);
        Task<Stock> UpdateAsync(int id, UpdateStockDto stockDto);
        Task<Stock> UpdatePatchAsync(int id, PatchUpdateDto patchDto);
        Task<Stock> DeleteAsync(int id);
        Task<Boolean> StockExists(int id);
    }
}