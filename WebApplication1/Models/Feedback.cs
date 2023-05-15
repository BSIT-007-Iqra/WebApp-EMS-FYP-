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
        [Key]
        public int Feedback_ID { get; set; }

        [StringLength(100)]
        public string Feedback_Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Feedback_Date { get; set; }

        public int Customer_FID { get; set; }

        public int? Venue_FID { get; set; }

        public int? Service_FID { get; set; }

        public int? Event_FID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Event_tbl Event_tbl { get; set; }

        public virtual Venue Venue { get; set; }

        public virtual Service Service { get; set; }
    }
}
