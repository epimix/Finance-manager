using Microsoft.EntityFrameworkCore;
using TeamProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TeamProject.Database
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
        {
        }
    }
}
