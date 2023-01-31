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
        var reviewer = _context.Reviewers.OrderBy(r => r.Id)
            .Include(e => e.Reviews)
            .ToList();
        return reviewer;
    }

    public Reviewer GetReviewer(int reviewerId)
    {
        var reviewer = _context.Reviewers
            .Where(r => r.Id == reviewerId)
            .Include(e => e.Reviews)
            .FirstOrDefault();
        return reviewer;
    }
    

    public ICollection<Review> GetReviewsByReviewer(int reviewerId)
    {
        var reviewer = _context.Reviews.Where(r => r.Reviewer.Id == reviewerId)
            .Include(e => e.Reviewer)
            .Include(p => p.Pokemon)
            .ToList();
        return reviewer;
    }

    public bool ReviewerExist(int reviewerId)
    {
        var reviewer = _context.Reviewers.Any(r => r.Id == reviewerId);
        return reviewer;
    }

    public bool CreateReviewer(Reviewer createReviewer)
    {
        _context.Add(createReviewer);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}