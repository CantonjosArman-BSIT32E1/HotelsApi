using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllTransactions();
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> CreateTransaction(Transaction transaction);
        Task<Transaction?> UpdateTransaction(Transaction transaction, int id);
        Task<bool> DeleteTransaction(int id);
    }

    public class TransactionRepository : ITransactionRepository
    {
        public DatabaseContext databaseContext;

        public TransactionRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            await databaseContext.Transaction.AddAsync(transaction);
            await databaseContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            var transactionFromDatabase = await databaseContext.Transaction.Where(t => t.TransactionId == id).FirstOrDefaultAsync();

            if (transactionFromDatabase == null)
                return false;

            databaseContext.Remove(transactionFromDatabase);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            List<Transaction> listOfTransactions = await databaseContext.Transaction.ToListAsync();
            return listOfTransactions;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            Transaction? transaction = await databaseContext.Transaction.Where(t => t.TransactionId == id).FirstOrDefaultAsync();

            if (transaction != null)
                return transaction;

            return null;
        }

        public async Task<Transaction?> UpdateTransaction(Transaction transaction, int id)
        {
            var transactionFromDatabase = await databaseContext.Transaction.Where(t => t.TransactionId == id).FirstOrDefaultAsync();

            if (transactionFromDatabase == null)
                return null;


            transactionFromDatabase.HotelName = transaction.HotelName;
            transactionFromDatabase.HotelCode = transaction.HotelCode;
            transactionFromDatabase.DateFrom = transaction.DateFrom;
            transactionFromDatabase.DateTo = transaction.DateTo;
            transactionFromDatabase.FullName = transaction.FullName;
            transactionFromDatabase.PhoneNumber = transaction.PhoneNumber;
            transactionFromDatabase.EmailAddress = transaction.EmailAddress;

            await databaseContext.SaveChangesAsync();
            return transactionFromDatabase;
        }
    }
}
