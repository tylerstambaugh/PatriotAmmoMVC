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
    }
}
