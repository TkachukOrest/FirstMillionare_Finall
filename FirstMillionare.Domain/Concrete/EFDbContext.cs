using System;
using System.Data.Entity;
using FirstMillionare.Domain.Entities;

namespace FirstMillionare.Domain.Concrete
{    
    internal class EFDbContext : DbContext
    {
        public EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(null);
        }
        
        public DbSet<Question> Questions { get; set; }        
        public DbSet<Option> Options { get; set; }        
        public DbSet<Answer> Answers { get; set; }
    }
}
