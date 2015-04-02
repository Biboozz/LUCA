using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tree_builder.XMLClasses;

namespace tree_builder
{
    public partial class skillCreationForm : Form
    {
        public Form1 parent;

        public skillCreationForm()
        {
            InitializeComponent();
            skill S = new skill();
            foreach (moleculePack mp in S.workCosts.cellMolecules)
            {
                listBox1.Items.Add(mp);
            }
            foreach (moleculePack mp in S.workCosts.environmentMolecules)
            {
                listBox1.Items.Add(mp);
            }
            foreach (moleculePack mp in S.workProducts.cellMolecules)
            {
                listBox1.Items.Add(mp);
            }
            foreach (moleculePack mp in S.workProducts.environmentMolecules)
            {
                listBox1.Items.Add(mp);
            }
            foreach (moleculePack mp in S.devCosts.cellMolecules)
            {
                listBox1.Items.Add(mp);
            }
            foreach (moleculePack mp in S.devCosts.environmentMolecules)
            {
                listBox1.Items.Add(mp);
            }
            foreach (moleculePack mp in S.devProducts.cellMolecules)
            {
                listBox1.Items.Add(mp);
            }
            foreach (moleculePack mp in S.devProducts.environmentMolecules)
            {
                listBox1.Items.Add(mp);
            }
        }

        

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
