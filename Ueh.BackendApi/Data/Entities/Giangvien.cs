using System;
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
        public string makhoa { get; set; }

        public Khoa khoa { get; set; }

        public ICollection<Dangky> dangkies { get; set; }

    }
}
