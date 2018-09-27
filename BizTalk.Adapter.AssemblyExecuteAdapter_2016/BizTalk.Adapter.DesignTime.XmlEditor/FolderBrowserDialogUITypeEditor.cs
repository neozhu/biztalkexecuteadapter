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

namespace BizTalk.Adapter.DesignTime.XmlEditor
{
    public class FolderBrowserDialogUITypeEditor : UITypeEditor
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



                string folderName = (string)value;

                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.SelectedPath = folderName;
                    DialogResult result = folderBrowserDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        folderName = folderBrowserDialog.SelectedPath;
                        
                    }
                }
                return folderName;

            }
            finally
            {
                editorService = null;
            }
        }
    }
}
