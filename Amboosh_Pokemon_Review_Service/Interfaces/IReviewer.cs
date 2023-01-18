using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Interfaces;

public interface IReviewer
{
    ICollection<Reviewer> GetReviewers();
    Reviewer Reviewer(int reviewId);
    ICollection<Reviewer> GetReviewsByReviewer();
    bool ReviewerExist(int reviewId);
}