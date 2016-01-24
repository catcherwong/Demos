using System;

namespace MovieDemoWithOwin.ViewModels
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public int MovieTypeId { get; set; }

        public string MovieTypeName { get; set; }

        public DateTime MovieAddTime { get; set; }
    }
}