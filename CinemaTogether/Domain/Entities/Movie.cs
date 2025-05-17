using Domain.Common;

namespace Domain.Entities;

public class Movie : IBaseEntity
{
    public Guid Id { get; set; }
    public int TmdbId { get; set; }
    
    public string Title { get; set; }
    
    public int Duration { get; set; }
    
    public string Description { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

    public string PosterPath { get; set; }
    
    public string Director { get; set; }
    
    public string Actors { get; set; }
    
    public decimal Rating { get; set; }
    
    public int RatingCount { get; set; }
    
    public decimal RatingTmdb { get; set; }
    
    public List<MovieReview> MovieReviews { get; set; } = new List<MovieReview>();
}