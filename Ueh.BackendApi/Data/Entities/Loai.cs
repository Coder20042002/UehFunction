using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ueh.BackendApi.Data.Entities
{
    public class Loai
    {
        public string maloai { get; set; }
        public string name { get; set; }
        public ICollection<Dangky> dangkies { get; set; }
        public ICollection<Phancong> phanCongs { get; set; }

    }
}
