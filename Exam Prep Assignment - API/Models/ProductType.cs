using System.Collections.Generic;

namespace Exam_Prep_Assignment___API.Models
{
    public class ProductType : BaseEntity
    {
        public int ProductTypeId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
