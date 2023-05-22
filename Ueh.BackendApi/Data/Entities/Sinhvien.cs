using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ueh.BackendApi.Data.Entities
{
    public class Sinhvien
    {
        public string mssv { get; set; }
        public string email { get; set; }
        public string hoten { get; set; }
        public string tenlop { get; set; }
        public string khoa { get; set; }
        public string chuyennganh { get; set; }
        public string status { get; set; }
        public string sdt { get; set; }
        public string HDT { get; set; }

        public ICollection<Review> reviews { get; set; }
        public Dangky dangky { get; set; }
        public ICollection<SinhvienLoai> sinhvienloais { get; set; }
        public ICollection<SinhvienDot> sinhviendots { get; set; }
    }

}
