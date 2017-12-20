namespace BizTalk.Adapter.DesignTime.XmlEditor
{
    partial class XmlEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.createbutton = new System.Windows.Forms.Button();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.okbutton = new System.Windows.Forms.Button();
            this.xmlTextEditor = new Greg.XmlEditor.Presentation.Views.XmlTextEditorBase();
            this.SuspendLayout();
            // 
            // createbutton
            // 
            this.createbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.createbutton.Location = new System.Drawing.Point(275, 280);
            this.createbutton.Name = "createbutton";
            this.createbutton.Size = new System.Drawing.Size(75, 38);
            this.createbutton.TabIndex = 1;
            this.createbutton.Text = "New";
            this.createbutton.UseVisualStyleBackColor = true;
            this.createbutton.Click += new System.EventHandler(this.createbutton_Click);
            // 
            // cancelbutton
            // 
            this.cancelbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbutton.Location = new System.Drawing.Point(437, 280);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(75, 38);
            this.cancelbutton.TabIndex = 2;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // okbutton
            // 
            this.okbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okbutton.Location = new System.Drawing.Point(356, 280);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 38);
            this.okbutton.TabIndex = 3;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // xmlTextEditor
            // 
            this.xmlTextEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xmlTextEditor.FoldingStrategy = "XML";
            this.xmlTextEditor.Font = new System.Drawing.Font("Courier New", 9F);
            this.xmlTextEditor.IsIconBarVisible = true;
            this.xmlTextEditor.Location = new System.Drawing.Point(0, 0);
            this.xmlTextEditor.Name = "xmlTextEditor";
            this.xmlTextEditor.ShowVRuler = false;
            this.xmlTextEditor.Size = new System.Drawing.Size(512, 274);
            this.xmlTextEditor.SyntaxHighlighting = "XML";
            this.xmlTextEditor.TabIndent = 2;
            this.xmlTextEditor.TabIndex = 0;
            // 
            // XmlEditForm
            // 
            this.AcceptButton = this.okbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.cancelbutton;
            this.ClientSize = new System.Drawing.Size(512, 320);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.createbutton);
            this.Controls.Add(this.xmlTextEditor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XmlEditForm";
            this.Text = "Input Xml Parameters Editor";
            this.Load += new System.EventHandler(this.XmlEditForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Greg.XmlEditor.Presentation.Views.XmlTextEditorBase xmlTextEditor;
        private System.Windows.Forms.Button createbutton;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.Button okbutton;
    }
}