using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required, Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required, Range(0, 250), Display(Name = "# In Stock")]
        public int InventoryCount { get; set; }
        [Required, Display(Name="IsFood")]
        public bool IsExplosive { get; set; }

    }
}
