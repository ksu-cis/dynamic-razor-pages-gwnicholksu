using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Selected ratings to show
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// Selected Genres to show
        /// </summary>
        [BindProperty]
        public string[] Genres { get; set; }

        /// <summary>
        /// The current search terms
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
//        [BindProperty]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
//        [BindProperty]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The maximum acceptable rotten tomatoes score
        /// </summary>
//        [BindProperty]
        public double? RottenMax { get; set; }

        /// <summary>
        /// The minimum acceptable rotten tomatoes score
        /// </summary>
//        [BindProperty]
        public double? RottenMin { get; set; }

        /// <summary>
        /// The movies found using the given search terms
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        public void OnGet(double? IMDBMin, double? IMDBMax, double? RottenMax, double? RottenMin)
        {
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            this.RottenMax = RottenMax;
            this.RottenMin = RottenMin;

            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenRating(Movies, RottenMin, RottenMax);
        }
    }
}
