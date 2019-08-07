namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServedProducts
    {
        [Key]
        public int ServeID { get; set; }

        public int ProductID { get; set; }

        public int SellerID { get; set; }

        public double Price { get; set; }

        public virtual Products Products { get; set; }

        public virtual Users Users { get; set; }
    }
}
