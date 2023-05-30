namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event_Organizers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event_Organizers()
        {
            Notifications = new HashSet<Notification>();
            Services = new HashSet<Service>();
        }

        [Key]
        public int EventOrganizer_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string EventOrganizer_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string EventOrganizer_Email { get; set; }

        [Required]
        [StringLength(20)]
        public string EventOrganizer_Password { get; set; }

        [Required]
        [StringLength(20)]
        public string EventOrganizer_Contact { get; set; }

        [StringLength(20)]
        public string EventOrganizer_Gender { get; set; }

        public string EventOrganizer_Picture { get; set; }

        public int Status { get; set; }

        public int? Admin_FID { get; set; }

        public virtual Admin Admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }
    }
}
