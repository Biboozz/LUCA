using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TreeBuilder
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //main
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //create new
            //test
            skill bullshit = new skill();
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(skill));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, bullshit);
            file.Close();
            //test
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //remove
        }

        private void perkSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //perk selection change
        }

        
    }
}
