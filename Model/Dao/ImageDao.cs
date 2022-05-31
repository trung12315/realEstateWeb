using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ImageDao
    {
        bdsWebContext db = null;
        public ImageDao()
        {
            db = new bdsWebContext();
        }
        public List<Image> ListAll()
        {
            return db.Images.ToList();
        }
    }
}
