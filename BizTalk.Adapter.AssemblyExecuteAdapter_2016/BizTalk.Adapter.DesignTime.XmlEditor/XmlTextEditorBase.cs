using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Greg.XmlEditor.Presentation.Views
{
    using ICSharpCode.TextEditor;
   
    using ICSharpCode.TextEditor.Document;
    using System;
    using System.Drawing;
    using System.Text;
    using ICSharpCode.TextEditor.Src.Document.FoldingStrategy;

    public class XmlTextEditorBase : TextEditorControlEx 
    {
        public XmlTextEditorBase()
        {
            base.SyntaxHighlighting = "XML";
           
            base.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
            base.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
            //base.Document.FormattingStrategy = new XmlFormattingStrategy();
            base.TextEditorProperties = InitializeProperties();
            base.Document.FoldingManager.UpdateFoldings(string.Empty, null);
            //base.TextArea.Refresh(base.TextArea.FoldMargin);
        }

        private  ITextEditorProperties InitializeProperties()
        {
           return new DefaultTextEditorProperties
            {
                Font = new Font("Courier new", 9f, FontStyle.Regular),
                IndentStyle = IndentStyle.Smart,
                ShowSpaces = false,
                LineTerminator = Environment.NewLine,
                ShowTabs = false,
                ShowInvalidLines = false,
                ShowEOLMarker = false,
                TabIndent = 2,
                CutCopyWholeLine = true,
                LineViewerStyle = LineViewerStyle.None,
                MouseWheelScrollDown = true,
                MouseWheelTextZoom = true,
                AllowCaretBeyondEOL = false,
                AutoInsertCurlyBracket = true,
                BracketMatchingStyle = BracketMatchingStyle.After,
                ConvertTabsToSpaces = false,
                ShowMatchingBracket = true,
                EnableFolding = true,
                ShowVerticalRuler = false,
                IsIconBarVisible = true,
                Encoding = Encoding.Unicode,
               
            };
        }
    }
}






