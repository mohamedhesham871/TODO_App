using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DBContexts
{
    public  class DbApplication(DbContextOptions<DbApplication> option):DbContext(option)
    {
      
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbApplication).Assembly);

            base.OnModelCreating(modelBuilder);
           
        }
        public DbSet<TaskTable> TaskTables { get; set; }
    }
}
