namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking_Details
    {
        [Key]
        public int BookingDetail_ID { get; set; }

        public decimal? Price { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public int? Booking_FID { get; set; }

        public int? Hall_FID { get; set; }

        public int? Service_FID { get; set; }

        public virtual Booking Booking { get; set; }

        public virtual Hall Hall { get; set; }

        public virtual Service Service { get; set; }
    }
}
