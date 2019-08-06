namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            Comments = new HashSet<Comments>();
            OrderInfo = new HashSet<OrderInfo>();
        }

        [Key]
        public int OrderID { get; set; }

        public DateTime OrderTime { get; set; }

        public int OrdererID { get; set; }

        public int SellerID { get; set; }

        public int OrderStatusID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }

        public virtual Users Users { get; set; }

        public virtual OrderStats OrderStats { get; set; }

        public virtual Users Users1 { get; set; }
    }
}
