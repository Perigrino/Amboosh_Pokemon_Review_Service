using System.ComponentModel.DataAnnotations;
using Amboosh_Library.Data.Paging;
using Amboosh_Pokemon_Review_Service.Data;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Repository;

public class ReviewRepo : IReviewRepo
{
    private readonly AppDbContext _context;

    public ReviewRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public ICollection<Review> GetReviews()
    {
        var review = _context.Reviews.OrderBy(r => r.Id).ToList();
        return review;
    }

    public Review Review(int reviewId)
    {
        var review = _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        return review;
    }

    public ICollection<Review> GetReviewOfPokemon(int pokemonId)
    {
        var review = _context.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();
        return review;
    }

    public bool ReviewExists(int reviewId)
    {
        var review = _context.Reviews.Any(r => r.Id == reviewId);
        return review;
    }

    public bool CreateReview(Review review)
    {
        _context.Add(review);
        return Save();
    }

    public bool UpdateReview(Review review)
    {
        _context.Update(review);
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

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}