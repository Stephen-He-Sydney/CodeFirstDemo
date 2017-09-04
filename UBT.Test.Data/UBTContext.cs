using System;
using System.Data.Entity;
using UBTTest.Data.Domain;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace UBTTest.Data
{
    public class UBTContext : DbContext
    {
        public UBTContext()
            : base("name=UBTContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Database.SetInitializer(new DropCreateDatabaseAlways<UBTContext>());

            Database.SetInitializer(new UBTContextCustomInitializer());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
