namespace TSD
{
    partial class SelectProducts
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
            this.label_current_tovar = new System.Windows.Forms.Label();
            this.listView_tovar = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label_current_tovar
            // 
            this.label_current_tovar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label_current_tovar.Location = new System.Drawing.Point(8, 217);
            this.label_current_tovar.Name = "label_current_tovar";
            this.label_current_tovar.Size = new System.Drawing.Size(302, 72);
            // 
            // listView_tovar
            // 
            this.listView_tovar.Location = new System.Drawing.Point(6, 3);
            this.listView_tovar.Name = "listView_tovar";
            this.listView_tovar.Size = new System.Drawing.Size(307, 208);
            this.listView_tovar.TabIndex = 2;
            // 
            // SelectProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(318, 295);
            this.ControlBox = false;
            this.Controls.Add(this.label_current_tovar);
            this.Controls.Add(this.listView_tovar);
            this.Name = "SelectProducts";
            this.Text = "Выберите номенклатуру";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_current_tovar;
        public System.Windows.Forms.ListView listView_tovar;
    }
}