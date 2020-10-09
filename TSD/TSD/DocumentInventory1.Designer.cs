namespace TSD
{
    partial class DocumentInventory1
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
            this.label_decription_document = new System.Windows.Forms.Label();
            this.btn_start_continue = new System.Windows.Forms.Button();
            this.btn_show_divergence = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_complete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_decription_document
            // 
            this.label_decription_document.BackColor = System.Drawing.Color.Silver;
            this.label_decription_document.Location = new System.Drawing.Point(4, 4);
            this.label_decription_document.Name = "label_decription_document";
            this.label_decription_document.Size = new System.Drawing.Size(311, 78);
            this.label_decription_document.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_start_continue
            // 
            this.btn_start_continue.Location = new System.Drawing.Point(4, 101);
            this.btn_start_continue.Name = "btn_start_continue";
            this.btn_start_continue.Size = new System.Drawing.Size(311, 40);
            this.btn_start_continue.TabIndex = 1;
            this.btn_start_continue.Text = "(1) Начать/продолжить";
            this.btn_start_continue.Click += new System.EventHandler(this.btn_start_continue_Click);
            // 
            // btn_show_divergence
            // 
            this.btn_show_divergence.Location = new System.Drawing.Point(4, 148);
            this.btn_show_divergence.Name = "btn_show_divergence";
            this.btn_show_divergence.Size = new System.Drawing.Size(311, 40);
            this.btn_show_divergence.TabIndex = 3;
            this.btn_show_divergence.Text = "(2) Просмотр расхождений";
            this.btn_show_divergence.Click += new System.EventHandler(this.btn_show_divergence_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Enabled = false;
            this.btn_delete.Location = new System.Drawing.Point(4, 195);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(311, 40);
            this.btn_delete.TabIndex = 4;
            this.btn_delete.Text = "(8) Удалить";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_complete
            // 
            this.btn_complete.Location = new System.Drawing.Point(4, 242);
            this.btn_complete.Name = "btn_complete";
            this.btn_complete.Size = new System.Drawing.Size(311, 40);
            this.btn_complete.TabIndex = 6;
            this.btn_complete.Text = "(9) Завершить";
            this.btn_complete.Click += new System.EventHandler(this.btn_complete_Click);
            // 
            // DocumentInventory1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(318, 295);
            this.ControlBox = false;
            this.Controls.Add(this.btn_complete);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_show_divergence);
            this.Controls.Add(this.btn_start_continue);
            this.Controls.Add(this.label_decription_document);
            this.Name = "DocumentInventory1";
            this.Text = "Инвентаризация";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_decription_document;
        private System.Windows.Forms.Button btn_start_continue;
        private System.Windows.Forms.Button btn_show_divergence;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_complete;
    }
}