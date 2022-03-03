using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WemaAPI.Data.Repository.IRepository;

namespace WemaAPI.Data.Repository
{

    /// <summary>
    /// Repository UnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            Customer = new CustomerRepository(_db);
            StateOfResidence = new StateOfResidenceRepository(_db);
            LGA = new LGARepository(_db);
            OTP = new OTPRepository(_db);
        }

        public ICustomerRepository Customer { get; private set; }

        public IStateOfResidenceRepository StateOfResidence { get; private set; }

        public ILGARepository LGA { get; private set; }
        public IOTPRepository OTP { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
