namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int GenreId { get; set; }

    public Genre? Genre { get; set; } // this is type from Genre.cs - in the genre type, if used, this cannot be null (hencerequired) but here usage of the type is optional

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
