using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFluxoCaixa.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório")]
        public TransactionType Type { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public string Description { get; set; }

        public Transaction()
        {
            Date = DateTime.Now;
        }
    }

    public enum TransactionType
    {
        Credit,
        Debit
    }
}
