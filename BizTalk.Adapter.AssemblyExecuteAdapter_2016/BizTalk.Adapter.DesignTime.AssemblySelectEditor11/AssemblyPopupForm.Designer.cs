namespace BizTalk.Adapter.DesignTime.AssemblySelectEditor
{
    partial class AssemblyPopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblyPopupForm));
            this.assemblylabel = new System.Windows.Forms.Label();
            this.assemblyfullnametextbox = new System.Windows.Forms.TextBox();
            this.openbutton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.okbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // assemblylabel
            // 
            this.assemblylabel.AutoSize = true;
            this.assemblylabel.Location = new System.Drawing.Point(12, 10);
            this.assemblylabel.Name = "assemblylabel";
            this.assemblylabel.Size = new System.Drawing.Size(83, 12);
            this.assemblylabel.TabIndex = 0;
            this.assemblylabel.Text = "Assembly File";
            // 
            // assemblyfullnametextbox
            // 
            this.assemblyfullnametextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assemblyfullnametextbox.Location = new System.Drawing.Point(101, 6);
            this.assemblyfullnametextbox.Name = "assemblyfullnametextbox";
            this.assemblyfullnametextbox.Size = new System.Drawing.Size(386, 21);
            this.assemblyfullnametextbox.TabIndex = 1;
            // 
            // openbutton
            // 
            this.openbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openbutton.Location = new System.Drawing.Point(487, 6);
            this.openbutton.Name = "openbutton";
            this.openbutton.Size = new System.Drawing.Size(35, 21);
            this.openbutton.TabIndex = 2;
            this.openbutton.Text = "...";
            this.openbutton.UseVisualStyleBackColor = true;
            this.openbutton.Click += new System.EventHandler(this.openbutton_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 33);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(510, 124);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(447, 166);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 4;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // okbutton
            // 
            this.okbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okbutton.Location = new System.Drawing.Point(366, 166);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 5;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // AssemblyPopupForm
            // 
            this.AcceptButton = this.okbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(534, 201);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.openbutton);
            this.Controls.Add(this.assemblyfullnametextbox);
            this.Controls.Add(this.assemblylabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssemblyPopupForm";
            this.Text = "Select Schema Assembly";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label assemblylabel;
        private System.Windows.Forms.TextBox assemblyfullnametextbox;
        private System.Windows.Forms.Button openbutton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button okbutton;
    }
}