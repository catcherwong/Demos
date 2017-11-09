using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenusSolution.Models
{
    public class Menu
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string Content { get; set; }
        public string IconClass { get; set; }
        public string Href { get; set; }
        public long Order { get; set; }
    }

    public class MenuViewModel
    {
        public string ID { get; set; }
        public string Content { get; set; }
        public string IconClass { get; set; }
        public string Href { get; set; }
        public IList<MenuViewModel> Children { get; set; }
    }

}
