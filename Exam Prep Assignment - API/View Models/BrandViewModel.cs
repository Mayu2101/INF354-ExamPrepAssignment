using System.Collections.Generic;

namespace Exam_Prep_Assignment___API.View_Models
{
    public class BrandViewModel : BaseEntityViewModel
    {
        public int BrandId { get; set; }
        public virtual ICollection<ProductViewModel> Products { get; set; }
    }
}
