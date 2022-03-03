using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WemaAPI.Data.Repository.IRepository;
using WemaAPI.Models;

namespace WemaAPI.Data.Repository
{
    public class StateOfResidenceRepository : Repository<StateOfResidence>, IStateOfResidenceRepository
    {
        private readonly ApplicationDbContext _db;
        public StateOfResidenceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
