using System.ComponentModel.DataAnnotations;

namespace Trekstore.Models
{
    public class Client
    {
        [Key] public int ClientId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName{ get; set; } = null!;

        public int PhoneNumber { get; set; }

        public string? Email { get; set; }

        public virtual ICollection<SalesDetails> SalesDetails { get; set; } = new List<SalesDetails>();
    }
}
