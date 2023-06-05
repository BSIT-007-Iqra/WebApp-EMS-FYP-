namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Website_Details
    {
        [Key]
        public int Website_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Website_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Website_Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Website_Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Website_Contact { get; set; }

        [StringLength(200)]
        public string Website_Logo { get; set; }
    }
}
