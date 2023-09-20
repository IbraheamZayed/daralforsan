using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Models;

namespace DarAlforsan.Data.Core
{
    public class DBContext : DbContext
    {
        public virtual DbSet<AboutSchool> AboutSchool { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<SaidAboutUs> SaidAboutUs { get; set; }
        public virtual DbSet<SchoolFruits> SchoolFruits { get; set; }
        public virtual DbSet<SchoolFruitsDetails> SchoolFruitsDetails { get; set; }
        public virtual DbSet<SchoolSystem> SchoolSystem { get; set; }
        public virtual DbSet<SchoolVision> SchoolVision { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<SocialMedia> SocialMedia { get; set; }
        public virtual DbSet<Statistics> Statistics { get; set; }
        /// ////////////////////////////////////////////////////////////////
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<ResourceModule> ResourceModule { get; set; }
        public virtual DbSet<ThemeLogin> ThemeLogin { get; set; }
        public virtual DbSet<UILanguage> UILanguage { get; set; }
        public virtual DbSet<User> User { get; set; }
        //-----------------------------------------------------------------------------------------
        public DBContext()
        {

        }
        //-----------------------------------------------------------------------------------------
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        //-----------------------------------------------------------------------------------------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }
        //-----------------------------------------------------------------------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AboutSchoolEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContactUsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NewsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SaidAboutUsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolFruitsDetailsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolFruitsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolSystemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolVisionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SliderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SocialMediaEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StatisticsEntityConfiguration());

            modelBuilder.ApplyConfiguration(new BranchEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceModuleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ThemeLoginEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UILanguageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}