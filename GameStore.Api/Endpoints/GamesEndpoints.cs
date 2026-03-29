using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";


    public static void MapGamesEnpoints(this WebApplication app)
    {

        var group = app.MapGroup("/games");

        // GET /games
        group.MapGet("/", (GameStoreContext dbContext) =>
        {
            return dbContext.Games.ToList();
        });



        // GET /games/{id}
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {

            var game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
            .WithName(GetGameEndpointName);


        // POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {

            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);

        });


        // PUT /games/{id}
        // group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        // {

        //     var game = dbContext.Games.Find(id);


        //     if (game == null)
        //     {
        //         return Results.NotFound();
        //     }

        //     var game = new GameDto(
        //         id,
        //         updatedGame.Name,
        //         updatedGame.Genre,
        //         updatedGame.Price,
        //         updatedGame.ReleaseDate
        //     );

        //     return Results.NoContent();

        // });

        // DELETE /games/{id}
        // group.MapDelete("/{id}", (int id) =>
        // {
        //     games.RemoveAll(game => game.Id == id);

        //     return Results.NoContent();
        // });

    }



}