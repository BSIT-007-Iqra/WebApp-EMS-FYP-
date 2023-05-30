namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Service_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service_Category()
        {
            Sub_ServiceCategory = new HashSet<Sub_ServiceCategory>();
        }

        [Key]
        public int ServiceCategory_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceCategory_Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sub_ServiceCategory> Sub_ServiceCategory { get; set; }
    }
}
