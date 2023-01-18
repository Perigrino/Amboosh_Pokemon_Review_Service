using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Dto;

public class ReviewDto
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    //Navigation Properties
    public Reviewer Reviewer { get; set; }
    public Pokemon Pokemon { get; set; }
}