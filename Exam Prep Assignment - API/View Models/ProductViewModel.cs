using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_Prep_Assignment___API.View_Models
{
    public class ProductViewModel : BaseEntityViewModel
    {
        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Price { get; set; }
        public virtual ProductTypeViewModel ProductType { get; set; }
        public virtual BrandViewModel Brand { get; set; }
    }
}
