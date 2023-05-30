namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_ServiceCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sub_ServiceCategory()
        {
            Services = new HashSet<Service>();
        }

        [Key]
        public int SubCategory_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Sub_category_Name { get; set; }

        public int Service_Category_FID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }

        public virtual Service_Category Service_Category { get; set; }
    }
}
