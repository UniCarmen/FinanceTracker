﻿using Microsoft.EntityFrameworkCore;
using MoneyManagement.DbContexts;
using MoneyManagement.Entities;
using Transaction = MoneyManagement.Models.Transaction;

namespace MoneyManagement.DataAccess
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinanceContext _financeContext;

        public TransactionRepository(FinanceContext financeContext)
        {
            _financeContext = financeContext ?? throw new ArgumentNullException(nameof(financeContext));
        }

        // CodeReview: Unnötigen Filter entfernen (Catergories)
        // Wenn Filter gebraucht wird, dann in eine separate Repository Function packen. ggf. mit filter - parameter
        public async Task<List<Transaction>> LoadTransactions(Guid accountId)
        {
            var categories = await _financeContext.Categories./*Where(c => c.Expense).*/ToListAsync();

            var transactionEntities = await
                _financeContext.Transactions
                .Where(t => t.AccountId == accountId)
                .Where(t => categories
                    .Select(c=> c.Name)
                    .Contains(t.CategoryName))
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            var transactions =
                transactionEntities
                .Select(t => t.TransactionEntityToTransaction())
                .ToList();

            return transactions;
        }

        // CodeReview: return nur Task, Und Statusmeldungen den überliegenden Schichten überlassen
        // leere input liste prüfen und ggf. vorzeitiges return.
        public async Task<string> SaveTransactions(List<Transaction> transactions)
        {
            var transactionEntities = new List<TransactionEntity>();

            //könnte ich Linqen
            foreach (var transaction in transactions)
            {
                var transactionEntity = transaction.TransactionToTransactionEntity();
                transactionEntities.Add(transactionEntity);
            }

            await _financeContext.Transactions.AddRangeAsync(transactionEntities);
            await _financeContext.SaveChangesAsync();

            return "Transfer saved";
        }
    }

}
