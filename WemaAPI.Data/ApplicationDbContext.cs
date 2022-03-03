using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WemaAPI.Models;

namespace WemaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<StateOfResidence> StateOfResidence { get; set; }
        public DbSet<LGA> LGA { get; set; }

        public DbSet<OTP> OTP { get; set; }
    }
}
