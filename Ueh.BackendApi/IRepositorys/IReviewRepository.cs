using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(string reviewId);
        ICollection<Review> GetReviewsOfASinhvien(string mssv);
        bool ReviewExists(string reviewId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();
    }
}
