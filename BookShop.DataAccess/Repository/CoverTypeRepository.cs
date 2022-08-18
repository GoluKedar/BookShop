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
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType coverType)
        {
            var coverTypeObj = _db.coverTypes.FirstOrDefault(x => x.Id == coverType.Id);
            if (coverTypeObj != null)
            {
                coverTypeObj.Name = coverType.Name;
            }
        }
    }
}
