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
using tree_builder.XMLClasses;

namespace tree_builder
{
    public partial class Form1 : Form
    {
        public List<skill> skills;
        public List<molecule> molecules;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //main
            this.skills = new List<skill>();
            this.molecules = new List<molecule>();

        }

        private void SerializeSkills(string fileName)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<skill>));

                var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + fileName + ".xml";
                System.IO.FileStream file = System.IO.File.Create(path);

                writer.Serialize(file, skills);
                file.Close();
            }
            catch (Exception e)
            {
                throw new Exception("unable to serialize the skill list", e);
            }
        }

        private void SerializeMolecules(string fileName)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<molecule>));

                var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + fileName + ".xml";
                System.IO.FileStream file = System.IO.File.Create(path);

                writer.Serialize(file, molecules);
                file.Close();
            }
            catch (Exception e)
            {
                throw new Exception("unable to serialize the molecule list", e);
            }
        }

        private void createNew_Click(object sender, EventArgs e)
        {
            creationForm CF = new creationForm();
            CF.parent = this;
            CF.Show();
        }

        
        
    }
}
