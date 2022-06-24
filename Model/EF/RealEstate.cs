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
            Reports = new HashSet<Report>();
        }

        public int RealEstateID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTile { get; set; }

        public string Description { get; set; }

        public int? Price { get; set; }

        [StringLength(500)]
        public string Detail { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public int UserID { get; set; }

        public int CatID { get; set; }

        public int CateID { get; set; }

        public int? Acreage { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(50)]
        public string Lat { get; set; }

        [StringLength(50)]
        public string Lng { get; set; }

        public virtual Category Category { get; set; }

        public virtual Custommer Custommer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        public virtual RealEstateCategory RealEstateCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
