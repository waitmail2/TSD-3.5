using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TSD
{
    public partial class DocumentList : Form
    {

        public string guid = "";
        public bool its_new = false;
        public string type="";
        public string label_decription_document = ""; 
        private int index_position=0;
        private DateTime datetime_select_position = DateTime.Now;
        //private bool listView_stroki_change_pos = false;
        private int display_quantity = 0;
        private bool show_all_stroki = true;
        //string comment = "";

        public DocumentList()
        {
            InitializeComponent();
            this.Load += new EventHandler(Document_Load);
            this.txtB_inputbarcode.KeyPress += new KeyPressEventHandler(txtB_inputbarcode_KeyPress);
            this.KeyPreview = true;
            this.txtB_status.KeyDown += new KeyEventHandler(txtB_status_KeyDown);
            this.listView_stroki.SelectedIndexChanged += new EventHandler(listView_stroki_SelectedIndexChanged);
            this.listView_stroki.KeyDown += new KeyEventHandler(listView_stroki_KeyDown); 
            
        }

        /// <summary>
        /// kolstr количество позиций на которое необходимо прокрутить экран
        /// direction направление true вперед иначе назад
        /// </summary>
        /// <param name="kolstr"></param>
        /// <param name="direction"></param>
        private void listView_stroki_repaint(int kolstr, bool direction)
        {
            int position = listView_stroki.SelectedIndices[0];
            int count    = listView_stroki.Items.Count;
            int index    = 0;

            //получить возможное количество позиций на которое можем переместится
            if (direction)
            {
                if (position + kolstr < count)
                {
                    index = position + kolstr;
                }
                else
                {
                    index = count - 1;
                }
            }
            else
            {
                if (position - kolstr >0)
                {
                    index = position - kolstr;
                }
                else
                {
                    index = 0;
                } 
            }

            listView_stroki.Focus();
            listView_stroki.Items[index].Focused = true;            
            listView_stroki.Items[index].Selected = true;            
            listView_stroki.EnsureVisible(index);
            
            index_position = index;
            datetime_select_position = DateTime.Now;
             
        }

        private string get_characteristic_guid(string tovar_code,string characteristic_name)
        {
            string result = "";
            SQLiteConnection conn = Program.ConnectForDataBase();
            try
            {
                conn.Open();
                string query = "SELECT guid FROM characteristic WHERE tovar_code=" + tovar_code + 
                    " AND name='" + characteristic_name.Trim() + "'";
                SQLiteCommand command = new SQLiteCommand(query, conn);
                object result_query = command.ExecuteScalar();
                if (result_query != null)
                {
                    result = result_query.ToString();
                }
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
            return result;
        }

        /// <summary>
        /// При поиске строки в листвью в режиме показа
        /// расхождений, если строки не найдены 
        /// необходимо поискать в табличной части документа
        /// и если там есть такие строки то добавить в 
        /// листвью и спозиционироваться на этой строке
        /// 
        /// </summary>
        /// <param name="barcode"></param>
        private int add_string_listview(string barcode)
        {
            int result = 0;
            SQLiteConnection conn = Program.ConnectForDataBase();
            try
            {
                conn.Open();
                string query = "";
                if (barcode.Length < 7)
                {
                    query = "SELECT code FROM tovar where code=@barcode ";
                }
                else
                {
                    query = "SELECT barcodes.tovar_code FROM barcodes where barcodes.barcode_code=@barcode ";
                }
                SQLiteParameter _barcode = new SQLiteParameter("barcode", barcode);
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Parameters.Add(_barcode);
                object result_query = command.ExecuteScalar();
                if (result_query != null)//такой товар найден
                {
                    query = "SELECT dt.tovar_code, dt.quantity,dt.quantity_shop,dt.price_buy,dt.price," +
                    " tovar.name AS tovar_name,characteristic.guid AS characteristic_guid," +
                    " characteristic.name AS characteristic_name,dt.line_number FROM dt " +
                    " LEFT JOIN tovar ON dt.tovar_code = tovar.code " +
                    " LEFT JOIN characteristic ON dt.characteristic = characteristic.guid " +
                    " where dt.guid=@guid AND dt.tovar_code=@tovar_code order by dt.line_number";                    
                    command = new SQLiteCommand(query, conn);
                    SQLiteParameter _guid = new SQLiteParameter("guid", guid);
                    SQLiteParameter _tovar_code = new SQLiteParameter("tovar_code", result_query.ToString());                    
                    command.Parameters.Add(_guid);
                    command.Parameters.Add(_tovar_code);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result = 1;
                        ListViewItem item = new ListViewItem(reader["line_number"].ToString());                        
                        item.SubItems.Add(reader["quantity_shop"].ToString());
                        if (display_quantity == 1)
                        {
                            item.SubItems.Add(reader["quantity"].ToString());
                        }
                        item.SubItems.Add(reader["tovar_name"].ToString());
                        item.SubItems.Add(reader["characteristic_name"].ToString());                        
                        item.SubItems.Add(reader["price_buy"].ToString());                        
                        item.Tag = reader["tovar_code"].ToString();
                        listView_stroki.Items.Add(item); 
                    }
                    //спозиционируемся на последней строке
                    listView_stroki.Items[listView_stroki.Items.Count-1].Selected = true;
                    listView_stroki.Items[listView_stroki.Items.Count - 1].Focused = true;
                    listView_stroki.EnsureVisible(listView_stroki.Items.Count - 1);
                }
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

            return result;

        }

        private string get_barcode(string tovar_code)
        {
            string result = "";
            SQLiteConnection conn = Program.ConnectForDataBase();
            try
            {
                conn.Open();
                string query = "SELECT barcode FROM barcodes WHERE tovar_code=" + tovar_code;
                SQLiteCommand command = new SQLiteCommand(query, conn);
                object result_query = command.ExecuteScalar();
                if (result_query != null)
                {
                    result = result_query.ToString();
                }
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
            return result;
        }

        private void listView_stroki_KeyDown(object sender, KeyEventArgs e)
        {
            if (listView_stroki.SelectedIndices.Count > 0)
            {

                if (e.KeyCode == Keys.Enter)
                {
                    //return;//пока что отбой 
                    if (type != "4")
                    {
                        WorkWithBarcode wb = new WorkWithBarcode();
                        ListViewItem lvi = listView_stroki.Items[listView_stroki.SelectedIndices[0]];
                        string tovar_code = lvi.Tag.ToString();
                        //find_barcode_or_code_in_tovar(tovar_code);//Непонятно зачем это здесь 
                        wb.txtB_tovar.Text = tovar_code + "," + listView_stroki.Items[lvi.Index].SubItems[3].Text + ";" +
                        listView_stroki.Items[lvi.Index].SubItems[4].Text;
                        //wb.label_decription_document.Text = this.label_decription_document;
                        wb.guid = guid;
                        //wb.txtB_input_barcode.Text = get_barcode(tovar_code);
                        wb.tovar_code = tovar_code;
                        //Получить guid характеристики
                        string characteristic_name = listView_stroki.Items[lvi.Index].SubItems[2].Text.Trim();
                        if (characteristic_name.Length != 0)
                        {
                            wb.characteristic_guid = get_characteristic_guid(tovar_code, characteristic_name);
                        }
                        wb.txtB_quantity.Text = "1";
                        wb.open_from_document_list = true;

                        if (display_quantity == 1)
                        {
                            wb.label_количество_в_1с.Text = listView_stroki.Items[lvi.Index].SubItems[2].Text.Trim();
                        }
                        wb.label_количество_в_магазине.Text = " в документе " + listView_stroki.Items[lvi.Index].SubItems[1].Text.Trim();
                        string num_str = lvi.SubItems[0].Text;
                        //wb.display_quantity = display_quantity;
                        wb.close_this_form = true;
                        wb.ShowDialog();
                        //переррисовать список и спозиционироваться на старой строке 
                        load_stroki();
                        //
                        int index = 0;
                        foreach (ListViewItem item in listView_stroki.Items)
                        {
                            //if (item.Tag.ToString() == tovar_code.ToString())
                            //{
                            //    index = item.Index;
                            //    break;
                            //}
                            if (item.SubItems[0].Text.Trim() == num_str.Trim())
                            {
                                index = item.Index;
                                break;
                            }
                        }
                        //int index = listView_stroki.Items.IndexOf(lvi);
                        listView_stroki.Items[index].Selected = true;
                        listView_stroki.Items[index].Focused = true;
                        listView_stroki.EnsureVisible(index);
                    }
                    else
                    {
                        CheckAndPrintPrices chp = new CheckAndPrintPrices();
                        ListViewItem lvi = listView_stroki.Items[listView_stroki.SelectedIndices[0]];
                        string tovar_code = lvi.Tag.ToString();
                        //find_barcode_or_code_in_tovar(tovar_code);//Непонятно зачем это здесь 
                        chp.txtB_tovar.Text = tovar_code + "," + listView_stroki.Items[lvi.Index].SubItems[3].Text + ";" +
                        listView_stroki.Items[lvi.Index].SubItems[4].Text;
                        //wb.label_decription_document.Text = this.label_decription_document;
                        chp.guid = guid;
                        //wb.txtB_input_barcode.Text = get_barcode(tovar_code);
                        chp.tovar_code = tovar_code;
                        //Получить guid характеристики
                        string characteristic_name = listView_stroki.Items[lvi.Index].SubItems[2].Text.Trim();
                        if (characteristic_name.Length != 0)
                        {
                            chp.characteristic_guid = get_characteristic_guid(tovar_code, characteristic_name);
                        }
                        chp.txtB_quantity.Text = "1";
                        //chp.txtB_quantity.SelectedText = true;
                        //chp.open_from_document_list = true;

                        //if (display_quantity == 1)
                        //{
                        //    chp.label_количество_в_1с.Text = listView_stroki.Items[lvi.Index].SubItems[2].Text.Trim();
                        
                        //}
                        //wb.label_количество_в_магазине.Text = " в документе " + listView_stroki.Items[lvi.Index].SubItems[1].Text.Trim();
                        chp.txtB_tovar.Text = tovar_code + "," + characteristic_name;//listView_stroki.Items[lvi.Index].SubItems[1].Text.Trim(); 
                        string num_str = lvi.SubItems[0].Text;
                        //wb.display_quantity = display_quantity;
                        chp.close_this_form = true;
                        chp.txtB_quantity.Focus();
                        chp.ShowDialog();
                        //переррисовать список и спозиционироваться на старой строке 
                        load_stroki();
                        //
                        int index = 0;
                        foreach (ListViewItem item in listView_stroki.Items)
                        {
                            //if (item.Tag.ToString() == tovar_code.ToString())
                            //{
                            //    index = item.Index;
                            //    break;
                            //}
                            if (item.SubItems[0].Text.Trim() == num_str.Trim())
                            {
                                index = item.Index;
                                break;
                            }
                        }
                        //int index = listView_stroki.Items.IndexOf(lvi);
                        listView_stroki.Items[index].Selected = true;
                        listView_stroki.Items[index].Focused = true;
                        listView_stroki.EnsureVisible(index);
 
                    }                    
                }
                else if (e.KeyCode == Keys.D1)
                {
                    listView_stroki_repaint(5, false);
                }
                else if (e.KeyCode == Keys.D2)
                {
                    listView_stroki_repaint(50, false);
                }
                else if (e.KeyCode == Keys.D3)
                {
                    listView_stroki_repaint(250, false);
                }
                else if (e.KeyCode == Keys.D7)
                {
                    listView_stroki_repaint(5, true);
                }
                else if (e.KeyCode == Keys.D8)
                {
                    listView_stroki_repaint(50, true);
                }
                else if (e.KeyCode == Keys.D9)
                {
                    listView_stroki_repaint(250, true);
                }
            }
        }


        /// <summary>
        ///  при смене строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_stroki_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_stroki.SelectedIndices.Count > 0)
            {
                if (listView_stroki.SelectedIndices[0] != index_position)
                {
                    if ((DateTime.Now - datetime_select_position).Seconds < 2)
                    {

                        listView_stroki.Focus();
                        listView_stroki.Items[index_position].Focused = true;
                        listView_stroki.Items[index_position].Selected = true;
                        listView_stroki.EnsureVisible(index_position);
                    }
                }

                label_selected_quantity_shop.Text = listView_stroki.Items[listView_stroki.SelectedIndices[0]].SubItems[1].Text;
                if (display_quantity == 1)
                {
                    label_selected_quantity_1c.Text = " из " + listView_stroki.Items[listView_stroki.SelectedIndices[0]].SubItems[2].Text;
                    label_price.Text = " Цена " + listView_stroki.Items[listView_stroki.SelectedIndices[0]].SubItems[5].Text;
                }
                else
                {
                    label_price.Text = " Цена " + listView_stroki.Items[listView_stroki.SelectedIndices[0]].SubItems[4].Text;
                }
                               
                //+ " из "+
                //listView_stroki.Items[listView_stroki.SelectedIndices[0]].SubItems[4].Text;

                label_description_tovar.Text = listView_stroki.Items[listView_stroki.SelectedIndices[0]].Tag.ToString() + "," +
                    listView_stroki.Items[listView_stroki.SelectedIndices[0]].SubItems[3].Text + ";" +
                     listView_stroki.Items[listView_stroki.SelectedIndices[0]].SubItems[4].Text;
                //показать 5 последних знаков штрихкода 
                //if (!show_all_stroki)
                //{
                    txtB_info_1s.Text = get_all_barcodes(listView_stroki.Items[listView_stroki.SelectedIndices[0]].Tag.ToString());
                //}
                //else
                //{
                //    txtB_info_1s.Text = comment;
                //}
            }
        }

        private string get_all_barcodes(string tovar_code)        
        {
            string result = "";
            SQLiteConnection conn = Program.ConnectForDataBase();
            try
            {
                conn.Open();
                string query = "SELECT barcode_code FROM barcodes WHERE tovar_code="+tovar_code;
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string local_barcode = reader["barcode_code"].ToString().Trim();
                    if(local_barcode.Length>5)
                    {
                        local_barcode = local_barcode.Substring(local_barcode.Length-5,5);
                    }
                    result += local_barcode + " ";
                }
                reader.Close();
                command.Dispose();
                conn.Close();
            }
            catch
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            
            return result;
        }

        private void txtB_status_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.F)
            {
                txtB_inputbarcode.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            //else if (e.KeyCode == Keys.F15)
            else if ((e.KeyCode == Keys.F15)||(e.KeyCode == Keys.F14))
            {
                if (display_quantity == 0)
                {
                    return;
                }
                if (show_all_stroki)
                {
                    show_all_stroki = false;
                }
                else
                {
                    show_all_stroki = true;
                }
                load_stroki();
            }
        }


        private void txtB_inputbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                if (this.txtB_inputbarcode.Text.Length == 0)//||(this.inputbarcode.Text=="\r\n"))//тут еще проверка на минимальность символов
                {
                    MessageBox.Show("Штрихкод не найден");
                    return;
                }
                find_barcode_or_code_in_tovar(this.txtB_inputbarcode.Text.Trim());
                // this.listView1.Items.Count

                txtB_inputbarcode.Text = "";
                return;
            }

            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {

                    e.Handled = true;
                }
            }
        }

        private void find_barcode_or_code_in_tovar(string barcode)
        {
            int listView_stroki_index=0;

            if (listView_stroki.SelectedIndices.Count > 0)
            {
                listView_stroki_index = listView_stroki.SelectedIndices[0];
            }            

            SQLiteConnection conn = Program.ConnectForDataBase();
            bool find = false;
            try
            {
                conn.Open();
                string query = "";
                if (barcode.Length < 7)
                {
                    query = "SELECT code FROM tovar where code=@barcode";
                }
                else
                {
                    query = "SELECT barcodes.tovar_code FROM barcodes where barcodes.barcode_code=@barcode";
                }
                SQLiteParameter _barcode = new SQLiteParameter("barcode", barcode);
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Parameters.Add(_barcode);
                object result_query = command.ExecuteScalar();
                conn.Close();
                if (result_query != null)
                {
                    string tovar_code = result_query.ToString();
                   
                    foreach (ListViewItem lvi in listView_stroki.Items)
                    {
                        if (lvi.Tag.ToString().Trim() == tovar_code)
                        {
                            listView_stroki.Focus();
                            listView_stroki.Items[lvi.Index].Selected = true;
                            listView_stroki.EnsureVisible(lvi.Index);
                            //string kolm = listView_stroki.Items[lvi.Index].SubItems[4].Text.Trim();
                            //if (kolm == "")
                            //{
                            //    listView_stroki.Items[lvi.Index].SubItems[4].Text = "1";
                            //}
                            //else
                            //{
                            //    listView_stroki.Items[lvi.Index].SubItems[4].Text = (Convert.ToInt32(kolm) + 1).ToString();
                            //}
                            //Открываем форму для редактирвоания
                            //WorkWithBarcode wb = new WorkWithBarcode();                            
                            //find_barcode_or_code_in_tovar(tovar_code);
                            //wb.txtB_tovar.Text=tovar_code+","+listView_stroki.Items[lvi.Index].SubItems[1].Text+";"+
                            //    listView_stroki.Items[lvi.Index].SubItems[2].Text;
                            //wb.label_decription_document.Text = this.label_decription_document;
                            //wb.guid = guid;
                            //wb.ShowDialog();                     
                            find = true;
                            break;
                        }
                    }
                    if (!find)
                    {
                        //Теперь пробуем найти в скрытых строках которые не попали в отбор
                        if (!show_all_stroki)//Режим показа расхождений
                        {
                            if (add_string_listview(barcode)== 1)//Если дополнительно не нашли тогда спозиционируемя по старому алгоритму
                            {
                                find = true;
                            }
                        }
                        if (!find)
                        {
                            MessageBox.Show("В строках товар с кодом " + result_query + " и штрихкодом " + barcode + " не найден");
                        }
                    }

                }

                else
                {
                    MessageBox.Show("Товар со штрихкодом " + barcode + " не найден ");
                }

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

            if (find)
            {
                listView_stroki_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }
            else
            {
                //if (!show_all_stroki)//Режим показа расхождений
                //{
                //    if (add_string_listview(barcode) != 1)//Если дополнительно не нашли тогда спозиционируемя по старому алгоритму
                //    {
                //        listView_stroki.Focus();
                //        listView_stroki.Items[listView_stroki_index].Selected = true;
                //        listView_stroki.Items[listView_stroki_index].Focused = true;
                //    }
                //}
                //else
                //{
                    listView_stroki.Focus();
                    listView_stroki.Items[listView_stroki_index].Selected = true;
                    listView_stroki.Items[listView_stroki_index].Focused = true; 
                //}               
            }
        }


        private void  load_stroki()
        {
            int num_str = 0;

            if (show_all_stroki) 
            {
                if (listView_stroki.SelectedIndices.Count > 0)
                {
                    num_str = Convert.ToInt32(listView_stroki.Items[listView_stroki.SelectedIndices[0]].SubItems[0].Text) - 1;//запомним код позиции
                }
            }

           // listView_stroki.Enabled = false;
            if (type == "1")
            {
                this.Text = "Инвентаризация";
            }
            else if (type == "2")
            {
                this.Text = "Поступление";
            }
            else if (type == "3")
            {
                this.Text = "Заказ клиента";
            }
            else if (type == "4")
            {
                this.Text = " Проверка и печать ценников ";
            }


            listView_stroki.Items.Clear();

            SQLiteConnection conn = Program.ConnectForDataBase();
            try
            {
                conn.Open();
                string query = "SELECT type,date,info_1s,status,display_quantity FROM dh where guid=@guid";

                SQLiteParameter _guid = new SQLiteParameter("guid", guid);
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Parameters.Add(_guid);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    type = reader["type"].ToString();
                    date.Value = reader.GetDateTime(1);
                    txtB_info_1s.Text = reader["info_1s"].ToString();
                    //comment = txtB_info_1s.Text;
                    txtB_status.Text = reader["status"].ToString();
                    display_quantity = Convert.ToInt16(reader["display_quantity"]);
                }
                reader.Close();
                paint_column();


                //нарисовать колонки

                //if (display_quantity == 1)
                //{
                //    listView_stroki.Columns.Add("П", 20, HorizontalAlignment.Right);
                //}
                //listView_stroki.Columns.Add("Ц.пр.", 100, HorizontalAlignment.Right);

                string dop_query = "";
                if (!show_all_stroki)
                {
                    this.Text += "  Отбор ";//"  расхождения ) верх. кнопки ";
                    dop_query = " AND dt.quantity <> dt.quantity_shop ";
                }
                else
                {
                    this.Text += "";
                }

                query = "SELECT dt.tovar_code, dt.quantity,dt.quantity_shop,dt.price_buy,dt.price," +
                    " tovar.name AS tovar_name,characteristic.guid AS characteristic_guid," +
                    " characteristic.name AS characteristic_name,dt.line_number FROM dt " +
                    " LEFT JOIN tovar ON dt.tovar_code = tovar.code " +
                    " LEFT JOIN characteristic ON dt.characteristic = characteristic.guid " +
                    " where dt.guid=@guid " + dop_query +
                    " order by dt.line_number";
                _guid = new SQLiteParameter("guid", guid);
                command = new SQLiteCommand(query, conn);
                command.Parameters.Add(_guid);
                reader = command.ExecuteReader();
                //int i = 0;
                while (reader.Read())
                {

                    //if (!show_all_stroki)
                    //{
                    //    int result_find = find_tovar_in_listView_stroki(reader["tovar_code"].ToString(), reader["characteristic_name"].ToString());

                    //    if (result_find != -1)
                    //    {
                    //        // listView_stroki.Items[result_find].SubItems[1].Text; quantity_shop
                    //        // listView_stroki.Items[result_find].SubItems[2].Text; quantity
                    //        int i = 1;
                    //    }
                    //}

                    ListViewItem item = new ListViewItem(reader["line_number"].ToString());
                    //ListViewItem.ListViewSubItem lvi_sub = new ListViewItem.ListViewSubItem();
                    item.SubItems.Add(reader["quantity_shop"].ToString());
                    if (display_quantity == 1)
                    {
                        item.SubItems.Add(reader["quantity"].ToString());
                    }
                    item.SubItems.Add(reader["tovar_name"].ToString());
                    item.SubItems.Add(reader["characteristic_name"].ToString());
                    //item.SubItems.Add(reader["quantity_shop"].ToString());
                    //if (display_quantity == 1)
                    //{
                    //    item.SubItems.Add(reader["quantity"].ToString());
                    //}
                    item.SubItems.Add(reader["price_buy"].ToString());
                    //item.SubItems.Add(reader["price"].ToString());
                    //item.SubItems.Add(reader["barcode"].ToString()); 
                    item.Tag = reader["tovar_code"].ToString();
                    listView_stroki.Items.Add(item);

                    //i++;
                }
                //if (show_all_stroki)
                //{
                //    foreach (ListViewItem lvi in listView_stroki.Items)
                //    {
                //        listView_stroki.Focus();
                //        listView_stroki.Items[num_str].Selected = true;
                //        listView_stroki.Items[num_str].Focused = true;
                //    }
                //}
                //else //(listView_stroki.Items.Count > 0)
                //{          
                if (num_str != 0)
                {
                    if (listView_stroki.Items.Count != 0)
                    {
                        //if (listView_stroki.Items.Count > num_str + 1)
                        //{
                        //    listView_stroki.EnsureVisible(num_str + 1);
                        //    listView_stroki.Focus();
                        //    listView_stroki.Items[num_str].Selected = true;
                        //    listView_stroki.Items[num_str].Focused = true;
                        //}
                        //else
                        //{
                        listView_stroki.EnsureVisible(num_str);
                        listView_stroki.Focus();
                        listView_stroki.Items[num_str].Selected = true;
                        listView_stroki.Items[num_str].Focused = true;

                        //}
                    }
                }
                //}

                //удалить строки с одинаковым количеством
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
        }


        /// <summary>
        /// для отбора , для сложных случаев
        /// </summary>
        /// <param name="tovar_code"></param>
        /// <param name="characteristic_name"></param>
        /// <returns></returns>
        private int find_tovar_in_listView_stroki(string tovar_code, string characteristic_name)
        {
            int index = -1;
            foreach (ListViewItem item in listView_stroki.Items)
            {
                if (item.Tag.ToString() == tovar_code)
                {
                    index = item.Index;
                    if (characteristic_name.Trim().Length > 0)
                    {
                        if (display_quantity == 1)
                        {
                            if (item.SubItems[4].Text.Trim() != characteristic_name.Trim())
                            {
                                index = -1;
                            }
                        }
                        else
                        {
                            if (item.SubItems[1].Text.Trim() != characteristic_name.Trim())
                            {
                                index = -1;
                            }
                        }
                    }
                    if (index != -1)
                    {
                        break;
                    }
                }
            }
            return index;
        }


        private void paint_column()
        {

            if (listView_stroki.Columns.Count > 0)
            {
                listView_stroki.Columns.Clear();
            }

            if (show_all_stroki)
            {
                listView_stroki.Columns.Add("Н.с.", 30, HorizontalAlignment.Center);
                listView_stroki.Columns.Add("Ф", 30, HorizontalAlignment.Right);
                if (display_quantity == 1)
                {
                    listView_stroki.Columns.Add("П", 30, HorizontalAlignment.Right);
                }
                listView_stroki.Columns.Add("Номенклатура", 200, HorizontalAlignment.Left);
                listView_stroki.Columns.Add("Характеристика", 200, HorizontalAlignment.Left);
                listView_stroki.Columns.Add("Ц.пр.", 100, HorizontalAlignment.Right);
            }
            else
            {
                listView_stroki.Columns.Add("Н.с.", 30, HorizontalAlignment.Center);
                listView_stroki.Columns.Add("Ф", 30, HorizontalAlignment.Right);
                if (display_quantity == 1)
                {
                    listView_stroki.Columns.Add("П", 30, HorizontalAlignment.Right);
                }
                listView_stroki.Columns.Add("Номенклатура", 225, HorizontalAlignment.Left);
                //listView_stroki.Columns.Add("Характеристика", 200, HorizontalAlignment.Left);
                //listView_stroki.Columns.Add("Ц.пр.", 100, HorizontalAlignment.Right);
            }

        }

        private void Document_Load(object sender, EventArgs e)
        {
          

            // Set the view to show details.
            listView_stroki.View = View.Details;

            //listView_inventory.AllowColumnReorder = false;

            // Select the item and subitems when selection is made.
            listView_stroki.FullRowSelect = true;

            // Display grid lines.
            //listView_inventory.GridLines = true;

            // Sort the items in the list in ascending order.
            //listView1.Sorting = SortOrder.Ascending;
            //listView_stroki.Columns.Add("Н.с.", 60, HorizontalAlignment.Center);
            //listView_stroki.Columns.Add("Ф", 20, HorizontalAlignment.Right);
            //if (display_quantity == 1)
            //{
            //    listView_stroki.Columns.Add("П", 20, HorizontalAlignment.Right);
            //}
            //listView_stroki.Columns.Add("Номенклатура", 200, HorizontalAlignment.Left);
            //listView_stroki.Columns.Add("Характеристика", 200, HorizontalAlignment.Left);
            ////listView_stroki.Columns.Add("Ф", 20, HorizontalAlignment.Right);
            ////if (display_quantity == 1)
            ////{
            ////    listView_stroki.Columns.Add("П", 20, HorizontalAlignment.Right);
            ////}
            //listView_stroki.Columns.Add("Ц.пр.", 100, HorizontalAlignment.Right);
               
            ////listView_stroki.Columns.Add("Ц.з.", 100, HorizontalAlignment.Right);    
            //listView_stroki.Columns.Add("Штрихкод", 100, HorizontalAlignment.Left);    

            paint_column();
            load_stroki();

            //SQLiteConnection conn = Program.ConnectForDataBase();
            //try
            //{
            //    conn.Open();
            //    string query = "SELECT type,date,info_1s,status,display_quantity FROM dh where guid=@guid";
            //    SQLiteParameter _guid = new SQLiteParameter("guid", guid);
            //    SQLiteCommand command = new SQLiteCommand(query, conn);
            //    command.Parameters.Add(_guid);
            //    SQLiteDataReader reader = command.ExecuteReader();                
            //    while(reader.Read())
            //    {
            //        type = reader["type"].ToString();
            //        date.Value = reader.GetDateTime(1);
            //        txtB_info_1s.Text = reader["info_1s"].ToString();
            //        txtB_status.Text = reader["status"].ToString();
            //        display_quantity = Convert.ToInt16(reader["display_quantity"]);
            //    }
            //    reader.Close();

            //    if (display_quantity == 1)
            //    {
            //        listView_stroki.Columns.Add("П", 20, HorizontalAlignment.Right);
            //    }
            //    listView_stroki.Columns.Add("Ц.пр.", 100, HorizontalAlignment.Right); 

            //    query = "SELECT dt.tovar_code, dt.quantity,dt.quantity_shop,dt.price_buy,dt.price,"+
            //        " tovar.name AS tovar_name,characteristic.guid AS characteristic_guid,"+
            //        " characteristic.name AS characteristic_name,dt.line_number FROM dt " +                    
            //        " LEFT JOIN tovar ON dt.tovar_code = tovar.code " +
            //        " LEFT JOIN characteristic ON dt.characteristic = characteristic.guid " +                    
            //        " where dt.guid=@guid order by dt.line_number";
            //    _guid = new SQLiteParameter("guid", guid);
            //    command = new SQLiteCommand(query, conn);
            //    command.Parameters.Add(_guid);
            //    reader = command.ExecuteReader();
            //    //int i = 0;
            //    while (reader.Read())
            //    {                  
            //        ListViewItem item = new ListViewItem(reader["line_number"].ToString());
            //        //ListViewItem.ListViewSubItem lvi_sub = new ListViewItem.ListViewSubItem();
                    
            //        item.SubItems.Add(reader["tovar_name"].ToString());
            //        item.SubItems.Add(reader["characteristic_name"].ToString());
            //        item.SubItems.Add(reader["quantity_shop"].ToString());
            //        if (display_quantity == 1)
            //        {
            //            item.SubItems.Add(reader["quantity"].ToString());
            //        }                    
            //        item.SubItems.Add(reader["price_buy"].ToString());
            //        //item.SubItems.Add(reader["price"].ToString());
            //        //item.SubItems.Add(reader["barcode"].ToString()); 
            //        item.Tag = reader["tovar_code"].ToString();                    
            //        listView_stroki.Items.Add(item);                    
                    
            //        //i++;
            //    }

            //    if (listView_stroki.Items.Count > 0)
            //    {
            //        listView_stroki.Focus();
            //        listView_stroki.Items[0].Selected = true;
            //        listView_stroki.Items[0].Focused  = true;
            //    }

            //    conn.Close();

            //}
            //catch (SQLiteException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    if (conn.State == ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //}

        }

        private void btn_Esc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_write_Click(object sender, EventArgs e)
        {
            SQLiteConnection conn = Program.ConnectForDataBase();
            SQLiteTransaction trans=null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                //Удаление шапки документа
                string query = "DELETE FROM dh where guid=@guid";
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter _guid = new SQLiteParameter("guid", guid);
                command.Transaction = trans;
                command.Parameters.Add(_guid);
                command.ExecuteNonQuery();

                //Удаление строк документа

                query = "DELETE FROM dt where guid=@guid";
                command = new SQLiteCommand(query, conn);
                _guid = new SQLiteParameter("guid", guid);
                command.Transaction = trans;
                command.Parameters.Add(_guid);
                command.ExecuteNonQuery();

                //Вставка шапки документа

                query = "INSERT INTO dh(type,date,guid,info_1s,status)VALUES(@type,@date,@guid,@info_1s,@status);";
                SQLiteParameter _type = new SQLiteParameter("type", type);
                SQLiteParameter _date = new SQLiteParameter("date", date.Value.ToString("yyyy-MM-dd"));
                _guid = new SQLiteParameter("guid", guid);
                SQLiteParameter _info_1s = new SQLiteParameter("info_1s", txtB_info_1s.Text.Trim());
                SQLiteParameter _status = new SQLiteParameter("status", Convert.ToInt16(txtB_status.Text.Trim()));

                command.Transaction = trans;
                command.Parameters.Add(_type);
                command.Parameters.Add(_date);
                command.Parameters.Add(_guid);
                command.Parameters.Add(_info_1s);
                command.Parameters.Add(_status);
                command.ExecuteNonQuery();
                
                _guid = new SQLiteParameter("guid", guid);

                foreach (ListViewItem lvi in listView_stroki.Items)
                {
                    query = "INSERT INTO dt(guid, tovar_code, characteristic,quantity,quantity_shop,price_buy,price,line_number)VALUES " +
                        "(@guid, @tovar_code, @characteristic, @quantity, @quantity_shop, @price_buy, @price,line_number);";
                    _guid = new SQLiteParameter("guid", guid);
                    SQLiteParameter _tovar_code = new SQLiteParameter("tovar_code", lvi.Tag.ToString());
                    SQLiteParameter _characteristic = new SQLiteParameter("characteristic", lvi.SubItems[2].Text);
                    SQLiteParameter _quantity = new SQLiteParameter("quantity", lvi.SubItems[3].Text);
                    SQLiteParameter _quantity_shop = new SQLiteParameter("quantity_shop", SqlDbType.Int);

                    if (lvi.SubItems[4].Text.Trim() != "")
                    {
                        _quantity_shop.Value = Convert.ToInt16(lvi.SubItems[4].Text);
                    }
                    else
                    {
                        _quantity_shop.Value = 0;
                    }
                    
                    SQLiteParameter _price_buy = new SQLiteParameter("price_buy", 1);
                    SQLiteParameter _price = new SQLiteParameter("price", 1);

                        command = new SQLiteCommand(query, conn);
                        command.Parameters.Add(_guid);
                        command.Parameters.Add(_tovar_code);
                        command.Parameters.Add(_characteristic);
                        command.Parameters.Add(_quantity);
                        command.Parameters.Add(_quantity_shop);
                        command.Parameters.Add(_price_buy);
                        command.Parameters.Add(_price);
                        command.Transaction = trans;
                        command.ExecuteNonQuery();
                }
                trans.Commit();
                conn.Close();                
            }
            catch(SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                if (trans != null)
                {
                    trans.Rollback();
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            this.Close();
        }
    }
}
