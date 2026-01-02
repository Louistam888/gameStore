using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
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

//GET games
app.MapGet("games", () => games);
//GET indivdual game
app.MapGet("games/{id}", (int id) =>
{
    //checking DTO, which is the part of the API that is disposed, if !null return game. We check by DTO for get since REST can return a bunch of other stuff that is sensitive.
    GameDto? game = games.Find(game => game.Id == id);

    //use is - good practice since == can get messed up if == is also used in a class
    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName); //withname assigns name to endpoint

//POST /games
app.MapPost("games", (CreateGameDto newGame) =>
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

app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
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
app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent(); //not creating a new route
});

app.Run();


//1:20