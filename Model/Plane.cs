using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    public class Plane
    {
        public Plane()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] FileContent { get; set; }
    }
}
