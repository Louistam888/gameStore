namespace GameStore.Api.Entities;

public class Genre
{
    public int Id { get; set; }

    public required string Name { get; set; } // required ensures this field cannot be null in every instance
}

