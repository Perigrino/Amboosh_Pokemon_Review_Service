using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Interfaces;

public interface IReviewRepo
{
    ICollection<Review> GetReviews();
    Review Review(int reviewId);
    ICollection<Review> GetReviewOfPokemon(int pokemonId);
    bool ReviewExists(int reviewId);
    bool CreateReview(Review review);
    bool UpdateReview(Review review);
    bool DeleteReview(Review review);
    bool DeleteReviews(List<Review> reviews);
    bool Save();

}