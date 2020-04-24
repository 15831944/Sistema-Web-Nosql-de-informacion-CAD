using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    using System;
    using System.Collections.Generic;

    public partial class Plane
    {
        public Plane()
        {
            Attributes = new Dictionary<string, string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
    }
}
