﻿using System;
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
        public string firstName { get; set; }
        public string lastName { get; set; }
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
        public string? macn { get; set; }
        public string status { get; set; }


        public Chuyennganh chuyennganh { get; set; }
        public ICollection<SinhvienDot> sinhviendots { get; set; }
        public ICollection<Phancong> phancongs { get; set; }
        public ICollection<SinhvienKhoa> sinhvienkhoas { get; set; }


    }

}
