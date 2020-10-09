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
    public partial class SelectProducts : Form
    {
        public SelectProducts()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.listView_tovar.KeyDown += new KeyEventHandler(listView_tovar_KeyDown);
            this.listView_tovar.SelectedIndexChanged += new EventHandler(listView_tovar_SelectedIndexChanged);
            Program.TovarCode = "";
        }

        private void listView_tovar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_tovar.SelectedIndices.Count > 0)
            {
                label_current_tovar.Text = listView_tovar.Items[listView_tovar.SelectedIndices[0]].Text + ";" + listView_tovar.Items[listView_tovar.SelectedIndices[0]].SubItems[1].Text;
            }
        }

        private void listView_tovar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listView_tovar.SelectedIndices.Count > 0)
                {
                    Program.TovarCode = listView_tovar.Items[listView_tovar.SelectedIndices[0]].Tag.ToString();
                }
                this.Close();
            }
        }
    }
}