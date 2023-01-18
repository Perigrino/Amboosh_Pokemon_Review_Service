using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Interfaces;

public interface IReviewRepo
{
    ICollection<Review> GetReviews(int? pageNumber);
    Review Review(int reviewId);
    ICollection<Review> GetReviewOfPokemon(int pokemonId);
    bool ReviewExists(int reviewId);

}