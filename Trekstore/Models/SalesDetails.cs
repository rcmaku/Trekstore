using System.ComponentModel.DataAnnotations;

namespace Trekstore.Models
{
    public class SalesDetails
    {
        [Key] public int SalesDetailsID { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; }

        public int? ProductId { get; set; }

        public int? ClientId { get; set; }

        public int? TipoDePagoID { get; set; }

        public virtual Client? Clients { get; set; }

        public virtual Products? Product { get; set; }

        public virtual TipoDePago? TipoDePago{ get; set; }


    }
}
