using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tree_builder.XMLClasses
{
    public class macroMolecule : molecule
    {
        public molecule componant;
        public int[] nbComponantsRange;

        public macroMolecule(string name, float rarity, int[] nbComponantsRange, molecule componant) : base(name, rarity)
        {
            this.nbComponantsRange = nbComponantsRange;
            this.componant = componant;
        }

        public macroMolecule() : this("unnamed", 1f, new int[] { 1, 1 }, new molecule())
        {

        }
    }
}
