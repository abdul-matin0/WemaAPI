using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WemaAPI.Data.Repository.IRepository;
using WemaAPI.Models;

namespace WemaAPI.Data.Repository
{
    public class LGARepository : Repository<LGA>, ILGARepository
    {
        private readonly ApplicationDbContext _db;
        public LGARepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
