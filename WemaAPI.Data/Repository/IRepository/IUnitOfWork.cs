using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAPI.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        public ICustomerRepository Customer { get; }
        public IStateOfResidenceRepository StateOfResidence { get; }
        public ILGARepository LGA { get; }
        public IOTPRepository OTP { get; }

        void SaveAsync();
    }
}
