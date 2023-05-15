namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        public int ID { get; set; }

        [StringLength(500)]
        public string Message { get; set; }

        public DateTime? DateTime { get; set; }

        public int? EventOrganizers_FID { get; set; }

        public virtual Event_Organizers Event_Organizers { get; set; }
    }
}
