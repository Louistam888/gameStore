using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
    new (
        1,
        "Street FighterII",
        "Fighting",
        19.99M,
        new DateOnly(1992, 7, 15)
        ),
    new (
        2,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010, 9, 30)
        ),
    new (
        3,
        "FIFA 23",
        "Sports",
        69.99M,
        new DateOnly(2022, 9, 27)
        ),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        //GET games
        group.MapGet(".", () => games);
        //GET indivdual game
        group.MapGet("/{id}", (int id) =>
        {
            //checking DTO, which is the part of the API that is disposed, if !null return game. We check by DTO for get since REST can return a bunch of other stuff that is sensitive.
            GameDto? game = games.Find(game => game.Id == id);

            //use is - good practice since == can get messed up if == is also used in a class
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName); //withname assigns name to endpoint

        //POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game); //this creates the endpoint
        });

        //PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent(); //not creating a new route
        });

        //DELETE /games
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent(); //not creating a new route
        });
        return group;
    }
}