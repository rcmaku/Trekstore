using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trekstore.Models
{
    public class Sales
    {
        [Key] public int SalesID { get; set; }

        public int Amount { get; set; }

        [Required]
        public int ClientID {  get; set; }
        
        public virtual Client? Clients { get; set; }

        [Required]
        public int ProductID {  get; set; }
        public virtual Products? Product { get; set; }
    }
}
