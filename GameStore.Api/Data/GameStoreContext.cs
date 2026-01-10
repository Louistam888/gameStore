using Microsoft.EntityFrameworkCore;
using GameStore.Api.Entities;

namespace GameStore.Api.Data;
//DBCONTEXT DEFINES INTERACTION WITH DATABASE

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Games>(); //default state set // Dbset is object used to query and save instances of type game

    public DbSet<Genre> Genres => Set<Genre>();
}

