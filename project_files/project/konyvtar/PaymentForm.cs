using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;


namespace konyvtar
{
    public partial class PaymentForm : Form
    {

        private SQLiteConnection _con;

        private SQLiteDataAdapter _adapTagok;
        private SQLiteDataAdapter _adapStatuszok;
        private SQLiteDataAdapter _adapBefizetesek;
        private int bizonylatszam = 0;

        public PaymentForm(SQLiteConnection c)
        {
            InitializeComponent();

            _dataGridViewTagok.SelectionChanged += new EventHandler(dataGridViewTagok_SelectionChanged);

            _con = c;

            _dataGridViewTagok.AutoGenerateColumns = false;
            _dataGridViewTagok.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridViewTagok.ReadOnly = true;

            _dataGridViewBefizetesek.AutoGenerateColumns = false;
            _dataGridViewBefizetesek.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridViewBefizetesek.ReadOnly = true;

            LoadData();
        }

        private void LoadData()
        {
            SQLiteCommand command = _con.CreateCommand();
            command.CommandText = "SELECT MAX(bizonylatszam) FROM befizetesek";
            bizonylatszam = Convert.ToInt32(command.ExecuteScalar());

            _adapTagok = new SQLiteDataAdapter("SELECT * FROM tagok", _con);
            _adapBefizetesek = new SQLiteDataAdapter("SELECT * FROM befizetesek", _con);

            SQLiteCommandBuilder _builderTagok = new SQLiteCommandBuilder(_adapTagok);
            _adapTagok.InsertCommand = _builderTagok.GetInsertCommand();
            _adapTagok.DeleteCommand = _builderTagok.GetDeleteCommand();
            _adapTagok.UpdateCommand = _builderTagok.GetUpdateCommand();

            SQLiteCommandBuilder _builderBefizetesek = new SQLiteCommandBuilder(_adapBefizetesek);
            _adapBefizetesek.InsertCommand = _builderBefizetesek.GetInsertCommand();
            _adapBefizetesek.DeleteCommand = _builderBefizetesek.GetDeleteCommand();
            _adapBefizetesek.UpdateCommand = _builderBefizetesek.GetUpdateCommand();

            _dataSet = new DataSet("befizetesek");

            _adapTagok.Fill(_dataSet, "tagok");
            _bindingSourceTagok.DataSource = _dataSet.Tables["tagok"];
            _dataGridViewTagok.DataSource = _bindingSourceTagok;

            _adapBefizetesek.Fill(_dataSet, "befizetesek");
            _bindingSourceBefizetesek.DataSource = _dataSet.Tables["befizetesek"];
            _dataGridViewBefizetesek.DataSource = _bindingSourceBefizetesek;

            FormatTagokTable();
            FormatBefizetesekTable();

        }

        private void FormatTagokTable()
        {

            _dataSet.Relations.Add("Eredo", _dataSet.Tables["tagok"].Columns["tag_id"], _dataSet.Tables["befizetesek"].Columns["tag_id"]);
            _dataSet.Tables["tagok"].Columns.Add("eredo", typeof(Int32), "Sum(Child.osszeg)");



            _dataSet.Tables[0].Columns[0].AutoIncrement = true;
            SQLiteCommand command = _con.CreateCommand();
            command.CommandText = "SELECT MAX(tag_id) FROM tagok";
            _dataSet.Tables[0].Columns[0].AutoIncrementSeed = Convert.ToInt32(command.ExecuteScalar()) + 1;
            _dataSet.Tables[0].Columns[0].AutoIncrementStep = 1;

            _adapStatuszok = new SQLiteDataAdapter("SELECT * FROM statuszok", _con);
            _adapStatuszok.Fill(_dataSet, "statuszok");
            _bindingSourceStatuszok = new BindingSource();
            _bindingSourceStatuszok.DataSource = _dataSet.Tables["statuszok"];

            //tábla fejlécének formázása
            DataGridViewColumn[] columns = new DataGridViewColumn[7];

            columns[0] = new DataGridViewTextBoxColumn();
            columns[0].HeaderText = "Tag ID";
            columns[0].DataPropertyName = "tag_id";
            columns[0].Visible = false;

            columns[1] = new DataGridViewTextBoxColumn();
            columns[1].HeaderText = "Név";
            columns[1].DataPropertyName = "nev";

            columns[2] = new DataGridViewTextBoxColumn();
            columns[2].HeaderText = "Cím";
            columns[2].DataPropertyName = "cim";

            columns[3] = new DataGridViewTextBoxColumn();
            columns[3].HeaderText = "Azonosító";
            columns[3].DataPropertyName = "azon";

            columns[4] = new DataGridViewTextBoxColumn();
            columns[4].HeaderText = "Könyvtár jegy";
            columns[4].DataPropertyName = "konyvtarjegy";

            columns[5] = new DataGridViewComboBoxColumn();
            columns[5].HeaderText = "Státusz";
            columns[5].DataPropertyName = "statusz_id";
            ((DataGridViewComboBoxColumn)columns[5]).DataSource = _bindingSourceStatuszok;
            ((DataGridViewComboBoxColumn)columns[5]).ValueMember = "statusz_id";
            ((DataGridViewComboBoxColumn)columns[5]).DisplayMember = "statusz";

            columns[6] = new DataGridViewTextBoxColumn();
            columns[6].HeaderText = "Eredő";
            columns[6].DataPropertyName = "eredo";


            foreach (DataGridViewColumn col in columns)
            {
                _dataGridViewTagok.Columns.Add(col);
            }

        }

        private void FormatBefizetesekTable()
        {
            //azonosító automatikus növelése
            _dataSet.Tables[1].Columns[0].AutoIncrement = true;
            SQLiteCommand command = _con.CreateCommand();
            command.CommandText = "SELECT MAX(bizonylatszam) FROM befizetesek";
            _dataSet.Tables[1].Columns[0].AutoIncrementSeed = Convert.ToInt32(command.ExecuteScalar()) + 1;
            _dataSet.Tables[1].Columns[0].AutoIncrementStep = 1;


            //tábla fejlécének formázása
            DataGridViewColumn[] columns = new DataGridViewColumn[4];

            columns[0] = new DataGridViewTextBoxColumn();
            columns[0].HeaderText = "Bizonylatszám";
            columns[0].DataPropertyName = "bizonylatszam";
            columns[0].Visible = false;

            //ezt kéne idegenkulcsként megjeleníteni
            columns[1] = new DataGridViewComboBoxColumn();
            columns[1].HeaderText = "Tag neve";
            columns[1].DataPropertyName = "tag_id";
            ((DataGridViewComboBoxColumn)columns[1]).DataSource = _bindingSourceTagok;
            ((DataGridViewComboBoxColumn)columns[1]).ValueMember = "tag_id";
            ((DataGridViewComboBoxColumn)columns[1]).DisplayMember = "nev";

            columns[2] = new DataGridViewTextBoxColumn();
            columns[2].HeaderText = "Összeg";
            columns[2].DataPropertyName = "osszeg";

            columns[3] = new DataGridViewTextBoxColumn();
            columns[3].HeaderText = "Dátum";
            columns[3].DataPropertyName = "datum";


            foreach (DataGridViewColumn col in columns)
            {
                _dataGridViewBefizetesek.Columns.Add(col);
            }

        }

        private void dataGridViewTagok_SelectionChanged(object sender, EventArgs e)
        {
            SetTagokFilter();
        }

        private void SetFilter()
        {

            string s = "";


            if (_numericUpDown.Value != 0)
            {
                s += "convert(datum,'System.String') LIKE'%" + _numericUpDown.Value.ToString() + "%' AND ";
            }


            if (s.Length > 5)
            {
                s = s.Substring(0, s.Length - 5);
            }
            _bindingSourceBefizetesek.Filter = s;


        }

        private void SetTagokFilter()
        {
            String s = "";

            for (int i = 0; i < _dataGridViewTagok.SelectedRows.Count; i++)
            {
                s += "tag_id = " + _dataSet.Tables["tagok"].Rows[_dataGridViewTagok.SelectedRows[i].Index][0] + " OR ";
            }

            if (s.Length > 4)
            {
                s = s.Substring(0, s.Length - 4);
            }

            _bindingSourceBefizetesek.Filter = s;
        }

        private void _numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void _buttonRemoveFilter_Click(object sender, EventArgs e)
        {
            _bindingSourceBefizetesek.Filter = "";
            _numericUpDown.Value = 2010;
            SetTagokFilter();
        }

        private void _buttonBefizetes_Click(object sender, EventArgs e)
        {
            bool l = true;
            bool k = true;

            //ellenőrzöm, hogy lett-e kijelölve tag
            if (_dataGridViewTagok.SelectedRows.Count != 1)
            {
                MessageBox.Show("A befizetéshez egy tagot jelölj ki!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                l = false;
            }

            //ellenőrzöm, hogy írt-e valamit a textboxba és ha igen az helyese
            Int32 n;
            if (!Int32.TryParse(_textBoxBefizetes.Text, out n) || n <= -1) // ha nem pozitív számot írtunk be
            {
                MessageBox.Show("Hiba! A mezőben csak pozitív szám szerepelhet!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                k = false;
            }

            if (l && k)
            {               

                bizonylatszam += 1;
                Validate();
                _bindingSourceTagok.EndEdit();
                _bindingSourceBefizetesek.EndEdit();
                DataRow row = _dataSet.Tables["befizetesek"].NewRow();
                row["bizonylatszam"] = bizonylatszam;
                row["tag_id"] = Convert.ToInt32(_dataGridViewTagok.SelectedCells[0].FormattedValue);
                row["osszeg"] = Convert.ToInt32(_textBoxBefizetes.Text);
                row["datum"] = DateTime.Now;
                _dataSet.Tables["befizetesek"].Rows.Add(row);
            }
        }

        private void _buttonSztorno_Click(object sender, EventArgs e)
        {
            bool l = true;
            bool k = true;
            //ellenőrzöm, hogy lett-e kijelölve bizonylat
            if (_dataGridViewBefizetesek.SelectedRows.Count != 1)
            {
                MessageBox.Show("A sztornózáshoz egy befizetést jelölj ki!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                l = false;
            }

            //sztornót nem lehet sztornózni...
            if (Convert.ToInt32(_dataGridViewBefizetesek.Rows[_dataGridViewBefizetesek.SelectedRows[0].Index].Cells[2].FormattedValue) < 0)
            {
                MessageBox.Show("Sztornózott bejgyzés nem sztornózható!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                k = false;
            
            }

            //csak akkor sztornózok ha nem volt hiba
            if (l && k)
            {
                bizonylatszam += 1;


                Validate();
                _bindingSourceTagok.EndEdit();
                _bindingSourceBefizetesek.EndEdit();

                DataRow row = _dataSet.Tables["befizetesek"].NewRow();

                row["bizonylatszam"] = bizonylatszam;
                row["tag_id"] = Convert.ToInt32(_dataGridViewBefizetesek.SelectedCells[1].Value);
                row["osszeg"] = Convert.ToInt32(_dataGridViewBefizetesek.SelectedCells[2].FormattedValue) * -1;
                row["datum"] = DateTime.Now;
//                row["sztorno"] = Convert.ToInt32(_dataGridViewBefizetesek.SelectedCells[0].FormattedValue);
                _dataSet.Tables["befizetesek"].Rows.Add(row);
            }

        }

        private void _buttonRogzites_Click(object sender, EventArgs e)
        {
            Validate();
            _bindingSourceBefizetesek.EndEdit();
            
            try
            {
                _adapBefizetesek.Update(_dataSet, "befizetesek");
            }
            catch (SQLiteException)
            {
                MessageBox.Show("Az adatok mentése sikertelen!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            
        }        
    }
}
