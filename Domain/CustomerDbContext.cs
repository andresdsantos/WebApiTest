using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApiJwt.Domain
{
    public partial class CustomerDbContext : DbContext,IDataContext
    {
        public CustomerDbContext()
            : base("name=CustomerDbContext")
        {
        }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public void ExecuteComand(string command, params object[] parameters)
        {
            base.Database.ExecuteSqlCommand(command, parameters);
        }

    }
}
