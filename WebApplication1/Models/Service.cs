namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Service")]
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            Booking_Details = new HashSet<Booking_Details>();
            Feedbacks = new HashSet<Feedback>();
            Views = new HashSet<View>();
        }

        [Key]
        public int Service_ID { get; set; }

        [StringLength(50)]
        public string Service_Name { get; set; }

        [StringLength(500)]
        public string Service_Description { get; set; }

        [Required]
        public string Service_Picture { get; set; }

        public decimal? Service_Price { get; set; }

        public DateTime? Service_Date { get; set; }

        [StringLength(20)]
        public string Service_Status { get; set; }

        public int? Organizer_FID { get; set; }

        public int? SubCategory_FID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking_Details> Booking_Details { get; set; }

        public virtual Event_Organizers Event_Organizers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual Sub_ServiceCategory Sub_ServiceCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<View> Views { get; set; }
    }
}
