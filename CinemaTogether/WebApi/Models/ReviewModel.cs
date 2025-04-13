namespace WebApi.Models;

public record ReviewModel(
    Guid MovieId,
    decimal Rating,
    string Comment);