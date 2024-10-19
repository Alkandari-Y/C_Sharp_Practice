using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(StockQueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateByIdAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);

    }
}