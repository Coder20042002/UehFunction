namespace Ueh.BackendApi.Data.Entities
{
    public class Reviewer
    {
        public string id { get; set; }
        public string hoten { get; set; }
        public ICollection<Review> reviews { get; set; }
    }
}