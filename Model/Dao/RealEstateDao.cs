using Common;
using Model.EF;
using Model.ViewModel;
using Model1.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model.Dao
{
    public class RealEstateDao
    {
        bdsWebContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public RealEstateDao()
        {
            db = new bdsWebContext();
        }
        //public RealEstate DocXml()
        //{
        //    List<ImageModel> imagesmodel = new List<ImageModel>();
        //    XmlDocument doc = new XmlDocument();

        //}
        public IEnumerable<RealEstate> Manage(int id, string searchString, int page, int pageSize = 3)
        {
            IQueryable<RealEstate> model = db.RealEstates;
            if (!string.IsNullOrEmpty(searchString)) {
                model = model.Where(x => x.UserID == id && x.Name.Contains(searchString));
            }
               
            return model.Where(x => x.UserID == id).OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize); ;
        }
        public string TongBaiDang(string txtNgayThangNamSinh)
        {
            string TongBaiDang;
            if (!string.IsNullOrEmpty(txtNgayThangNamSinh))
            {
                var date = Convert.ToDateTime(txtNgayThangNamSinh).Date;
                var nextDay = date.AddDays(1);
                TongBaiDang = db.RealEstates.Where(x => x.CreateDate >= date && x.CreateDate < nextDay).Count().ToString();
            }
            else
            {
                TongBaiDang = db.RealEstates.Count().ToString();
            }

            return TongBaiDang;
        }
        public RealEstate GetByID(long id)
        {
            return db.RealEstates.Find(id);
        }
        public long Insert(RealEstate entity)
        {
            db.RealEstates.Add(entity);
            entity.MetaTile = entity.Name.RemoveDiacritics().Replace(" ", "-");
            entity.CreateDate = DateTime.Now;
            db.SaveChanges();
            return entity.RealEstateID;
        }
        public bool Update(RealEstate entity)
        {

            try
            {
                var realEstate = db.RealEstates.Find(entity.RealEstateID);
                realEstate.Name = entity.Name;
                realEstate.MetaTile = entity.Name.RemoveDiacritics().Replace(" ", "-");
                realEstate.Description = entity.Description;
                realEstate.Detail = entity.Detail;
                realEstate.Image = entity.Image;

                realEstate.Price = entity.Price;
                realEstate.Address = entity.Address;
                //realEstate.ModifiedDate = DateTime.Now.Date;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Logging

                return false;
            }

        }
        public void UpdateImage(string images)
        {
            RealEstate realestate = new RealEstate();
            int foreignKey = realestate.RealEstateID;
            Image image = new Image();
            image.RealEstateID = foreignKey;
            image.LinkImage = images;
            db.SaveChanges();
        }
        public void DeleteImage(int id)
        {

            var a = db.Images.Where(x => x.RealEstateID == id).ToList();
            foreach (var item in a)
            {
                db.Images.Remove(item);
                db.SaveChanges();
            }
        }
        public void UpdateImages(int id, string images)
        {
            
            Image image = new Image();
            image.LinkImage = images;
            image.RealEstateID = id;
            db.Images.Add(image);
            db.SaveChanges();
        }
        public bool UpdateMaps(RealEstate entity)
        {

            try
            {
                var realEstate = db.RealEstates.Find(entity.RealEstateID);
                realEstate.Name = entity.Name;
                realEstate.MetaTile = entity.Name.RemoveDiacritics().Replace(" ", "-");
                realEstate.Description = entity.Description;
                realEstate.Detail = entity.Detail;
                realEstate.Image = entity.Image;
                realEstate.Lat = entity.Lat;
                realEstate.Lng = entity.Lng;
                //content.ViewCount = entity.ViewCount;

                realEstate.Price = entity.Price;
                realEstate.Address = entity.Address;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Logging

                return false;
            }

        }
        public List<RealEstate> ListByCategoryId(int categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 6)
        {
            totalRecord = db.RealEstates.Where(x => x.CatID == categoryID).Count();
            var model = db.RealEstates.Where(x => x.CatID == categoryID).OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }
        public IEnumerable<RealEstate> Search(string search, string searchString, int pageIndex , int pageSize=6 )
        {
            IQueryable<RealEstate> model = db.RealEstates;
            if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(search))
            {
                //totalRecord = db.RealEstates.Where(x => x.Address.Contains(SearchString) && x.RealEstateCategory.Name.Contains(search)).Count();
                model = model.Where(x => x.Address.Contains(searchString) && x.RealEstateCategory.Name.Contains(search));
            }
            else
            {
                model = db.RealEstates;
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(pageIndex,pageSize);
            
        }
        public IEnumerable<RealEstate> ListAllPaging(string searchString, int page=5, int pageSize=12)
        {
            IQueryable<RealEstate> model = db.RealEstates;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) && x.Status == true || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<RealEstate> ListAllMaps(string searchString, int page, int pageSize)
        {
            IQueryable<RealEstate> model = db.RealEstates;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) && x.Status == true );//&& x.ViewCount >= 400
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<RealEstate> ListAllPaging(int page, int pageSize)
        {
            IQueryable<RealEstate> model = db.RealEstates;
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<RealEstate> ListAllByTag(string tag, int page, int pageSize)
        {
            var model = (from a in db.RealEstates
                         join b in db.RealEstateTags
                         on a.RealEstateID equals b.RealEstateID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             MetaTile = a.MetaTile,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreateDate,

                             RealEstateID = a.RealEstateID

                         }).AsEnumerable().Select(x => new RealEstate()
                         {
                             Name = x.Name,
                             MetaTile = x.MetaTile,
                             Image = x.Image,
                             Description = x.Description,
                             CreateDate = x.CreatedDate,

                             RealEstateID = x.RealEstateID
                         });
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public List<RealEstate> ListNewRealEstate(int top)
        {
            return db.RealEstates.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public RealEstate ViewDetail(int id)
        {
            return db.RealEstates.Find(id);
        }
        public List<Image> ViewDetail2(int id)
        {
            return db.Images.Where(x=>x.RealEstateID==id).ToList();
        }
        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var content = db.RealEstates.Find(id);
                db.RealEstates.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public long Create(RealEstateViewModel viewModel)
        {

            RealEstate realestate = new RealEstate();
            DateTime dt = DateTime.Now;
            String.Format("{0:dd/MM/yyyy}", dt);
            //Xử lý alias
            //if (string.IsNullOrEmpty(realestate.MetaTile))
            //{
            //    realestate.MetaTile = StringHelper.ToUnsignString(realestate.Name);
            //}
            realestate.CreateDate = dt;
            realestate.Address = viewModel.Address;
            realestate.Name = viewModel.Name;
            realestate.CateID = viewModel.CateID;
            realestate.CatID = viewModel.CatID;
            realestate.Description = viewModel.Description;
            realestate.Acreage = viewModel.Acreage;
            realestate.Detail = viewModel.Detail;
            realestate.Status = viewModel.Status;

            db.RealEstates.Add(realestate);
            db.SaveChanges();
            int foreignKey = realestate.RealEstateID;
            Image image = new Image();
            image.LinkImage = viewModel.LinkImage;
            image.RealEstateID = foreignKey;
            db.Images.Add(image);
            db.SaveChanges();

            //Xử lý tag
            //if (!string.IsNullOrEmpty(realRstate.Tags))
            //{
            //    string[] tags = content.Tags.Split(',');
            //    foreach (var tag in tags)
            //    {
            //        var tagId = StringHelper.ToUnsignString(tag);
            //        var existedTag = this.CheckTag(tagId);

            //        //insert to to tag table
            //        if (!existedTag)
            //        {
            //            this.InsertTag(tagId, tag);
            //        }

            //        //insert to content tag
            //        this.InsertContentTag(content.ID, tagId);

            //    }
            //}

            return viewModel.RealEstateID;
        }

        public long Edit(RealEstate realRstate)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(realRstate.MetaTile))
            {
                realRstate.MetaTile = StringHelper.ToUnsignString(realRstate.Name);
            }
            realRstate.CreateDate = DateTime.Now;
            db.SaveChanges();

            //Xử lý tag
            //if (!string.IsNullOrEmpty(content.Tags))
            //{
            //    this.RemoveAllContentTag(content.ID);
            //    string[] tags = content.Tags.Split(',');
            //    foreach (var tag in tags)
            //    {
            //        var tagId = StringHelper.ToUnsignString(tag);
            //        var existedTag = this.CheckTag(tagId);

            //        //insert to to tag table
            //        if (!existedTag)
            //        {
            //            this.InsertTag(tagId, tag);
            //        }

            //        //insert to content tag
            //        this.InsertContentTag(content.ID, tagId);

            //    }
            //}

            return realRstate.RealEstateID;
        }
        public List<Image> ListImages(long id, int a=1)
        {
            var image = db.Images.Find(id);
            return db.Images.Where(x => x.RealEstateID == id).ToList();
        }
        public List<Image> ListImage(long id)
        {
            var image = db.Images.Find(id);
            return db.Images.Where(x => x.RealEstateID == id ).ToList();
        }
        public List<RealEstate> Listrelatedproducts(long id)
        {
            var realEstate = db.RealEstates.Find(id);
            return db.RealEstates.Where(x => x.RealEstateID != id && x.CatID == realEstate.CatID ).OrderByDescending(x => x.CreateDate).Take(7).ToList();
        }
   
        //public void RemoveAllContentTag(long contentId)
        //{
        //    db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
        //    db.SaveChanges();
        //}

        //public void InsertTag(string id, string name)
        //{
        //    var tag = new Tag();
        //    tag.ID = id;
        //    tag.Name = name;
        //    db.Tags.Add(tag);
        //    db.SaveChanges();
        //}
        //public void InsertContentTag(long contentId, string tagId)
        //{
        //    var contentTag = new ContentTag();
        //    contentTag.ContentID = contentId;
        //    contentTag.TagID = tagId;
        //    db.ContentTags.Add(contentTag);
        //    db.SaveChanges();
        //}
        //public bool CheckTag(string id)
        //{
        //    return db.Tags.Count(x => x.ID == id) > 0;
        //}
        //public List<Tag> ListTag(long contentId)
        //{
        //    var model = (from a in db.Tags
        //                 join b in db.ContentTags
        //                 on a.ID equals b.TagID
        //                 where b.ContentID == contentId
        //                 select new
        //                 {
        //                     ID = b.TagID,
        //                     Name = a.Name
        //                 }).AsEnumerable().Select(x => new Tag()
        //                 {
        //                     ID = x.ID,
        //                     Name = x.Name
        //                 });
        //    return model.ToList();
        //}

        public List<string> ListName(string keyword)
        {
            return db.RealEstates.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        //public List<RealEstateViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{
        //    totalRecord = db.RealEstates.Where(x => x.Name.Contains(keyword)).Count();
        //    var model = (from a in db.RealEstates
        //                 join b in db.RealEstateCategories
        //                 on a.CatID equals b.CateID
        //                 where a.Name.Contains(keyword)
        //                 select new
        //                 {
        //                     CateMetaTile = b.MetaDescription,
        //                     CateName = b.Name,
        //                     CreateBy = a.CreateBy,
        //                     Description = a.Description,
        //                     CreateDate = a.CreateDate,
        //                     RealEstateID = a.RealEstateID,
        //                     Detail = a.Detail,
        //                     Price = a.Price,
        //                     Image = a.Image,
        //                     Name = a.Name,
        //                     MetaTile = a.MetaTile,
        //                     Address = a.Address,
        //                     Acreage = a.Acreage
        //                 }).AsEnumerable().Select(x => new RealEstateViewModel()
        //                 {
        //                     //CateMetaTitle = x.CateMetaTile,
        //                     CateName = x.Name,
        //                     CreateBy = x.CreateBy,
        //                     Description = x.Description,
        //                     CreateDate = x.CreateDate,
        //                     RealEstateID = x.RealEstateID,
        //                     Detail = x.Detail,
        //                     Price = x.Price,
        //                     Image = x.Image,
        //                     Name = x.Name,
        //                     MetaTile = x.MetaTile,
        //                     Address = x.Address,
        //                     Acreage = x.Acreage
        //                 });
        //    model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    return model.ToList();
        //}

        public List<RealEstate> ListHotNews(int top)
        {
            return db.RealEstates.Where(x => x.CreateDate != null && x.CreateDate > DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<RealEstate> ListMaps(int top)
        {
            return db.RealEstates.Where(x =>x.Status == false).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public bool changeStatus(long id)
        {
            var content = db.RealEstates.Find(id);

            content.Status = !content.Status;
            db.SaveChanges();
            return content.Status;
        }
    }
}
