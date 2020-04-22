using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = new List<Movie>();

        private static string[] genres;
        /// <summary>
        /// Get the movie genres represented in the database
        /// </summary>
        public static string[] Genres => genres;

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        static MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }

            HashSet<string> genreSet = new HashSet<string>();
            foreach(Movie movie in movies)
            {
                if(movie.MajorGenre != null)
                {
                    genreSet.Add(movie.MajorGenre);
                }
            }
            genres = genreSet.ToArray();
        }

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        public static IEnumerable<Movie> All { get { return movies; } }

        /// <summary>
        /// Gets all the movies specified by a search term
        /// </summary>
        /// <param name="terms">The search term</param>
        /// <returns>List of all movies specified by search term</returns>
        public static IEnumerable<Movie> Search(string terms)
        {
            List<Movie> results = new List<Movie>();

            if (terms == null) return All;
            foreach(Movie movie in All)
            {
                if(movie.Title != null && movie.Title.Contains(terms, StringComparison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        /// <summary>
        /// Filters the provided collection of movies
        /// </summary>
        /// <param name="movies">List of movies to filter</param>
        /// <param name="ratings">The ratings to include</param>
        /// <returns>Filtered list of movies</returns>
        public static IEnumerable<Movie> FilterByMPAARating(IEnumerable<Movie> movies, IEnumerable<string> ratings)
        {
            if (ratings == null || ratings.Count() == 0) return movies;
            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if(movie.MPAARating != null && ratings.Contains(movie.MPAARating))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        /// <summary>
        /// Filters the provided collection of movies
        /// </summary>
        /// <param name="movies">List of movies to filter</param>
        /// <param name="ratings">The genres to include</param>
        /// <returns>Filtered list of movies</returns>
        public static IEnumerable<Movie> FilterByGenre(IEnumerable<Movie> movies, IEnumerable<string> genreList)
        {
            if (genreList == null || genreList.Count() == 0) return movies;
            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if(movie.MajorGenre != null && genreList.Contains(movie.MajorGenre))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        /// <summary>
        /// Filter the movies by IMDB Rating
        /// </summary>
        /// <param name="movies">Movies to filter</param>
        /// <param name="min">Minimum rating</param>
        /// <param name="max">Maximum rating</param>
        /// <returns>Filtered collection of movies</returns>
        public static IEnumerable<Movie> FilterByIMDBRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;

            List<Movie> results = new List<Movie>();

            if(min == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.IMDBRating <= max) results.Add(movie);
                }
            }
            else if(max == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.IMDBRating >= min) results.Add(movie);
                }
            }
            else
            {
                foreach(Movie movie in movies)
                {
                    if (movie.IMDBRating >= min && movie.IMDBRating <= max) results.Add(movie);
                }
            }

            return results;
        }

        /// <summary>
        /// Filter the movies by Rotten Tomatoes Rating
        /// </summary>
        /// <param name="movies">Movies to filter</param>
        /// <param name="min">Minimum rating</param>
        /// <param name="max">Maximum rating</param>
        /// <returns>Filtered collection of movies</returns>
        public static IEnumerable<Movie> FilterByRottenRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;

            List<Movie> results = new List<Movie>();

            if(min == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating <= max) results.Add(movie);
                }
            }
            else if(max == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating >= min) results.Add(movie);
                }
            }
            else
            {
                foreach(Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating >= min && movie.RottenTomatoesRating <= max) results.Add(movie);
                }
            }

            return results;
        }



        /// <summary>
        /// Get a list of possible MPAA Ratings
        /// </summary>
        public static string[] MPAARatings
        {
            get => new string[]
            {
                "G",
                "PG",
                "PG-13",
                "R",
                "NC-17"
            };
        }

    }
}
