using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using BookShop.Web.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var categoryObj = _db.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (categoryObj != null)
            {
                categoryObj.Name = category.Name;
            }
        }
    }
}
