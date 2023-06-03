namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Withdraw_Amount
    {
        public int Id { get; set; }

        [Column("Withdraw_Amount")]
        public decimal Withdraw_Amount1 { get; set; }

        public DateTime Request_Time { get; set; }

        public int Organizer_FID { get; set; }

        public DateTime Recive_Time { get; set; }

        public bool? IsTranffered { get; set; }

        [Required]
        [StringLength(50)]
        public string Paypal_Account { get; set; }

        public virtual Event_Organizers Event_Organizers { get; set; }
    }
}
