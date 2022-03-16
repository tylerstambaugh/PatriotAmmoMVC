using GeneralStore.Data;
using GeneralStore.Models.TransactionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Services
{
    public class TransactionService
    {
        private readonly Guid _userId;
        public TransactionService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTransaction(TransactionCreate model)
        {
            if(model == null)
            return false;
            
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    Transaction transactionToCreate = new Transaction
                    {
                        CustomerId = model.CustomerId,
                        ProductId = model.ProductId,
                        TransactionDate = DateTime.Now
                    };
                    ctx.Transactions.Add(transactionToCreate);
                    return (ctx.SaveChanges() == 1);
                }
            }
            catch (Exception ex)
            {
                //Do something clever instead.
                Console.WriteLine(ex);
                Console.ReadLine();
                return false;
            }
        }

        public IEnumerable<TransactionDetail> GetAllTransactions()
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var transactionList = ctx.Transactions.Select(t => new TransactionDetail
                    {
                        TransactionId = t.TransactionId,
                        CustomerId = t.CustomerId,
                        ProductId = t.ProductId,
                        TransactionDate = t.TransactionDate

                    });

                    return transactionList.ToList();
                }
            }
            catch (Exception ex)
            {
                //Do something clever instead.
                Console.WriteLine(ex);
                Console.ReadLine();
                return null;
            }
        }

        public bool EditTransaction(int id, TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Transaction transactionToUpdate = ctx.Transactions.Find(id);
                if(transactionToUpdate != null)
                {
                    transactionToUpdate.CustomerId = model.CustomerId;
                    transactionToUpdate.ProductId = model.ProductId;
                }
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTransaction(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Transaction transactionToDelete = ctx.Transactions.Find(id);
                if (transactionToDelete != null)
                {
                    ctx.Transactions.Remove(transactionToDelete);
                }
                return ctx.SaveChanges() == 1;
            }
        }

        public TransactionDetail GetTransactionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var transactionToReturn = ctx.Transactions.Find(id);
                    if (transactionToReturn != null)
                    return new TransactionDetail
                    {
                        TransactionId = transactionToReturn.TransactionId,
                        CustomerId = transactionToReturn.CustomerId,
                        ProductId = transactionToReturn.ProductId,
                        TransactionDate = transactionToReturn.TransactionDate

                    };
                return null;
            }
        }
    }
}
