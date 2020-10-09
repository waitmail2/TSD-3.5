namespace TSD
{
    partial class SelectCharacteristic
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
            this.listView_characteristic = new System.Windows.Forms.ListView();
            this.label_current_characteristic = new System.Windows.Forms.Label();
            this.label_tovar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView_characteristic
            // 
            this.listView_characteristic.Location = new System.Drawing.Point(5, 74);
            this.listView_characteristic.Name = "listView_characteristic";
            this.listView_characteristic.Size = new System.Drawing.Size(307, 136);
            this.listView_characteristic.TabIndex = 0;
            // 
            // label_current_characteristic
            // 
            this.label_current_characteristic.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label_current_characteristic.Location = new System.Drawing.Point(7, 216);
            this.label_current_characteristic.Name = "label_current_characteristic";
            this.label_current_characteristic.Size = new System.Drawing.Size(302, 72);
            // 
            // label_tovar
            // 
            this.label_tovar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label_tovar.Location = new System.Drawing.Point(5, 4);
            this.label_tovar.Name = "label_tovar";
            this.label_tovar.Size = new System.Drawing.Size(304, 67);
            // 
            // SelectCharacteristic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(318, 295);
            this.ControlBox = false;
            this.Controls.Add(this.label_tovar);
            this.Controls.Add(this.label_current_characteristic);
            this.Controls.Add(this.listView_characteristic);
            this.Name = "SelectCharacteristic";
            this.Text = "Выберите характеристику";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_characteristic;
        private System.Windows.Forms.Label label_current_characteristic;
        private System.Windows.Forms.Label label_tovar;
    }
}