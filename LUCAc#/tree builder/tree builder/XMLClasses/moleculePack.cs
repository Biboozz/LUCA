using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tree_builder.XMLClasses
{
    public class moleculePack
    {
        public molecule moleculeType;
        public int count;

        public moleculePack(int count, molecule moleculeType)
        {
            this.count = count;
            this.moleculeType = moleculeType;
        }

        public moleculePack() : this(1, new molecule())
        {

        }

        public override string ToString()
        {
            return count.ToString() + " - " + moleculeType.name;
        }
    }
}
