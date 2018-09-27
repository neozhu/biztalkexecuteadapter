using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BizTalk.Adapter.DesignTime.AssemblySelectEditor
{
    public partial class AssemblyPopupForm : Form
    {
        private string assemlbyFullyQualifiedName = string.Empty;
        private string schemaFullyQualifiedName = string.Empty;
        public AssemblyPopupForm(string value)
        {
            InitializeComponent();
            this.okbutton.Enabled = false;
            schemaFullyQualifiedName = value;
            this.listBox1.Items.Clear();

            if (!string.IsNullOrEmpty(schemaFullyQualifiedName))
            {
                Type type = Type.GetType(schemaFullyQualifiedName);
                if (type != null)
                {

                    this.assemblyfullnametextbox.Text = type.Assembly.FullName;
                    assemlbyFullyQualifiedName = type.Assembly.FullName;
                    filllist(assemlbyFullyQualifiedName);
                }
                else {
                    schemaFullyQualifiedName = string.Empty;
                }
            }

           
            

        }


        private void filllist(string assemlbyFullyQualifiedName) {

            if (string.IsNullOrEmpty(assemlbyFullyQualifiedName)) return ;
            var _assembly = Assembly.Load(assemlbyFullyQualifiedName);
            var schemaList = (from type in _assembly.GetTypes()
                              where !type.IsAbstract && type.BaseType == typeof(Microsoft.XLANGs.BaseTypes.SchemaBase)
                              select type.AssemblyQualifiedName).ToList();
            this.listBox1.Items.Clear();
            foreach (var str in schemaList) {
                this.listBox1.Items.Add(str);
                if (str == this.schemaFullyQualifiedName)
                {
                    this.listBox1.SelectedItem = str;
                    this.okbutton.Enabled = true;
                }
            }
        }
        public string GetSelectSchemaTypeName() {
            if (this.listBox1.SelectedIndex >= 0)
            {
                string selectitem = this.listBox1.SelectedItem.ToString();
                return selectitem;
            }
            else {
                return string.Empty;
            }
            
            
        }
        private void openbutton_Click(object sender, EventArgs e)
        {
            var filename = string.Empty;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files (*.*)|*.*";
            dlg.CheckFileExists = true;
            using (dlg)
            {
                DialogResult res = dlg.ShowDialog();
                if (res == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    this.assemblyfullnametextbox.Text = Assembly.LoadFile(filename).FullName;
                    this.assemlbyFullyQualifiedName = this.assemblyfullnametextbox.Text;
                    this.filllist(this.assemlbyFullyQualifiedName);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
                this.okbutton.Enabled = true;
            else
                this.okbutton.Enabled = false;
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            if(this.listBox1.SelectedIndex>=0)
                this.DialogResult = DialogResult.OK;
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
