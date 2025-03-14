using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Repositories;
using HotelsApi.Validators;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Services
{
    public interface ITransactionService
    {
        Task<List<GetTransactionModel>> GetAllTransactions();
        Task<GetTransactionModel> GetTransactionById(int id);
        Task<GetTransactionModel> CreateTransaction(CreateTransactionModel transaction);
        Task<GetTransactionModel?> UpdateTransaction(UpdateTransactionModel transaction, int id);
        Task<bool> DeleteTransaction(int id);
    }

    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public TransactionService(DatabaseContext databaseContext, ITransactionRepository transactionRepository, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
        }

        public async Task<GetTransactionModel> CreateTransaction(CreateTransactionModel transaction)
        {
            CreateTransactionValidator validator = new CreateTransactionValidator(databaseContext);
            ValidationResult results = validator.Validate(transaction);

            if (results.Errors.Count > 0)
            {
                throw new Exception(string.Join(",", results.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var createTransactionResult = await transactionRepository.CreateTransaction(mapper.Map<Transaction>(transaction));
            return mapper.Map<GetTransactionModel>(createTransactionResult);
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

        public async Task<List<GetTransactionModel>> GetAllTransactions()
        {
            List<Transaction> listOfTransactions = await databaseContext.Transaction.ToListAsync();
            return mapper.Map<List<GetTransactionModel>>(listOfTransactions);
        }

        public async Task<GetTransactionModel> GetTransactionById(int id)
        {
            Transaction? transaction = await databaseContext.Transaction.Where(t => t.TransactionId == id).FirstOrDefaultAsync();

            if (transaction != null)
                return mapper.Map<GetTransactionModel>(transaction);

            return null;
        }

        public async Task<GetTransactionModel?> UpdateTransaction(UpdateTransactionModel transaction, int id)
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
            return mapper.Map<GetTransactionModel>(transactionFromDatabase);
        }
    }
}