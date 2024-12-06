using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Context 
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<WorkTable> WorkTables { get; set; }
        public DbSet<User> Users { get; set; }
        private readonly IConfiguration config;

        string connectionString = "Host=localhost;Port=5432; Database=Git_LMS; " +
            "Username=postgres; Password=123";

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{
        //    Database.EnsureCreated();   // создаем базу данных при первом обращении
        //}


        //public AppDbContext(IConfiguration config)
        //{
        //    this.config = config;
        //}
    }
}
