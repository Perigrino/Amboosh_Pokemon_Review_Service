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
    
    public ICollection<Review> GetReviews(int? pageNumber)
    {
        var review = _context.Reviews.OrderBy(r => r.Id).ToList();
        
        //Paging
        int pageSize = 10;
        review = PaginatedList<Review>.Create(review.AsQueryable(), pageNumber ?? 1, pageSize);
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
}