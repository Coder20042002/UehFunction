using Microsoft.EntityFrameworkCore;
using ServiceStack.FluentValidation.Resources;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Loai>().HasData(
                new Loai() { maloai = "HKDN", tenloai = "Học kỳ doanh nghiệp" },
                new Loai() { maloai = "KL", tenloai = "Khoá luận tốt nghiệp" });

        }
    }
}
