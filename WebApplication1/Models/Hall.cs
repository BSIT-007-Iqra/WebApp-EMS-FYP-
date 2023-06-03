namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hall
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hall()
        {
            Booking_Details = new HashSet<Booking_Details>();
            Feedbacks = new HashSet<Feedback>();
            Packages = new HashSet<Package>();
            Services = new HashSet<Service>();
            Views = new HashSet<View>();
        }

        [Key]
        public int Hall_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Hall_Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Hall_Description { get; set; }

        [Required]
        [StringLength(500)]
        public string Hall_Picture { get; set; }

        [StringLength(500)]
        public string Hall_Picture1 { get; set; }

        [StringLength(500)]
        public string Hall_Picture2 { get; set; }

        [StringLength(500)]
        public string Hall_Picture3 { get; set; }

        public decimal Hall_Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Event_Slot { get; set; }

        public DateTime Event_Time_Slot { get; set; }

        [Required]
        [StringLength(50)]
        public string Hall_Location { get; set; }

        [Required]
        [StringLength(50)]
        public string Staff { get; set; }

        [Required]
        public string Ameities { get; set; }

        [Required]
        [StringLength(50)]
        public string Cancellation_Policy { get; set; }

        [Required]
        [StringLength(50)]
        public string Venue_Type { get; set; }
        [NotMapped]
        public int Quantity { get; set; } = 100;

        public int? Venue_FID { get; set; }

        public decimal? Per_Head_Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking_Details> Booking_Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual Venue Venue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package> Packages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<View> Views { get; set; }
    }
}
