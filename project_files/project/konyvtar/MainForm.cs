using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace konyvtar
{
    public partial class MainForm : Form
    {
        private TableForm _tf;
        private SQLiteConnection _con;

        public MainForm()
        {
            InitializeComponent();
            Load += new EventHandler(MainForm_Load);

        }

        void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                _con = new SQLiteConnection("Data Source=Konyvtar.db;Version=3;");
                _con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\nHiba, nem sikerült kapcsolódni az adatbázishoz!","Hiba!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                _con.Close();            
            }            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_con != null && _con.State == ConnectionState.Open)
                _con.Close();
        }

        private void OnMenuTableClicked(String _table_name)
        {
            try
            {
                _tf = new TableForm(_con, _table_name);
                _tf.MdiParent = this;
                _tf.Show();
            }
            catch (Exception ex)            
            {
                MessageBox.Show(ex.Message + "\nHiba, nem tudom lekérdezni a tábla adatait!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }

        /*private void MenuTable_kolcsonzesek_Clicked(object sender, EventArgs e)
        {
            OnMenuTableClicked("kolcsonzesek");
        }*/

        private void MenuTable_Konyvek_Clicked(object sender, EventArgs e)
        {
            OnMenuTableClicked("konyvek");
        }

        private void MenuTable_kilepes_Clicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}
