using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class RealEstateViewModel
    {
        public int RealEstateID { get; set; }

    
        public string Name { get; set; }

   
        public string MetaTile { get; set; }
        public string CateName { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string Image { get; set; }


        public string Detail { get; set; }

        public DateTime? CreateDate { get; set; }
        //public string CateMetaTitle { set; get; }

        public string CreateBy { get; set; }



        public string Address { get; set; }

        public string Acreage { get; set; }

    }
}
