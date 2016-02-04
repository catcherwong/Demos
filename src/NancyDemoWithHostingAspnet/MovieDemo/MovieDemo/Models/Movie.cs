using System;

namespace MovieDemo.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public int MovieTypeId { get; set; }

        public DateTime MovieAddTime { get; set; }

        public virtual MovieType MovieType { get; set; }

        public Movie()
        {
            MovieAddTime = DateTime.Now;
        }
    }
}