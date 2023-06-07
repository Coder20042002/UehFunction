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
        public string ngaysinh { get; set; }
        public string sdt { get; set; }
        public string HDT { get; set; }
        public string status { get; set; }

        public ICollection<Review> reviews { get; set; }
        public ICollection<SinhvienDot> sinhviendots { get; set; }
        public ICollection<PhanCong> phanCongs { get; set; }
        public ICollection<SinhvienKhoa> sinhvienkhoas { get; set; }
        public ICollection<SinhvienChuyenNganh> sinhvienchuyennganhs { get; set; }

    }

}
