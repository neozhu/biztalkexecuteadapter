namespace BizTalk_Server_Project1 {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"BizTalk_Server_Project1.LOT_SHIPPING", typeof(global::BizTalk_Server_Project1.LOT_SHIPPING))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"BizTalk_Server_Project1.dataset", typeof(global::BizTalk_Server_Project1.dataset))]
    public sealed class Map1 : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0"" version=""1.0"" xmlns:s0=""http://CompalWH.LOTSHIPPING"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s0:Records"" />
  </xsl:template>
  <xsl:template match=""/s0:Records"">
    <NewDataSet>
      <xsl:for-each select=""s0:row"">
        <Table>
          <LOT_NO>
            <xsl:value-of select=""@LOT_NO"" />
          </LOT_NO>
          <WERKS>
            <xsl:value-of select=""@WERKS"" />
          </WERKS>
          <CUST_NO>
            <xsl:value-of select=""@CUST_NO"" />
          </CUST_NO>
          <LINE>
            <xsl:value-of select=""@MATNR"" />
          </LINE>
          <STATUS>
            <xsl:value-of select=""@GROUP_ID"" />
          </STATUS>
        </Table>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"BizTalk_Server_Project1.LOT_SHIPPING";
        
        private const string _strTrgSchemasList0 = @"BizTalk_Server_Project1.dataset";
        
        public override string XmlContent {
            get {
                return _strMap;
            }
        }
        
        public override string XsltArgumentListContent {
            get {
                return _strArgList;
            }
        }
        
        public override string[] SourceSchemas {
            get {
                string[] _SrcSchemas = new string [1];
                _SrcSchemas[0] = @"BizTalk_Server_Project1.LOT_SHIPPING";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"BizTalk_Server_Project1.dataset";
                return _TrgSchemas;
            }
        }
    }
}
