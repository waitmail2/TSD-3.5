using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace TSD
{
    public partial class SelectCharacteristic : Form
    {
        public string tovar_code = "";

        public SelectCharacteristic()
        {
            InitializeComponent();
            this.Load += new EventHandler(SelectCharacteristic_Load);
            this.listView_characteristic.KeyDown += new KeyEventHandler(listView_characteristic_KeyDown);
            this.listView_characteristic.SelectedIndexChanged += new EventHandler(listView_characteristic_SelectedIndexChanged);
        }

        private void listView_characteristic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_characteristic.SelectedIndices.Count > 0)
            {
                label_current_characteristic.Text = listView_characteristic.Items[listView_characteristic.SelectedIndices[0]].Text;
            }
        }

        private void listView_characteristic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listView_characteristic.SelectedIndices.Count > 0)
                {
                    Program.CharacteristicGuid = listView_characteristic.Items[listView_characteristic.SelectedIndices[0]].Tag.ToString();
                }
                this.Close();
            }
        }

        private void SelectCharacteristic_Load(object sender, EventArgs e)
        {
            Program.CharacteristicGuid = "";
            // Set the view to show details.
            listView_characteristic.View = View.Details;

            //listView_inventory.AllowColumnReorder = false;

            // Select the item and subitems when selection is made.
            listView_characteristic.FullRowSelect = true;

            listView_characteristic.Columns.Add("Наименование", 200, HorizontalAlignment.Center);
            listView_characteristic.Columns.Add("Цена", 60, HorizontalAlignment.Left);

            SQLiteConnection conn = Program.ConnectForDataBase();
            try
            {
                conn.Open();
                string query = "SELECT  guid,characteristic.name AS characteristic_name ,retail_price_characteristic, " +
                    " tovar.name AS tovar_name "+
                    " FROM characteristic " +
                    " LEFT JOIN tovar ON characteristic.tovar_code = tovar.code "+
                    " WHERE tovar_code = " + tovar_code;
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem lvi = new ListViewItem(reader["characteristic_name"].ToString());
                    lvi.Tag = reader["guid"].ToString();
                    lvi.SubItems.Add(reader["retail_price_characteristic"].ToString());
                    listView_characteristic.Items.Add(lvi);
                    label_tovar.Text = reader["tovar_name"].ToString();
                }
                if (listView_characteristic.Items.Count > 0)
                {
                    //question.wav
                    PlaySound ps = new PlaySound();
                    ps.PlaySound_WAV("\\Windows\\question.wav");
                    ps.PlaySound_WAV("\\Windows\\question.wav");
                    listView_characteristic.Items[0].Selected = true;
                    listView_characteristic.Items[0].Focused = true;
                }
                reader.Close();
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            if (listView_characteristic.Items.Count == 0)
            {
                this.Close();
            }
            else
            {
                this.Visible = true;
            }
        }


    }
}