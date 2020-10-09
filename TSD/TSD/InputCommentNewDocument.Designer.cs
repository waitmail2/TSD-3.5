namespace TSD
{
    partial class InputCommentNewDocument
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
            this.txtb_description = new System.Windows.Forms.TextBox();
            this.btn_write = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtb_description
            // 
            this.txtb_description.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtb_description.Location = new System.Drawing.Point(4, 5);
            this.txtb_description.MaxLength = 200;
            this.txtb_description.Multiline = true;
            this.txtb_description.Name = "txtb_description";
            this.txtb_description.Size = new System.Drawing.Size(311, 235);
            this.txtb_description.TabIndex = 0;
            // 
            // btn_write
            // 
            this.btn_write.Location = new System.Drawing.Point(4, 246);
            this.btn_write.Name = "btn_write";
            this.btn_write.Size = new System.Drawing.Size(151, 45);
            this.btn_write.TabIndex = 1;
            this.btn_write.Text = "ЗАПИСАТЬ(CTRL)";
            this.btn_write.Click += new System.EventHandler(this.btn_write_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(161, 246);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(154, 45);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "ОТМЕНА(ESC)";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // InputCommentNewDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(318, 295);
            this.ControlBox = false;
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_write);
            this.Controls.Add(this.txtb_description);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputCommentNewDocument";
            this.Text = "Ввод описания нового документа";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_write;
        private System.Windows.Forms.Button btn_cancel;
        public System.Windows.Forms.TextBox txtb_description;
    }
}