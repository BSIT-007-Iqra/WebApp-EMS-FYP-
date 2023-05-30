namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Venue")]
    public partial class Venue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venue()
        {
            Feedbacks = new HashSet<Feedback>();
            Halls = new HashSet<Hall>();
            Packages = new HashSet<Package>();
        }

        [Key]
        public int Venue_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Venue_Name { get; set; }

        [Required]
        public string Venue_Details { get; set; }

        [Required]
        public string Venue_Picture { get; set; }

        [Required]
        [StringLength(50)]
        public string Venue_Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Venue_Contact { get; set; }

        [Required]
        [StringLength(100)]
        public string Venue_Location { get; set; }

        public decimal Venue_Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hall> Halls { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package> Packages { get; set; }
    }
}
