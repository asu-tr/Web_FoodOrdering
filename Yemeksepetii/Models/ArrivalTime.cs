namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArrivalTime")]
    public partial class ArrivalTime
    {
        [Key]
        public int ArrivalID { get; set; }

        public int SellerID { get; set; }

        public int OrdererLocationID { get; set; }

        public int ArrivalTimeID { get; set; }

        public virtual ArrivalTimes ArrivalTimes { get; set; }

        public virtual Locations Locations { get; set; }

        public virtual Users Users { get; set; }
    }
}
