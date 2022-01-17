using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    //DBset ini berguna untuk penghubung tabel sepertin line 19
    //Berisi row yang melambangkan tabel yang dimiliki oleh projek
    //Cara menentukan relasi atau cardinalitasnya dialkukan di blankspace ini
    public class MyContext : DbContext //DbContext adalam ORM yang nanti digunakan untuk penghubung ke database, berupa Entity Framework
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<namamodel> alias { getter setter }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Profilling> Profillings { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>()
                .HasMany(e => e.Educations)
               .WithOne(u => u.University);

            modelBuilder.Entity<Education>()
                .HasMany(p => p.Profillings)
               .WithOne(e => e.Education);

            modelBuilder.Entity<Employee>()
               .HasOne(a => a.Account)
               .WithOne(e => e.Employee)
               .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(p => p.Profilling)
                .WithOne(a => a.Account)
                .HasForeignKey<Profilling>(p => p.NIK);

            //modelBuilder.Entity<Account>()
            //    .HasMany(ar => ar.accountRoles)
            //    .WithOne(a => a.Account);

            //modelBuilder.Entity<Role>()
            //    .HasMany(ar => ar.accountRoles)
            //    .WithOne(r => r.Role);

            modelBuilder.Entity<AccountRole>()
                .HasOne(ac => ac.Account)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(ac => ac.AccountId);
            modelBuilder.Entity<AccountRole>()
                .HasOne(r => r.Role)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(r => r.RoleId);


        }
    }

}
