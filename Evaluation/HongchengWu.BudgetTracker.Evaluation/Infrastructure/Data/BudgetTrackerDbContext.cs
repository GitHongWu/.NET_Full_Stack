using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class BudgetTrackerDbContext : DbContext
    {
        public BudgetTrackerDbContext(DbContextOptions<BudgetTrackerDbContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Income>(ConfigureIncome);
            modelBuilder.Entity<Expenditure>(ConfigureExpenditure);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(10).IsRequired();
            builder.Property(u => u.FullName).HasMaxLength(50);
            builder.Property(u => u.JoinedOn).HasDefaultValueSql("getdate()");
        }

        private void ConfigureIncome(EntityTypeBuilder<Income> builder)
        {
            builder.ToTable("Income");
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.User).WithMany(i => i.Incomes).HasForeignKey(i => i.UserId);
            builder.Property(i => i.Amount).HasColumnType("money").IsRequired();
            builder.Property(e => e.Description).HasMaxLength(100);
            builder.Property(e => e.Remarks).HasMaxLength(500);
        }

        private void ConfigureExpenditure(EntityTypeBuilder<Expenditure> builder)
        {
            builder.ToTable("Expenditure");
            builder.HasKey(u => u.Id);
            builder.HasOne(i => i.User).WithMany(i => i.Expenditures).HasForeignKey(i => i.UserId);
            builder.Property(i => i.Amount).HasColumnType("money").IsRequired();
            builder.Property(e => e.Description).HasMaxLength(100);
            builder.Property(e => e.Remarks).HasMaxLength(500);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }
    }
}
