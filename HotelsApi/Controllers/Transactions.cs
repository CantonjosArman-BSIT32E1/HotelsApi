using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TransactionsController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Transactions>> GetAllTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transactions>> GetTransactionById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return NotFound();
            return transaction;
        }

        [HttpPost]
        public async Task<ActionResult<Transactions>> CreateTransaction([FromBody] Transactions transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.TransactionId }, transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transactions transaction)
        {
            var existingTransaction = await _context.Transactions.FindAsync(id);
            if (existingTransaction == null) return NotFound();

            existingTransaction.HotelId = transaction.HotelId;
            existingTransaction.HotelName = transaction.HotelName;
            existingTransaction.HotelCode = transaction.HotelCode;
            existingTransaction.DateFrom = transaction.DateFrom;
            existingTransaction.DateTo = transaction.DateTo;
            existingTransaction.FullName = transaction.FullName;
            existingTransaction.PhoneNumber = transaction.PhoneNumber;
            existingTransaction.EmailAddress = transaction.EmailAddress;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return NotFound();

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
