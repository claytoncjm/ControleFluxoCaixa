using ControleFluxoCaixa.Data;
using ControleFluxoCaixa.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFluxoCaixa.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetByDateAsync(DateTime date)
        {
            return await _context.Transactions
                .Where(t => t.Date.Date == date.Date)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
