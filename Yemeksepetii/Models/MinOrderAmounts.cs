namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MinOrderAmounts
    {
        [Key]
        public int MOAID { get; set; }

        public int SellerID { get; set; }

        public int OrdererLocationID { get; set; }

        [Column(TypeName = "money")]
        public decimal MOA { get; set; }

        public virtual Locations Locations { get; set; }

        public virtual Users Users { get; set; }
    }
}
