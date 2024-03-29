﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ueh.BackendApi.Data.Entities
{
    public class Giangvien
    {
        public string magv { get; set; }
        public string tengv { get; set; }
        public string status { get; set; }
        public string? chuyenmon { get; set; }
        public string? makhoa { get; set; }

        public ICollection<Dangky> dangkys { get; set; }
        public ICollection<Phancong> phancongs { get; set; }
        public Khoa khoa { get; set; }


    }
}
