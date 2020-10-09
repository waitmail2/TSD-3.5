using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Runtime.InteropServices;

namespace TSD
{
    public partial class InputCommentNewDocument : Form
    {
        //[DllImport("coredll.dll")]
        //private static extern bool SipShowIM(int dwFlag);
        
        public InputCommentNewDocument()
        {
            InitializeComponent();
            this.KeyPreview = true;            
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                btn_write_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btn_cancel_Click(null , null); 
            }
        }

        private void btn_write_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Вы хотите записать новый документ ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                return;
            }

            if (txtb_description.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Комментарий должен быть заполнен у нового документа ");
                return;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }       
    }
}