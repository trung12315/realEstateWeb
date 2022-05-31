namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RealEstate")]
    public partial class RealEstate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RealEstate()
        {
            Images = new HashSet<Image>();
        }

        public int RealEstateID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTile { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public double? Price { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        public long? CategoryID { get; set; }

        [StringLength(500)]
        public string Detail { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateBy { get; set; }

        public bool? Browser { get; set; }

        public bool Status { get; set; }

        public DateTime? Datehire { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public int? UserID { get; set; }

        public int? CatID { get; set; }

        public int? CateID { get; set; }

        [StringLength(10)]
        public string Acreage { get; set; }

        [StringLength(50)]
        public string Lat { get; set; }

        [StringLength(50)]
        public string Lng { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        public virtual RealEstateCategory RealEstateCategory { get; set; }

        public virtual User User { get; set; }
    }
}
