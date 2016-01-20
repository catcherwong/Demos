using System.Collections.Generic;

namespace MovieDemo.Models
{
    public class MovieType
    {
        public MovieType()
        {
            Movies = new HashSet<Movie>();
        }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}