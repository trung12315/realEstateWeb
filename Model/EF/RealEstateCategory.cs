namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RealEstateCategory")]
    public partial class RealEstateCategory
    {
        [Key]
        public int CateID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        public bool Status { get; set; }

        public int? Sort { get; set; }

        public int? ParentID { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(50)]
        public string MetaDescription { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
