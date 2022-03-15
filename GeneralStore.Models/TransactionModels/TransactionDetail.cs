using GeneralStore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Models.TransactionModels
{
    public class TransactionDetail
    {
        [Key]
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }

        //[Required(false, [ErrorMessage = "Item Count is required"])]
        //public int ItemCount { get; set; }

        //Customer relationship and navigation property
        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        //Product relationship and navigation property
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
