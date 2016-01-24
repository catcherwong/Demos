using System.Collections.Generic;

namespace MovieDemoWithOwin.ViewModels
{
    public class MovieTypeListViewModel
    {
        public IEnumerable<Models.MovieType> MovieTypes { get; set; }
    }
}