using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace realEstateWeb.Models
{
    public class CategoryModel
    {
        public List<RealEstateViewModel> ListByCategoryId { get; set; }
        //public RealEstate RealEstate { get; set; }
    }
}