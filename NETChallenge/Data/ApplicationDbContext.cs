using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NETChallenge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETChallenge.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Url> Url{ get; set; }
        public DbSet<UrlDetail> UrlDetails { get; set; }
    }
}
