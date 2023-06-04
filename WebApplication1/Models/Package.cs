namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Package")]
    public partial class Package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Package()
        {
            Booking_Details = new HashSet<Booking_Details>();
        }

        [Key]
        public int Package_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Package_Name { get; set; }

        [Required]
        public string Package_Details { get; set; }

        public int Service_FID { get; set; }

        public int Hall_FID { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public decimal Packages_Price { get; set; }
        [NotMapped]
        public int Quantity { get; set; } = 100;
        public decimal Per_Head_Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking_Details> Booking_Details { get; set; }

        public virtual Hall Hall { get; set; }

        public virtual Service Service { get; set; }
    }
}
