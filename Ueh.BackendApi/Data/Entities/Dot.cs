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
        public string name { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public string status { get; set; }
        public ICollection<SinhvienDot> sinhviendots { get; set; }
        public ICollection<Phancong> phanCongs { get; set; }

    }
}
