using System.Collections.Generic;

namespace MovieDemo.ViewModels
{
    public class MovieListViewModel
    {
        public IEnumerable<MovieViewModel> Movies { get; set; }
    }
}