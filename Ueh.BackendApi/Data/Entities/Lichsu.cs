﻿namespace Ueh.BackendApi.Data.Entities
{
    public class Lichsu
    {
        public Guid Id { get; set; }
        public string noidung { get; set; }
        public DateTime ngay { get; set; }
        public Phancong phancong { get; set; }
    }
}