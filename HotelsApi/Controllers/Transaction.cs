using HotelsApi.Services;  
using HotelsApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await transactionService.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById([FromRoute] int id)
        {
            var transaction = await transactionService.GetTransactionById(id);
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionModel transaction)
        {
            var createdTransaction = await transactionService.CreateTransaction(transaction);
            return Ok(createdTransaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction([FromRoute] int id, [FromBody] UpdateTransactionModel transaction)
        {
            var updatedTransaction = await transactionService.UpdateTransaction(transaction, id);
            return Ok(updatedTransaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {
            var result = await transactionService.DeleteTransaction(id);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
