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
            var Movies = MovieDatabase.All;

            Movies = MovieDatabase.All;
            // Search movie titles for the SearchTerms
            if(terms != null) {
                Movies = Movies.Where(movie =>
                    movie.Title != null &&
                    movie.Title.Contains(terms, StringComparison.InvariantCultureIgnoreCase));
            }

            return Movies;
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

            return movies.Where(movie =>
                movie.MPAARating != null && ratings.Contains(movie.MPAARating)
                );
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

            return movies.Where(movie =>
                movie.MajorGenre != null && genreList.Contains(movie.MajorGenre)
                );
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
            else if (min == null) min = 0;
            else if (max == null) max = Double.MaxValue;

            List<Movie> results = new List<Movie>();

            return movies.Where(movie =>
                movie.IMDBRating >= min && movie.IMDBRating <= max
                );
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
            else if (min == null) min = 0;
            else if (max == null) max = Double.MaxValue;

            return movies.Where(movie =>
                movie.RottenTomatoesRating >= min && movie.RottenTomatoesRating <= max
                );
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
