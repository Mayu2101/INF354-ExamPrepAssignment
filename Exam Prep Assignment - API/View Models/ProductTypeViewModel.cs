using System.Collections.Generic;

namespace Exam_Prep_Assignment___API.View_Models
{
    public class ProductTypeViewModel : BaseEntityViewModel
    {
        public int ProductTypeId { get; set; }
        public virtual ICollection<ProductViewModel> Products { get; set; }
    }
}
