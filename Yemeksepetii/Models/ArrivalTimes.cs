namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArrivalTimes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArrivalTimes()
        {
            ArrivalTime = new HashSet<ArrivalTime>();
        }

        public int ArrivalTimesID { get; set; }

        [Required]
        [StringLength(15)]
        public string ArrivalTimeText { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArrivalTime> ArrivalTime { get; set; }
    }
}
