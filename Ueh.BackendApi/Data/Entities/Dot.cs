using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ueh.BackendApi.Data.Entities
{
    public class Dot
    {
        public string madot { get; set; }
        public string tendot { get; set; }
        public DateTime? ngaybatdau { get; set; }
        public DateTime? ngayketthuc { get; set; }
        public string? status { get; set; }
        public ICollection<Phancong> phanCongs { get; set; }

    }
}
