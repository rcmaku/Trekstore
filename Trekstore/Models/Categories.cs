using System.ComponentModel.DataAnnotations;

namespace Trekstore.Models
{
    public class Categories
    {
        
        [Key] public int CategoryID { get; set; }

        public string CategoriaNombre { get; set; } = null!;

        public string CategoriaDescripcion { get; set; } = null!;

        public virtual ICollection<Products> Products { get; set; } = new List<Products>();
    }

}
