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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company Company)
        {
            var CompanyObj = _db.Companies.FirstOrDefault(x => x.Id == Company.Id);
            if (CompanyObj != null)
            {
                CompanyObj.Name = Company.Name;
                CompanyObj.StreetAddress = Company.StreetAddress;
                CompanyObj.City = Company.City;
                CompanyObj.State = Company.State;
                CompanyObj.PostalCode = Company.PostalCode;
                CompanyObj.PhoneNumber = Company.PhoneNumber;
                CompanyObj.IsAuthorizedCompany = Company.IsAuthorizedCompany;
            }
        }
    }
}
