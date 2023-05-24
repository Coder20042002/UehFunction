using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Repositorys
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(string reviewerId);
        ICollection<Review> GetReviewsByReviewer(string reviewerId);
        bool ReviewerExists(string reviewerId);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}
