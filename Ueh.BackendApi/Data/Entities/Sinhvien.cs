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
        public string? malop { get; set; }
        public string ho { get; set; }
        public string ten { get; set; }
        public string? ngaysinh { get; set; }
        public string maloai { get; set; }
        public string makhoa { get; set; }
        public string? macn { get; set; }
        public string madot { get; set; }

        public string status { get; set; }
        public Chuyennganh chuyennganh { get; set; }
        public Khoa khoa { get; set; }
        public ICollection<Phancong> phancongs { get; set; }


    }

}
