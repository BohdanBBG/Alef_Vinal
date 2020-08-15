using Alef_Vinal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Contexts
{
    public class CodeEntityContext: DbContext
    {
        public CodeEntityContext(DbContextOptions<CodeEntityContext> options)
            :base(options)
        {
             Database.EnsureCreated();
        }

        public DbSet<CodeEntity> CodeEntities { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {

        }
    }
}
