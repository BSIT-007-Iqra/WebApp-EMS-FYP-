namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            Booking_Details = new HashSet<Booking_Details>();
        }

        [Key]
        public int Booking_ID { get; set; }

        [StringLength(150)]
        public string Booking_Name { get; set; }

        public DateTime Event_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Booking_Status { get; set; }

        [Required]
        [StringLength(50)]
        public string Event_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Booking_Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Payment_Type { get; set; }

        public int? Customer_FID { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking_Details> Booking_Details { get; set; }
    }
}
