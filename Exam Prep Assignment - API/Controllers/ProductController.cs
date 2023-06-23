using Exam_Prep_Assignment___API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exam_Prep_Assignment___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //Endpoints for ProductDashboard;
        private readonly ITopSalesRepository _TopSalesRepository;
        public ProductController(ITopSalesRepository topSalesRepository)
        {
            _TopSalesRepository = topSalesRepository;
        }
        [HttpGet]
        [Route("GetTop10ProductsAsync")]
        public async Task<IActionResult> GetTop10ProductsAsync()
        {
            var results = await _TopSalesRepository.GetTop10ProductsAsync();
            return Ok(results);
        }

        [HttpGet]
        [Route("GetAllProductsByBrand")]
        public async Task<IActionResult> GetAllProductsByBrand()
        {
            var results = await _TopSalesRepository.GetProductsBrandGroupAsync();
            return Ok(results);
        }

        [HttpGet]
        [Route("GetAllProductsByProductType")]
        public async Task<IActionResult> GetAllProductsByProductType()
        {
            var results = await _TopSalesRepository.GetProductsProductTypeGroupAsync();
            return Ok(results);
        }
    }
}
