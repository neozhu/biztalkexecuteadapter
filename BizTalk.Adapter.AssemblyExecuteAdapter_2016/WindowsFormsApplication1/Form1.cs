using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizTalk.Adapter.DesignTime.AssemblySelectEditor;
using System.Xml;
using System.IO;
using System.Xml.Schema;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string schemaFullyQualifiedName = "";
            using (var frm = new AssemblyPopupForm(schemaFullyQualifiedName))
            {
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {

                    schemaFullyQualifiedName = frm.GetSelectSchemaTypeName();
                }

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            

            XmlDocument xmlDoc = new XmlDocument();
            StringWriter sw = new StringWriter();
            xmlDoc.Load("d:\\Store.xml");
            xmlDoc.Save(sw);
            String formattedXml = sw.ToString();

            this.textEditorControlEx1.Text = sw.ToString();
        }
        public static bool IsValidXml(string xml)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                CheckCharacters = true,
                ConformanceLevel = ConformanceLevel.Document,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true,
                ValidationFlags = XmlSchemaValidationFlags.None,
                ValidationType = ValidationType.None,
            };
            bool isValid;

            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml), settings))
            {
                try
                {
                    while (xmlReader.Read())
                    {
                        ; // This space intentionally left blank
                    }
                    isValid = true;
                }
                catch (XmlException)
                {
                    isValid = false;
                }
            }
            return isValid;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (IsValidXml(this.textEditorControlEx1.Text))
            {

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(this.textEditorControlEx1.Text);
                StringWriter sw = new StringWriter();
                // Save the document to a file and auto-indent the output.
                using (XmlTextWriter writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = Formatting.None;
                    doc.Save(writer);
                }
                MessageBox.Show(sw.ToString());
            }
        }
    }
}
