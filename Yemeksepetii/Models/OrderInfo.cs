namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderInfo")]
    public partial class OrderInfo
    {
        [Key]
        public int InfoID { get; set; }

        public int OrderID { get; set; }

        public int? ProductID { get; set; }

        public int? MenuID { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Menus Menus { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Products Products { get; set; }
    }
}
