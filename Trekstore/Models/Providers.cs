using System.ComponentModel.DataAnnotations;

namespace Trekstore.Models
{
    public class Providers
    {
        [Key] public int ProviderID { get; set; }

        public string Name { get; set; } = null!;

        public int Telephone { get; set; }

        public string Address { get; set; } = null!;

        public virtual ICollection<PurchaseDetails> PurchaseDetails { get; set; } = new List<PurchaseDetails>();
    }
}
