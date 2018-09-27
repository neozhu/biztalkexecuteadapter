using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Schema;

namespace BizTalk.Adapter.DesignTime.XmlEditor
{
    public partial class XmlEditForm : Form
    {
        private string xmlString;
        public XmlEditForm(string xmlstring)
        {
            InitializeComponent();
            xmlString = xmlstring;
            LoadXml();
        }
        private void LoadXml() {
            if (!string.IsNullOrEmpty(this.xmlString) && IsValidXhtml(this.xmlString))
            {

                XmlDocument xmlDoc = new XmlDocument();
                StringWriter sw = new StringWriter();
                xmlDoc.LoadXml(this.xmlString);
                xmlDoc.Save(sw);
                this.xmlTextEditor.Text = sw.ToString();
            }
            else {
                this.xmlTextEditor.Text = "";
            }
        }
        private void XmlEditForm_Load(object sender, EventArgs e)
        {

        }

        public string GetXmlContent() {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.xmlTextEditor.Text);
            StringWriter sw = new StringWriter();
            // Save the document to a file and auto-indent the output.
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                
                writer.Formatting = Formatting.None;
                doc.Save(writer);
            }
            return sw.ToString();
        }

        private void createbutton_Click(object sender, EventArgs e)
        {
            string xmlstring = @"<AssemblyExecuteAdapter>
	<InParameters>
		<ParameterName1>val</ParameterName1>
		<ParameterName2>val</ParameterName2>
		<ParameterName3>val</ParameterName3>
	</InParameters>
</AssemblyExecuteAdapter>";

            this.xmlTextEditor.Text = xmlstring;

        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            if (IsValidXhtml(this.xmlTextEditor.Text))
            {

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else {
                MessageBox.Show("This is not a valid xml structure");
                this.xmlTextEditor.Focus();
               
            }
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        public   bool IsValidXhtml( string text)
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

            using (XmlReader xmlReader = XmlReader.Create(new StringReader(text), settings))
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


        
    }


    
}
