namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WorkingHours
    {
        [Key]
        public int WHID { get; set; }

        public int SellerID { get; set; }

        public TimeSpan OpensAt { get; set; }

        public TimeSpan ClosesAt { get; set; }

        public virtual Users Users { get; set; }
    }
}
