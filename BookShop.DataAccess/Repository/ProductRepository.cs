using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using BookShop.Web.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productObj = _db.Products.FirstOrDefault(x => x.Id == product.Id);
            if (productObj != null)
            {
                if (productObj.ImageUrl != null)
                    productObj.ImageUrl = product.ImageUrl;
                productObj.Title = product.Title;
                productObj.ISBN = product.ISBN;
                productObj.Description = product.Description;
                productObj.Author = product.Author;
                productObj.CategoryId = product.CategoryId;
                productObj.CoverTypeId = product.CoverTypeId;
                productObj.ListPrice = product.ListPrice;
                productObj.Price = product.Price;
                productObj.Price50 = product.Price50;
                productObj.Price100 = product.Price100;
            }
        }
    }
}
