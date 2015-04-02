using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeBuilder
{
    [Serializable()]
    public class skill
    {
        public string name;
        public string description;

        public skill() : this("Unnamed", "this is a perk")
        {

        }

        public skill(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

    }
}
