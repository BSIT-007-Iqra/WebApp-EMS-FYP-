using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication1.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Booking_Details> Booking_Details { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Event_Organizers> Event_Organizers { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Service_Category> Service_Category { get; set; }
        public virtual DbSet<Sub_ServiceCategory> Sub_ServiceCategory { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<Website_Details> Website_Details { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Event_Organizers)
                .WithOptional(e => e.Admin)
                .HasForeignKey(e => e.Admin_FID);

            modelBuilder.Entity<Booking>()
                .HasMany(e => e.Booking_Details)
                .WithOptional(e => e.Booking)
                .HasForeignKey(e => e.Booking_FID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Bookings)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.Customer_FID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Feedbacks)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.Customer_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Views)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.Customer_FID);

            modelBuilder.Entity<Event_Organizers>()
                .HasMany(e => e.Notifications)
                .WithOptional(e => e.Event_Organizers)
                .HasForeignKey(e => e.EventOrganizers_FID);

            modelBuilder.Entity<Event_Organizers>()
                .HasMany(e => e.Services)
                .WithOptional(e => e.Event_Organizers)
                .HasForeignKey(e => e.Organizer_FID);

            modelBuilder.Entity<Feedback>()
                .HasMany(e => e.Feedback1)
                .WithOptional(e => e.Feedback2)
                .HasForeignKey(e => e.Feedback_FID);

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Booking_Details)
                .WithOptional(e => e.Hall)
                .HasForeignKey(e => e.Hall_FID);

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Feedbacks)
                .WithOptional(e => e.Hall)
                .HasForeignKey(e => e.Hall_FID);

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Views)
                .WithOptional(e => e.Hall)
                .HasForeignKey(e => e.Hall_FID);

            modelBuilder.Entity<Role>()
                .Property(e => e.Role_Name)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Admins)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.Role_FID);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Booking_Details)
                .WithOptional(e => e.Service)
                .HasForeignKey(e => e.Service_FID);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Feedbacks)
                .WithOptional(e => e.Service)
                .HasForeignKey(e => e.Service_FID);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Views)
                .WithOptional(e => e.Service)
                .HasForeignKey(e => e.Service_FID);

            modelBuilder.Entity<Service_Category>()
                .HasMany(e => e.Sub_ServiceCategory)
                .WithOptional(e => e.Service_Category)
                .HasForeignKey(e => e.Service_Category_FID);

            modelBuilder.Entity<Sub_ServiceCategory>()
                .HasMany(e => e.Services)
                .WithOptional(e => e.Sub_ServiceCategory)
                .HasForeignKey(e => e.SubCategory_FID);

            modelBuilder.Entity<Venue>()
                .HasMany(e => e.Feedbacks)
                .WithOptional(e => e.Venue)
                .HasForeignKey(e => e.Venue_FID);

            modelBuilder.Entity<Venue>()
                .HasMany(e => e.Halls)
                .WithOptional(e => e.Venue)
                .HasForeignKey(e => e.Venue_FID);
        }
    }
}
