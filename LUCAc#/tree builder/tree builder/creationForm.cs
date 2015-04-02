using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tree_builder
{
    public partial class creationForm : Form
    {
        public Form1 parent;
        public creationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skillCreationForm SCF = new skillCreationForm();
            SCF.Show();
            SCF.parent = this.parent;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

    }
}
