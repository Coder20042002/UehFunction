using AutoMapper;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Repositorys
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly UehDbContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(UehDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(string reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfASinhvien(string mssv)
        {
            return _context.Reviews.Where(r => r.sinhvien.mssv == mssv).ToList();
        }

        public bool ReviewExists(string reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
