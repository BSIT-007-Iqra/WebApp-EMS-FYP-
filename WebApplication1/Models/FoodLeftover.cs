namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FoodLeftover")]
    public partial class FoodLeftover
    {
        [Key]
        public int FoodRequest_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        public string Reason { get; set; }

        public DateTime Date { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int? Customer_FID { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
