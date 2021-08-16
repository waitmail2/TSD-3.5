namespace TSD
{
    partial class SelectButtonBox
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
            this.btn_acceptance = new System.Windows.Forms.Button();
            this.btn_show_divergence = new System.Windows.Forms.Button();
            this.btn_cansel_box = new System.Windows.Forms.Button();
            this.btn_without_control = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_acceptance
            // 
            this.btn_acceptance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btn_acceptance.Location = new System.Drawing.Point(4, 19);
            this.btn_acceptance.Name = "btn_acceptance";
            this.btn_acceptance.Size = new System.Drawing.Size(146, 124);
            this.btn_acceptance.TabIndex = 0;
            this.btn_acceptance.Text = "(1) По товарам";
            this.btn_acceptance.Click += new System.EventHandler(this.btn_acceptance_Click);
            // 
            // btn_show_divergence
            // 
            this.btn_show_divergence.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btn_show_divergence.Location = new System.Drawing.Point(156, 19);
            this.btn_show_divergence.Name = "btn_show_divergence";
            this.btn_show_divergence.Size = new System.Drawing.Size(159, 124);
            this.btn_show_divergence.TabIndex = 1;
            this.btn_show_divergence.Text = "(2) Расхождения";
            this.btn_show_divergence.Click += new System.EventHandler(this.btn_show_divergence_Click);
            // 
            // btn_cansel_box
            // 
            this.btn_cansel_box.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btn_cansel_box.Location = new System.Drawing.Point(156, 149);
            this.btn_cansel_box.Name = "btn_cansel_box";
            this.btn_cansel_box.Size = new System.Drawing.Size(159, 124);
            this.btn_cansel_box.TabIndex = 3;
            this.btn_cansel_box.Text = "(7) Отменить";
            this.btn_cansel_box.Click += new System.EventHandler(this.btn_cansel_box_Click);
            // 
            // btn_without_control
            // 
            this.btn_without_control.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btn_without_control.Location = new System.Drawing.Point(4, 149);
            this.btn_without_control.Name = "btn_without_control";
            this.btn_without_control.Size = new System.Drawing.Size(146, 124);
            this.btn_without_control.TabIndex = 2;
            this.btn_without_control.Text = "(3) Без контроля";
            this.btn_without_control.Click += new System.EventHandler(this.btn_without_control_Click);
            // 
            // SelectButtonBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(318, 295);
            this.ControlBox = false;
            this.Controls.Add(this.btn_cansel_box);
            this.Controls.Add(this.btn_without_control);
            this.Controls.Add(this.btn_show_divergence);
            this.Controls.Add(this.btn_acceptance);
            this.Name = "SelectButtonBox";
            this.Text = "Выберите действие";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_acceptance;
        private System.Windows.Forms.Button btn_show_divergence;
        private System.Windows.Forms.Button btn_cansel_box;
        private System.Windows.Forms.Button btn_without_control;
    }
}