using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tree_builder.XMLClasses
{
    public class molecule
    {
        public string name;
        public float rarity;

        public molecule(string name, float rarity)
        {
            this.name = name;
            this.rarity = rarity;
        }

        public molecule(): this("unvalid", 1)
        {

        }
    }
}
