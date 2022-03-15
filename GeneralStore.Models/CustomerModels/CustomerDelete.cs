using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Models.CustomerModels
{
    public class CustomerDelete
    {
        [Required]
        public int CustomerID { get; set; }
    }
}
