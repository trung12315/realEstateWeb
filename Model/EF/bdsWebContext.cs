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

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
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
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Village> Villages { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RealEstate>()
                .Property(e => e.MetaTile)
                .IsUnicode(false);

            modelBuilder.Entity<RealEstate>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

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
