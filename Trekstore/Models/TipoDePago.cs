using System.ComponentModel.DataAnnotations;


namespace Trekstore.Models
{
    public class TipoDePago
    {

        [Key] public int tipoPagoID{ get; set; }

        public string tipoPago {  get; set; }

        public virtual ICollection<PurchaseDetails> PurchaseDetails{ get; set; } = new List<PurchaseDetails>();

        public virtual ICollection<SalesDetails> SalesDetails{ get; set; } = new List<SalesDetails>();

    }
}
