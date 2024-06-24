using System.ComponentModel.DataAnnotations;

namespace Trekstore.Models
{
    public class Products
    {
        [Key]public int ProductId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "La descripción del producto es obligatoria.")]
        [StringLength(200, ErrorMessage = "La descripción no puede tener más de 200 caracteres.")]
        public string ProductDescription { get; set; } = null!;

        [Required(ErrorMessage = "El Precio del producto es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El precio debe ser un número positivo.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El precio solo puede contener números.")]
        public int ProductPrice { get; set; }

        public int InStock {  get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int? CategoryID { get; set; }

        public virtual Categories? Category { get; set; }

        public virtual ICollection<PurchaseDetails> PurchaseDetails { get; set; } = new List<PurchaseDetails>();

        public virtual ICollection<SalesDetails> SalesDetails { get; set; } = new List<SalesDetails>();
    }
}
