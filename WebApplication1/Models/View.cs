namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("View")]
    public partial class View
    {
        [Key]
        public int View_ID { get; set; }

        public DateTime? View_Date { get; set; }

        public int? Customer_FID { get; set; }

        public int? Hall_FID { get; set; }

        public int? Service_FID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Hall Hall { get; set; }

        public virtual Service Service { get; set; }
    }
}
