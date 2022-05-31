namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Image")]
    public partial class Image
    {
        public int ImageID { get; set; }

        [StringLength(250)]
        public string LinkImage { get; set; }

        public int? RealEstateID { get; set; }

        public virtual RealEstate RealEstate { get; set; }
    }
}
