using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ueh.BackendApi.Data.Entities
{
    public class Khoa
    {
        public string makhoa { get; set; }
        public string tenkhoa { get; set; }
        public ICollection<Giangvien> giangviens { get; set; }
    }
}
