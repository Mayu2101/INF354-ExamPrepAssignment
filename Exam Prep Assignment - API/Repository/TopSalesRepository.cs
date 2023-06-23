using Exam_Prep_Assignment___API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Prep_Assignment___API.Repository
{
    public class TopSalesRepository : ITopSalesRepository
    {
        private readonly AppDbContext _appDbContext;
        public TopSalesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _appDbContext.Add(entity);
        }

        public async Task<User> CheckUserAsync(string username, string password)
        {
            IQueryable<User> query = _appDbContext.Users
                .Where(u => u.username == username && u.Password == password);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> CheckUserOTPAsync(string username, int otp)
        {
            IQueryable<User> query = _appDbContext.Users
                .Where(u => u.username == username && u.otp == otp);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Brand[]> GetAllBrandsAsync()
        {
            IQueryable<Brand> query = _appDbContext.Brands.Distinct();
            return await query.ToArrayAsync();
        }

        public async Task<ProductType[]> GetAllProductTypesAsync()
        {
            IQueryable<ProductType> query = _appDbContext.ProductTypes;
            return await query.ToArrayAsync();
        }

        public async Task<object> GetProductsBrandGroupAsync()
        {
            IQueryable<Product> query = _appDbContext.Products;
            return new
            {
                Results = await query.GroupBy(product => product.Brand.Name)
               .Select(group => new
               {
                   Brand = group.Key,
                   Count = group.Count()
               }).ToListAsync()
            };
        }

        public async Task<object> GetProductsProductTypeGroupAsync()
        {
            IQueryable<Product> query = _appDbContext.Products;
            return new
            {
                Results = await query.GroupBy(product => product.ProductType.Name)
               .Select(group => new
               {
                   ProductType = group.Key,
                   Count = group.Count()
               }).ToListAsync()
            };
        }

        public async Task<object> GetTop10ProductsAsync()
        {
            IQueryable<Product> query = _appDbContext.Products;
            return new
            {
                Results = await query.OrderByDescending(u => u.Price).Take(10)
                .Select(product => new
                {
                    name = product.Name,
                    price = product.Price,
                    productType = product.ProductType.Name,
                    brand = product.Brand.Name,
                    description = product.Description
                }).ToListAsync()
            };
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
