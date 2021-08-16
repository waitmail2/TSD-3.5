using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TSD
{
    public partial class SelectButtonBox : Form
    {        
        public int select_choise = 0;
        public string status_box = "";

        public SelectButtonBox()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Load += new EventHandler(SelectButtonBox_Load);
        }

        void SelectButtonBox_Load(object sender, EventArgs e)
        {
            if (status_box == "т")
            {
                btn_without_control.Enabled = false;
            }
            else if (status_box == "б")
            {
                btn_show_divergence.Enabled = false;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();                
            }
            else if (e.KeyCode == Keys.D1)
            {
                btn_acceptance_Click(null, null);
            }
            else if (e.KeyCode == Keys.D2)
            {
                if (status_box == "б")
                {
                    return;
                }
                btn_show_divergence_Click(null, null);
            }
            else if (e.KeyCode == Keys.D3)
            {
                if (status_box == "т")
                {
                    return;
                }
                btn_without_control_Click(null, null);
            }
            else if (e.KeyCode == Keys.D7)
            {
                btn_cansel_box_Click(null, null);
            }
        }

        private void btn_acceptance_Click(object sender, EventArgs e)
        {
            select_choise = 1;
            this.DialogResult = DialogResult.Yes;
            this.Close();          
        }

        private void btn_show_divergence_Click(object sender, EventArgs e)
        {           
            select_choise = 2;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btn_without_control_Click(object sender, EventArgs e)
        {
            select_choise = 3;
            this.DialogResult = DialogResult.Yes;
            this.Close();            
        }

        private void btn_cansel_box_Click(object sender, EventArgs e)
        {            
            DialogResult dr = MessageBox.Show("Уверены ?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.No)
            {
                select_choise = 7;
                this.DialogResult = DialogResult.Yes;
                this.Close();                
            }            
        }
    }
}