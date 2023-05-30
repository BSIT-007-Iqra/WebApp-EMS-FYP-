namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admin()
        {
            Event_Organizers = new HashSet<Event_Organizers>();
        }

        [Key]
        public int Admin_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Admin_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Admin_Email { get; set; }

        [Required]
        [StringLength(15)]
        public string Admin_Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Admin_Contact { get; set; }

        [StringLength(250)]
        public string Admin_Picture { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        public int? Role_FID { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_Organizers> Event_Organizers { get; set; }
    }
}
