using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_Prep_Assignment___API.Models
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Price { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
