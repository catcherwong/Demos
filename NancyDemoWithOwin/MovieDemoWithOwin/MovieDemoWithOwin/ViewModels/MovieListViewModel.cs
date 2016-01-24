using System.Collections.Generic;

namespace MovieDemoWithOwin.ViewModels
{
    public class MovieListViewModel
    {
        public IEnumerable<MovieViewModel> Movies { get; set; }
    }
}