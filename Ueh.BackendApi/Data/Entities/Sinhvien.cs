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
        public string ho { get; set; }
        public string ten { get; set; }
        public string thuoclop { get; set; }
        public string khoagoc { get; set; }
        public string khoahoc { get; set; }
        public string mahp { get; set; }
        public string malhp { get; set; }
        public string tenhp { get; set; }
        public string soct { get; set; }
        public string malop { get; set; }
        public string bacdt { get; set; }
        public string loaihinh { get; set; }
        public string makhoa { get; set; }
        public string? macn { get; set; }
        public string madot { get; set; }

        public string status { get; set; }
        public Chuyennganh chuyennganh { get; set; }
        public Khoa khoa { get; set; }
        public ICollection<Phancong> phancongs { get; set; }


    }

}
