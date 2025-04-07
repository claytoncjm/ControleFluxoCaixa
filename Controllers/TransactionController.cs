using ControleFluxoCaixa.Models;
using ControleFluxoCaixa.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ControleFluxoCaixa.Attributes;

namespace ControleFluxoCaixa.Controllers
{
    [Authorize] 
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return View(transactions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Amount,Description")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                await _transactionService.AddTransactionAsync(transaction);
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        public async Task<IActionResult> DailyBalance(DateTime? date)
        {
            var searchDate = date ?? DateTime.Today;
            var transactions = await _transactionService.GetTransactionsByDateAsync(searchDate);
            var balance = await _transactionService.GetDailyBalanceAsync(searchDate);

            ViewBag.Date = searchDate;
            ViewBag.Balance = balance;

            return View(transactions);
        }
    }
}
