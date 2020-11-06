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
    public partial class DocumentInventory1 : Form
    {

        public string guid = "";
        public string typ_doc = "";
        public int its_new = 0;



        public DocumentInventory1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Load += new EventHandler(DocumentInventory1_Load);
           
        }

        public bool write_new_document(string info_1s)
        {
            bool result = true;

             //" type smallint NOT NULL,"+
             //      " date datetime NOT NULL," +
             //      " guid nvarchar(36) NOT NULL," +
             //      " info_1s nvarchar(200),"+
             //      " status smallint,"+
             //      " display_quantity smallint"+

            SQLiteConnection conn = Program.ConnectForDataBase();
            try
            {
                conn.Open();
                string query = "INSERT INTO dh(type,date,guid,info_1s,status,display_quantity,its_new,db_id)" +
                    " VALUES(@type,@date,@guid,@info_1s,@status,@display_quantity,@its_new,@db_id)";
                SQLiteParameter _type = new SQLiteParameter("type", typ_doc);               
                SQLiteParameter _date = new SQLiteParameter("date", DateTime.Now);
                SQLiteParameter _guid = new SQLiteParameter("guid", guid);
                SQLiteParameter _info_1s = new SQLiteParameter("info_1s", info_1s);
                SQLiteParameter _status = new SQLiteParameter("status", SqlDbType.Int);
                _status.Value = 0;
                SQLiteParameter _display_quantity = new SQLiteParameter("display_quantity", SqlDbType.Int);
                _display_quantity.Value = 1;
                SQLiteParameter _its_new = new SQLiteParameter("its_new", SqlDbType.Int);
                _display_quantity.Value = this.its_new;
                int db_id = Program.GetDbId();
                if (db_id == -1)
                {
                    return false;
                }
                SQLiteParameter _db_id = new SQLiteParameter("db_id", SqlDbType.Int);
                _display_quantity.Value = db_id;                               
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Parameters.Add(_type);
                command.Parameters.Add(_date);
                command.Parameters.Add(_guid);
                command.Parameters.Add(_info_1s);
                command.Parameters.Add(_status);
                command.Parameters.Add(_display_quantity);
                command.Parameters.Add(_its_new);
                command.Parameters.Add(_db_id); 
                command.ExecuteNonQuery();
                conn.Close();
            }
                
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
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
        

        protected override void OnKeyDown(KeyEventArgs e)
        {
           // base.OnKeyDown(e);
            if (e.KeyCode == Keys.D1)
            {
                btn_start_continue_Click(null, null);               
            }
            else if (e.KeyCode == Keys.D2)
            {
                btn_show_divergence_Click(null, null);
            }
            else if (e.KeyCode == Keys.D8)
            {
                btn_delete_Click(null, null);
            }
            else if (e.KeyCode == Keys.D9)
            {
                btn_complete_Click(null,null);
            }             
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }            
        }


        private void DocumentInventory1_Load(object sender, EventArgs e)
        {
             SQLiteConnection conn = Program.ConnectForDataBase();
             try
             {
                 conn.Open();
                 string query = "SELECT type,date,info_1s,status,its_new FROM dh where guid=@guid";
                 SQLiteParameter _guid = new SQLiteParameter("guid", guid);
                 SQLiteCommand command = new SQLiteCommand(query, conn);
                 command.Parameters.Add(_guid);
                 
                 SQLiteDataReader reader = command.ExecuteReader();
                 while (reader.Read())
                 {
                     
                     string type = reader["type"].ToString();
                     string date = reader.GetDateTime(1).ToString("dd-MM-yyyy");
                     string txt_info_1s = reader["info_1s"].ToString();
                     string txt_status = reader["status"].ToString();
                     its_new = Convert.ToInt16(reader["its_new"].ToString());
                     if(txt_status=="2")
                     {
                         btn_complete.Enabled = false;
                     }

                     string shapka = "";
                     if (type == "1")
                     {
                         shapka = "Инвентаризация";
                     }
                     this.Text = shapka +" от " + date;
                     label_decription_document.Text = txt_info_1s;
                 }
                 reader.Close();
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
             if (typ_doc == "4")
             {
                 btn_delete.Enabled = true;
             }
        }

        private void btn_start_continue_Click(object sender, EventArgs e)
        {
            if (typ_doc != "4")
            {
                WorkWithBarcode wb = new WorkWithBarcode();
                //wb.label_decription_document.Text = this.label_decription_document.Text;
                //wb.tovar_code
                wb.guid = guid;
                wb.typ_doc = this.typ_doc;
                wb.its_new = this.its_new;
                wb.ShowDialog();
                wb.Dispose();
            }
            else
            {
                CheckAndPrintPrices chp = new CheckAndPrintPrices();
                chp.guid = guid;
                chp.typ_doc = this.typ_doc;
                chp.its_new = this.its_new;
                chp.ShowDialog();                
                chp.Dispose();
            }
        }

      

        private void btn_show_divergence_Click(object sender, EventArgs e)
        {
            DocumentList doc = new DocumentList();
            doc.guid = guid;
            doc.label_decription_document = this.label_decription_document.Text;            
            doc.its_new = false;
            doc.type = this.typ_doc;
            doc.ShowDialog();
            doc.Dispose();
        }


        /// <summary>
        /// Проверка на наличие строк у документа, если строк нет
        /// то не завершаем документ и не отправляем его
        /// </summary>
        /// <returns></returns>
        private bool check_have_stroki()
        {            
            bool result = true;
            SQLiteConnection conn = Program.ConnectForDataBase();
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM dt WHERE guid='"+guid+"'";
                SQLiteCommand commаnd = new SQLiteCommand(query, conn);
                object result_query = commаnd.ExecuteScalar();
                if (Convert.ToInt32(result_query) == 0)
                {
                    result = false;
                }
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
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

        private bool check_have_not_zero_string()
        {
            bool result = true;

            SQLiteConnection conn = Program.ConnectForDataBase();

            try
            {
                conn.Open();
                string query = "SELECT SUM(quantity_shop) FROM dt WHERE guid='" + guid + "'";
                SQLiteCommand command = new SQLiteCommand(query, conn);
                object result_qery = command.ExecuteScalar();
                if (Convert.ToInt32(result_qery) == 0)
                {
                    result = false;
                }
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
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
        


        private void btn_complete_Click(object sender, EventArgs e)
        {

            //if (MessageBox.Show("Вы хотите завершить документ ?", "Завершение документа", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            if(DialogResult.Yes != MessageBox.Show("Вы хотите завершить документ ?","", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                return;
            }
            

            //Проверить на наличие строк и на заполненость хотя бы одной строки документа с заполненным количеством.
            if (!check_have_stroki())
            {
                MessageBox.Show(" В документе нет строк, он не может быть завершен ");
                return;
            }

            if (!check_have_not_zero_string())
            {
                MessageBox.Show(" В документе нет обработанных строк, он не может быть завершен ");
                return;
            }

            Program.BackupDatabases();

            SQLiteConnection conn = Program.ConnectForDataBase();

            try
            {
                conn.Open();
                string query = " UPDATE dh SET status=2 WHERE guid='" + guid + "'";
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();
                this.Close();
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
             
            this.Close();

            //Program.BackupDatabases();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Вы хотите безвозвратно удалить документ ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                return;
            }

            SQLiteConnection conn = Program.ConnectForDataBase();
            SQLiteTransaction tran = null;

            try
            {
                conn.Open();
                tran = conn.BeginTransaction();
                string query = " DELETE FROM dh WHERE guid='" + guid + "'";
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Transaction = tran;
                command.ExecuteNonQuery();
                query = " DELETE FROM dt WHERE guid='" + guid + "'";
                command = new SQLiteCommand(query, conn);
                command.Transaction = tran;
                command.ExecuteNonQuery();
                tran.Commit();
                tran.Dispose();
                command.Dispose();
                conn.Close();                
                this.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                if (tran != null)
                {
                    tran.Rollback();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (tran != null)
                {
                    tran.Rollback();
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            conn.Dispose();                
        }
        
    }
}