using System.ComponentModel.DataAnnotations;

namespace Trekstore.Models
{
    public class Purchases
    {
        [Key] public int PurchaseID { get; set; }

        public int Amount { get; set; }
    }
}
