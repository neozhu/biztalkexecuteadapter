using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace BizTalk.Adapter.DesignTime.AssemblySelectEditor
{
    public class SchemaAssemblySelectTypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context == null || context.Instance == null)
                return base.GetEditStyle(context);
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var assemblyfullName = string.Empty;
            IWindowsFormsEditorService editorService;

            if (context == null || context.Instance == null || provider == null)
                return value;

            try
            {
                // get the editor service, just like in windows forms  
                editorService = (IWindowsFormsEditorService)
                   provider.GetService(typeof(IWindowsFormsEditorService));

              

                string schemaFullyQualifiedName = (string)value;
                

                using (var frm=new AssemblyPopupForm(schemaFullyQualifiedName))
                {
                    DialogResult res = frm.ShowDialog();
                    if (res == DialogResult.OK)
                    {

                        schemaFullyQualifiedName = frm.GetSelectSchemaTypeName();
                    }
                    
                }
                return schemaFullyQualifiedName;

            }
            finally
            {
                editorService = null;
            }
        }
    }
}
