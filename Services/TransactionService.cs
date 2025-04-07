using ControleFluxoCaixa.Models;
using ControleFluxoCaixa.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFluxoCaixa.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY_PREFIX = "daily_balance_";

        public TransactionService(ITransactionRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateAsync(DateTime date)
        {
            return await _repository.GetByDateAsync(date);
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<decimal> GetDailyBalanceAsync(DateTime date)
        {
            string cacheKey = $"{CACHE_KEY_PREFIX}{date:yyyy-MM-dd}";

            if (_cache.TryGetValue(cacheKey, out decimal cachedBalance))
            {
                return cachedBalance;
            }

            var transactions = await _repository.GetByDateAsync(date);
            var balance = CalculateBalance(transactions);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(cacheKey, balance, cacheEntryOptions);

            return balance;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _repository.AddAsync(transaction);
            await _repository.SaveChangesAsync();

            // Invalidate cache for the day when new transaction is added
            string cacheKey = $"{CACHE_KEY_PREFIX}{transaction.Date:yyyy-MM-dd}";
            _cache.Remove(cacheKey);
        }

        private decimal CalculateBalance(IEnumerable<Transaction> transactions)
        {
            var credits = transactions.Where(t => t.Type == TransactionType.Credit)
                                   .Sum(t => t.Amount);
            var debits = transactions.Where(t => t.Type == TransactionType.Debit)
                                  .Sum(t => t.Amount);

            return credits - debits;
        }
    }
}
