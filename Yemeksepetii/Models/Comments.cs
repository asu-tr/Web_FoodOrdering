namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        [Key]
        public int CommentID { get; set; }

        public int SellerID { get; set; }

        public int OrdererID { get; set; }

        public int OrderID { get; set; }

        [StringLength(20)]
        public string CommentTitle { get; set; }

        [StringLength(150)]
        public string Comment { get; set; }

        public int SpeedScore { get; set; }

        public int ServiceScore { get; set; }

        public int TasteScore { get; set; }

        public virtual Users Users { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Users Users1 { get; set; }
    }
}
