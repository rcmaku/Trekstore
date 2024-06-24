using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trekstore.Models
{
    public class PurchaseDetails
    {
        [Key] public int purch_id { get; set; }

        public int Amount { get; set; }

        public DateTime PurchDate { get; set; }

         public int? ProductID { get; set; }

        public int? ProviderID{ get; set; }

        public virtual Products? Product{ get; set; }

        public virtual Providers? Provider { get; set; }
    }
}
