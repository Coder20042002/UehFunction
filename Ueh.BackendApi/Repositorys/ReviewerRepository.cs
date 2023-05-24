using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Repositorys
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly UehDbContext _context;
        private readonly IMapper _mapper;

        public ReviewerRepository(UehDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(string reviewerId)
        {
            return _context.Reviewers.Where(r => r.id == reviewerId).Include(e => e.reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(string reviewerId)
        {
            return _context.Reviews.Where(r => r.reviewer.id == reviewerId).ToList();
        }

        public bool ReviewerExists(string reviewerId)
        {
            return _context.Reviewers.Any(r => r.id == reviewerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }
    }
}

