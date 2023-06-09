﻿using AutoMapper;
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
            var entities = await _context.Transactions.Include(e => e.Category).OrderByDescending(e => e.CreatedAt).ToListAsync();
            return Ok(_mapper.Map<List<TransactionViewModel>>(entities));
        }
        [HttpGet("quicksearch")]
        public async Task<ActionResult<List<TransactionViewModel>>> Quicksearch(string query)
        {
            var entities = await _context.Transactions.Include(e => e.Category).Where(e => e.Name.ToLower().Contains(query.ToLower())).OrderByDescending(e => e.CreatedAt).ToListAsync();
            return Ok(_mapper.Map<List<TransactionViewModel>>(entities));

        }
        [HttpPut("payment/{id}")]
        public async Task<ActionResult<List<TransactionViewModel>>> UpdatePayment(Guid id, [FromBody] bool paid)
        {
            var entity = await _context.Transactions.FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null)
                return BadRequest();
            entity.Paid = paid;
            _context.Transactions.Update(entity);
            await _context.SaveChangesAsync();
            return Ok(new {paid});

        }
    }
}
