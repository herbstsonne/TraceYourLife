using System.IO;
using Microsoft.EntityFrameworkCore;
using TraceYourLife.Domain.Entities;
using Xamarin.Essentials;

namespace TraceYourLife.Database
{
    public class TraceYourLifeContext : DbContext
    {
        public DbSet<Food> Food { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<TemperaturePerDay> TemperaturePerDay { get; set; }
        public DbSet<WeightPerDay> WeightPerDay { get; set; }

        public TraceYourLifeContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "traceyourlife.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
