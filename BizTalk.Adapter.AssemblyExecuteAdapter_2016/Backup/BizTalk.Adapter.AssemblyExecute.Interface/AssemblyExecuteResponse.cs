using System;
using System.Collections.Generic;
using System.Text;
using BizTalk.Adapter.AssemblyExecute.Interface;
using System.Xml;
namespace BizTalk.Adapter.AssemblyExecute.Imp
{
    public class AssemblyExecuteResponse:IAssemblyExecute
    {
        
        #region IAssemblyExecute ≥…‘±

        public object GetInputParameter(System.Xml.XmlDocument xmldoc)
        {
            return null;
        }

        public System.IO.Stream ExecuteResponse(System.IO.Stream stream, object inputparameters)
        {
            return stream;
        }

        #endregion
    }
}
