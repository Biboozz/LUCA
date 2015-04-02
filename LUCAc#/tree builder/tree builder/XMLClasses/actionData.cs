using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tree_builder.XMLClasses
{
    public class actionData
    {
        public List<moleculePack> cellMolecules;
        public List<moleculePack> environmentMolecules;
        int ATP;

        public actionData(int ATP, List<moleculePack> cellMolecules, List<moleculePack> environmentMolecules)
        {
            this.ATP = ATP;
            this.cellMolecules = cellMolecules;
            this.environmentMolecules = environmentMolecules;
        }

        public actionData() : this(0, new List<moleculePack>(), new List<moleculePack>())
        {

        }
    }
}
