using ControleFluxoCaixa.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFluxoCaixa.Services
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetTransactionsByDateAsync(DateTime date);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<decimal> GetDailyBalanceAsync(DateTime date);
        Task AddTransactionAsync(Transaction transaction);
    }
}
