using System.Threading.Tasks;
using Exam_Prep_Assignment___API.Models;

namespace Exam_Prep_Assignment___API.Repository
{
    public interface ITopSalesRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<object> GetTop10ProductsAsync();
        Task<ProductType[]> GetAllProductTypesAsync();
        Task<object> GetProductsBrandGroupAsync();
        Task<object> GetProductsProductTypeGroupAsync();
        Task<Brand[]> GetAllBrandsAsync();
        Task<User> CheckUserAsync(string username, string password);
        Task<User> CheckUserOTPAsync(string username, int otp);
    }
}
