using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{

    public class RepostDao
    {
        bdsWebContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public RepostDao()
        {
            db = new bdsWebContext();
        }
        public bool Dangtin(string baocao , int userid, int id)
        {
            Report repost = new Report();
            DateTime dt = DateTime.Now;
            String.Format("{0:dd/MM/yyyy}", dt);

            
            repost.UserID = userid;
            repost.CreateDate = dt;
            repost.Contents = baocao;
            repost.RealEstateID = id;

            db.Reports.Add(repost);
            db.SaveChanges();
            return true;
        }
        public IEnumerable<Report> ListAllPaging(string searchString, int page = 5, int pageSize = 12)
        {
            IQueryable<Report> model = db.Reports;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Contents.Contains(searchString) && x.Status == true );
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public bool Delete(int id)
        {
            try
            {
                var content = db.Reports.Find(id);
                db.Reports.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool changeStatus(long id)
        {
            var content = db.RealEstates.Find(id);

            content.Status = !content.Status;
            db.SaveChanges();
            var kd = db.Reports.Where(x => x.RealEstateID == id).ToList();
            foreach(var a in kd)
            {
                a.Status = !a.Status;
                db.SaveChanges();
                
            }
          
            return true;
        }
    }
}