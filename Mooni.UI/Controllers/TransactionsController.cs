using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mooni.Domain.Entities;
using Mooni.Domain.InputModels.Transaction;
using Mooni.Domain.Repositories;
using Mooni.Domain.ViewModels.Transaction;
using Mooni.Infrastructure.Context;

namespace Mooni.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : CrudController<Transaction, TransactionViewModel, CreateTransactionInputModel>
    {
        private readonly MooniContext _context;
        public TransactionsController(IBaseRepository<Transaction> repository, IMapper mapper, MooniContext context) : base(repository, mapper)
        {
            _context = context;
        }
        public override async Task<ActionResult<List<TransactionViewModel>>> Get()
        {
            var entities = await _context.Transactions.Include(e=> e.Category).OrderByDescending(e=> e.CreatedAt).ToListAsync();
            return Ok(_mapper.Map<List<TransactionViewModel>>(entities));
        }
    }
}
