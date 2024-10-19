using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(stock => stock.Symbol.ToLower() == symbol.ToLower());
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteByIdAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null) return null;

            await _context.Stocks.Where(stock => stock.Id == id).ExecuteDeleteAsync();

            return stock;
        }

        public async Task<List<Stock>> GetAllAsync(StockQueryObject query)
        {
            var stocks = _context.Stocks.Include(s => s.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending 
                                ? stocks.OrderByDescending(s => s.Symbol)
                                : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize; 

            // Without performing queries, filters, or sorting
            // return await _context.Stock.Include(s => s.Comments).ToListAsync();

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks
                .Include(s => s.Comments)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateByIdAsync(int id, UpdateStockRequestDto stockDto)
        {
            Stock? stock = await _context.Stocks.FindAsync(id);

            if (stock == null) return null;

            _context.Stocks.Entry(stock).CurrentValues.SetValues(stockDto.ToStockFromUpdate(id));
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}