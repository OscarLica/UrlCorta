using Microsoft.EntityFrameworkCore;
using NETChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Config
{
    public static class ApplicationDbContextInMemory
    {
        public static ApplicationDbContext Get() {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName : "ShortUrl").Options;

            return new ApplicationDbContext(options);
        }
    }
}
