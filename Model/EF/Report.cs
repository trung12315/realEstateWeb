namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Report
    {
        public int ID { get; set; }

        public int? RaelEstateTag { get; set; }

        public bool Status { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? UpdateBy { get; set; }
    }
}
