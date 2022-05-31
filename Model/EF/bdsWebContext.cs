using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class bdsWebContext : DbContext
    {
        public bdsWebContext()
            : base("name=bdsWebContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<RealEstate> RealEstates { get; set; }
        public virtual DbSet<RealEstateCategory> RealEstateCategories { get; set; }
        public virtual DbSet<RealEstateComment> RealEstateComments { get; set; }
        public virtual DbSet<RealEstateTag> RealEstateTags { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<district> districts { get; set; }
        public virtual DbSet<province> provinces { get; set; }
        public virtual DbSet<village> villages { get; set; }
        public virtual DbSet<ward> wards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>()
                .Property(e => e.UpdateBy)
                .HasPrecision(18, 0);

            modelBuilder.Entity<RealEstate>()
                .Property(e => e.MetaTile)
                .IsUnicode(false);

            modelBuilder.Entity<RealEstate>()
                .Property(e => e.Acreage)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
