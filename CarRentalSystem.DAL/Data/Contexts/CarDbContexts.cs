using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.DAL.Data.Contexts
{
    public class CarDbContexts : DbContext
    {
        public CarDbContexts(DbContextOptions<CarDbContexts> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Car> Cars { get; set; }
    }
}
