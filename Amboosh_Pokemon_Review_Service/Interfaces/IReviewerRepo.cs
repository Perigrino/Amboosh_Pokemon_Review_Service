using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Interfaces;

public interface IReviewerRepo
{
    ICollection<Reviewer> GetReviewers();
    Reviewer Reviewer(int reviewerId);
    ICollection<Review> GetReviewsByReviewer(int reviewerId);
    bool ReviewerExist(int reviewerId);
}