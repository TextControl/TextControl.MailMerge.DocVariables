using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXTextControl;
using TXTextControl.DocumentServer.Fields;

namespace WindowsFormsApplication31
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TXTextControl.LoadSettings ls = new TXTextControl.LoadSettings();
            ls.ApplicationFieldFormat = TXTextControl.ApplicationFieldFormat.MSWord;

            textControl1.Load("template.docx", TXTextControl.StreamType.WordprocessingML, ls);

            // loop through all fields in all text parts
            foreach (IFormattedText textPart in textControl1.TextParts)
            {
                foreach (ApplicationField appField in textPart.ApplicationFields)
                {
                    // convert all "DocVariable" fields to "MergeFields"
                    if (appField.TypeName == "DOCVARIABLE")
                    {
                        MergeField newField = new MergeField();
                        newField.Name = appField.Parameters[0];

                        appField.Parameters = newField.ApplicationField.Parameters;
                        appField.TypeName = "MERGEFIELD";
                        appField.Text = newField.Name;
                    }
                }
            }

            address newAddress = new address("Peter", "Text Control, LLC");
            mailMerge1.MergeObjects(new List<address>() { newAddress });
        }
    }

    public class address
    {
        public string name { get; set; }
        public string company { get; set; }

        public address(string name, string company)
        {
            this.name = name;
            this.company = company;
        }
    }
}
