using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    internal class MyDbContextDesignFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDbContext> options = new DbContextOptionsBuilder<MyDbContext>();

            string connStr = "Data Source=.;Initial Catalog=donetweb;Integrated Security=SSPI;TrustServerCertificate=yes";
            options.UseSqlServer(connStr);
            MyDbContext myDbContext = new MyDbContext(options.Options);
            return myDbContext;
        }
    }
}
