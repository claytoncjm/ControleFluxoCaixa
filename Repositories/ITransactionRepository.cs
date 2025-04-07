using ControleFluxoCaixa.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFluxoCaixa.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetByDateAsync(DateTime date);
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task AddAsync(Transaction transaction);
        Task SaveChangesAsync();
    }
}
