using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
        {
            if (context.Movie.Any())
            {
                return;
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-02-12"),
                    Genre = "Romantic Comedy",
                    Rating = "R",
                    Price = 7.99M
                },
                new Movie
                {
                    Title = "Ghostbusters",
                    ReleaseDate = DateTime.Parse("1984-03-13"),
                    Genre = "Comedy",
                    Rating = "PG",
                    Price = 8.99M
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-02-23"),
                    Genre = "Comedy",
                    Rating = "PG",
                    Price = 9.99M
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-04-15"),
                    Genre = "Western",
                    Rating = "PG",
                    Price = 3.99M
                },
                new Movie
                {
                    Title = "Harry Potter",
                    ReleaseDate = DateTime.Parse("2001-11-16"),
                    Genre = "Fantasy",
                    Rating = "PG",
                    Price = 7.99M
                },
                new Movie
                {
                    Title = "The Fast and the Furious",
                    ReleaseDate = DateTime.Parse("2001-06-22"),
                    Genre = "Action",
                    Rating = "PG-13",
                    Price = 8.99M
                },
                new Movie
                {
                    Title = "Batman Arkham Knight",
                    ReleaseDate = DateTime.Parse("2015-06-23"),
                    Genre = "Action",
                    Rating = "M",
                    Price = 9.99M
                }
            );
            context.SaveChanges();
        }
    }
}
