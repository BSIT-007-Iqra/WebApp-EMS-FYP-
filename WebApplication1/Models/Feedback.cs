namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Feedback()
        {
            Feedback1 = new HashSet<Feedback>();
        }

        [Key]
        public int Feedback_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Feedback_Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime Feedback_Date { get; set; }

        public int Customer_FID { get; set; }

        public int? Venue_FID { get; set; }

        public int? Service_FID { get; set; }

        public int? Hall_FID { get; set; }

        public int? Feedback_FID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Hall Hall { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedback1 { get; set; }

        public virtual Feedback Feedback2 { get; set; }

        public virtual Venue Venue { get; set; }

        public virtual Service Service { get; set; }
    }
}
