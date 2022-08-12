using FinanceKeeper.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceKeeper.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<FinancialCategory?>? FinancialCategories { get; set; } = null;
        public DbSet<FinancialOperation?>? FinancialOperations { get; set; } = null;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FinancialCategory>(entity =>
            {
                entity.HasKey(x => x.CategoryId);
                entity.ToTable("FinancialCategory");
                
            });
            modelBuilder.Entity<FinancialOperation>(entity =>
            {
                entity.HasKey(e => e.OperationId);
                //entity
                    //.HasOne(x => x.Category)
                    //.WithMany()
                    ////.HasForeignKey(key => key.FinancialCategoryId);

                entity.ToTable("FinancialOperation");
            });
        }
    }
}
