namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Report
    {
        [Key]
        public int ReportsID { get; set; }

        public int? RealEstateID { get; set; }

        public int? UserID { get; set; }

        [StringLength(10)]
        public string Contents { get; set; }

        public bool Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public virtual Custommer Custommer { get; set; }

        public virtual RealEstate RealEstate { get; set; }
    }
}
