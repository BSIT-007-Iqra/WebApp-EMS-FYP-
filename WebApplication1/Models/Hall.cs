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
            Views = new HashSet<View>();
        }

        [Key]
        public int Hall_ID { get; set; }

        [StringLength(50)]
        public string Hall_Name { get; set; }

        [StringLength(500)]
        public string Hall_Description { get; set; }

        [StringLength(500)]
        public string Hall_Picture { get; set; }

        [StringLength(500)]
        public string Hall_Picture1 { get; set; }

        [StringLength(500)]
        public string Hall_Picture2 { get; set; }

        [StringLength(500)]
        public string Hall_Picture3 { get; set; }

        public decimal? Hall_Price { get; set; }

        [StringLength(50)]
        public string Event_Slot { get; set; }

        public DateTime? Event_Time_Slot { get; set; }

        [StringLength(50)]
        public string Hall_Location { get; set; }

        [StringLength(50)]
        public string Staff { get; set; }

        public string Ameities { get; set; }

        [StringLength(50)]
        public string Cancellation_Policy { get; set; }

        [StringLength(50)]
        public string Venue_Type { get; set; }

        public int? Venue_FID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking_Details> Booking_Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual Venue Venue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<View> Views { get; set; }
    }
}
