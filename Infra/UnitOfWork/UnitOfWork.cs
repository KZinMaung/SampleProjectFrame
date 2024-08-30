using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.UnitOfWork
{
    public class UnitOfWork
    {
        private AppDBContext _ctx;

        public UnitOfWork(AppDBContext ctx)
        {
            _ctx = ctx;
        }
        public UnitOfWork()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
            var contextOptions = new DbContextOptionsBuilder<AppDBContext>()
              .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
              .Options;
            _ctx = new AppDBContext(contextOptions);

        }

        ~UnitOfWork()
        {
            _ctx.Dispose();
        }

    }
}
