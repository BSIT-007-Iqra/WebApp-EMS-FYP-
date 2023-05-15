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
        [Key]
        public int Package_ID { get; set; }

        [StringLength(50)]
        public string Package_Name { get; set; }

        [StringLength(200)]
        public string Package_Details { get; set; }

        public string Service { get; set; }

        public decimal? Price { get; set; }
    }
}
