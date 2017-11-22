using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace BizTalk.Adapter.AssemblyExecute.Interface
{
    public interface IAssemblyExecute
    {
        object GetInputParameter(XmlDocument xmldoc);
        Stream ExecuteResponse(Stream stream,object inputparameters);
    }
}
