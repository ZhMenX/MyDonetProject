using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Identity
{
    internal class IdDbContextDesignFactory : IDesignTimeDbContextFactory<IdDbContext>
    {
        public IdDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<IdDbContext> options = new DbContextOptionsBuilder<IdDbContext>();

            string connStr = "Data Source=.;Initial Catalog=donetweb;Integrated Security=SSPI;TrustServerCertificate=yes";
            options.UseSqlServer(connStr);
            IdDbContext myDbContext = new IdDbContext(options.Options);
            return myDbContext;
        }
    }
}
