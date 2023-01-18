using Amboosh_Pokemon_Review_Service.Data;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Amboosh_Pokemon_Review_Service.Repository;

public class ReviewerRepo : IReviewerRepo
{
    private readonly AppDbContext _context;
    public ReviewerRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public ICollection<Reviewer> GetReviewers()
    {
        var reviewer = _context.Reviewers.OrderBy(r => r.ID).ToList();
        return reviewer;
    }

    public Reviewer Reviewer(int reviewerId)
    {
        var reviewer = _context.Reviewers
            .Where(r => r.ID == reviewerId)
            .Include(e => e.Reviews)
            .FirstOrDefault();
        return reviewer;
    }
    

    public ICollection<Review> GetReviewsByReviewer(int reviewerId)
    {
        var reviewer = _context.Reviews.Where(r => r.Reviewer.ID == reviewerId).ToList();
        return reviewer;
    }

    public bool ReviewerExist(int reviewerId)
    {
        var reviewer = _context.Reviewers.Any(r => r.ID == reviewerId);
        return reviewer;
    }
}