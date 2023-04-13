using Mooni.Domain.Entities;
using Mooni.Domain.InputModels.Transaction;
using Mooni.Domain.Repositories;

namespace Mooni.Domain.Services
{
    public class TransactionServices
    {
        private readonly IBaseRepository<Transaction> _repository;

        public TransactionServices(IBaseRepository<Transaction> repository)
        {
            _repository = repository;
        }
        public void Create(CreateTransactionInputModel model)
        {
               
        }
    }
}
