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
        private LendingFrom _lf;
        private PaymentForm _pf;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nHiba, nem sikerült kapcsolódni az adatbázishoz!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void MenuTable_befizetesek_Clicked(object sender, EventArgs e)
        {
            OnMenuTableClicked("befizetesek");
        }

        private void MenuTable_kolcsonzesek_Clicked(object sender, EventArgs e)
        {
            OnMenuTableClicked("kolcsonzesek");
        }

        private void MenuTable_Konyvek_Clicked(object sender, EventArgs e)
        {
            OnMenuTableClicked("konyvek");
        }

        private void MenuTable_olvasasok_Clicked(object sender, EventArgs e)
        {
            OnMenuTableClicked("olvasasok");
        }

        private void MenuTable_statuszok_Clicked(object sender, EventArgs e)
        {
            OnMenuTableClicked("statuszok");
        }

        private void MenuTable_tagok_Clicked(object sender, EventArgs e)
        {
            OnMenuTableClicked("tagok");
        }

        private void MenuTable_kilepes_Clicked(object sender, EventArgs e)
        {
            Close();
        }

        private void kölcsönzésToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                _lf = new LendingFrom(_con);
                _lf.MdiParent = this;
                _lf.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nHiba, nem tudom lekérdezni a tábla adatait!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tagdíjFizetésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _pf = new PaymentForm(_con);
                _pf.MdiParent = this;
                _pf.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nHiba, nem tudom lekérdezni a tábla adatait!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hell");
        }
    }
}
