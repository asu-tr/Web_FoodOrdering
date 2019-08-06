namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuContent")]
    public partial class MenuContent
    {
        public int MenuContentID { get; set; }

        public int MenuID { get; set; }

        public int ProductID { get; set; }

        public virtual Menus Menus { get; set; }

        public virtual Products Products { get; set; }
    }
}
