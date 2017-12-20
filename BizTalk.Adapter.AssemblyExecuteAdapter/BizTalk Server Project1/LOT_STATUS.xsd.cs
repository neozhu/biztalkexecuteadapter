namespace BizTalk_Server_Project1 {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://CompalWH.LOTSTATUS",@"Records")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"Records"})]
    public sealed class LOT_STATUS : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" attributeFormDefault=""unqualified"" elementFormDefault=""qualified"" targetNamespace=""http://CompalWH.LOTSTATUS"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""Records"">
    <xs:complexType>
      <xs:sequence>
        <xs:element maxOccurs=""unbounded"" name=""row"">
          <xs:complexType>
            <xs:attribute name=""LOT_NO"" type=""xs:string"" use=""required"" />
            <xs:attribute name=""WERKS"" type=""xs:string"" use=""required"" />
            <xs:attribute name=""CUST_NO"" type=""xs:string"" use=""required"" />
            <xs:attribute name=""LINE"" type=""xs:string"" use=""required"" />
            <xs:attribute name=""STATUS"" type=""xs:string"" use=""required"" />
            <xs:attribute name=""WHSEID"" type=""xs:string"" use=""required"" />
            <xs:attribute name=""ORDERKEY"" type=""xs:unsignedShort"" use=""required"" />
            <xs:attribute name=""TABLENAME"" type=""xs:string"" use=""required"" />
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public LOT_STATUS() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "Records";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
    }
}
