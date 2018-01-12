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

    public partial class LendingFrom : Form
    {
        private SQLiteConnection _con;

        private SQLiteDataAdapter _adapKolcsonzesek;
        private SQLiteDataAdapter _adapTagok;
        private SQLiteDataAdapter _adapKonyvek;
        private SQLiteDataAdapter _adapStatuszok;
        private BindingSource _bindingSourceStatuszok;
        private int kolcson_id = 0;
        

        public LendingFrom(SQLiteConnection c)
        {
            InitializeComponent();

            //eseménykezelő kapcsolt táblához
            _dataGridViewTagok.SelectionChanged += new EventHandler(dataGridViewTagok_SelectionChanged);
            

            _con = c;

            // hogy kézzel formázhassuk az oszlopokat
            _dataGridViewKolcsonzesek.AutoGenerateColumns = false; 
            _dataGridViewTagok.AutoGenerateColumns = false;
            _dataGridViewKonyvek.AutoGenerateColumns = false;

            //ne lehessen szerkeszteni
            _dataGridViewTagok.ReadOnly = true;
            _dataGridViewKolcsonzesek.ReadOnly = true;
            _dataGridViewKonyvek.ReadOnly = true;

            //csak teljes sorokat lehessen kijelölni
            _dataGridViewTagok.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridViewKolcsonzesek.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridViewKonyvek.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //csak egy sort lehessen kijelölni
            _dataGridViewKolcsonzesek.MultiSelect = false;
            _dataGridViewTagok.MultiSelect = false;
            _dataGridViewKonyvek.MultiSelect = false;



            LoadData();
        }

        private void LoadData()
        {
            SQLiteCommand command = _con.CreateCommand();
            command.CommandText = "SELECT MAX(kolcson_id) FROM kolcsonzesek";
            kolcson_id = Convert.ToInt32(command.ExecuteScalar());

            _adapKolcsonzesek = new SQLiteDataAdapter("SELECT * FROM kolcsonzesek WHERE vissza is null", _con);
            _adapTagok = new SQLiteDataAdapter("SELECT * FROM tagok", _con);
            _adapKonyvek = new SQLiteDataAdapter("SELECT * FROM konyvek", _con);

            SQLiteCommandBuilder _builderKolcsonzesek = new SQLiteCommandBuilder(_adapKolcsonzesek);
            _adapKolcsonzesek.InsertCommand = _builderKolcsonzesek.GetInsertCommand();
            _adapKolcsonzesek.DeleteCommand = _builderKolcsonzesek.GetDeleteCommand();
            _adapKolcsonzesek.UpdateCommand = _builderKolcsonzesek.GetUpdateCommand();

            SQLiteCommandBuilder _builderTagok = new SQLiteCommandBuilder(_adapTagok);
            _adapTagok.InsertCommand = _builderTagok.GetInsertCommand();
            _adapTagok.DeleteCommand = _builderTagok.GetDeleteCommand();
            _adapTagok.UpdateCommand = _builderTagok.GetUpdateCommand();

            SQLiteCommandBuilder _builderKonyvek = new SQLiteCommandBuilder(_adapKonyvek);
            _adapKonyvek.InsertCommand = _builderKonyvek.GetInsertCommand();
            _adapKonyvek.DeleteCommand = _builderKonyvek.GetDeleteCommand();
            _adapKonyvek.UpdateCommand = _builderKonyvek.GetUpdateCommand();


            _dataSet = new DataSet("kolcsonzes");

                       
            _adapKolcsonzesek.Fill(_dataSet, "kolcsonzesek");
            _bindingSourceKolcsonzesek.DataSource = _dataSet.Tables["kolcsonzesek"];
            _dataGridViewKolcsonzesek.DataSource = _bindingSourceKolcsonzesek;

            _adapTagok.Fill(_dataSet, "tagok");
            _bindingSourceTagok.DataSource = _dataSet.Tables["tagok"];
            _dataGridViewTagok.DataSource = _bindingSourceTagok;

            _adapKonyvek.Fill(_dataSet, "konyvek");
            _bindingSourceKonyvek.DataSource = _dataSet.Tables["konyvek"];
            _dataGridViewKonyvek.DataSource = _bindingSourceKonyvek;


            FormatKolcsonzesekTable();
            FormatTagokTable();
            FormatKonyvekTable();
        }

        private void FormatKolcsonzesekTable()
        {
            _dataSet.Tables[0].Columns[0].AutoIncrement = true;
            SQLiteCommand command = _con.CreateCommand();
            command.CommandText = "SELECT MAX(kolcson_id) FROM kolcsonzesek";
            _dataSet.Tables[0].Columns[0].AutoIncrementSeed = Convert.ToInt32(command.ExecuteScalar()) + 1;
            _dataSet.Tables[0].Columns[0].AutoIncrementStep = 1;


            //tábla fejlécének formázása
            DataGridViewColumn[] columns = new DataGridViewColumn[6];
            columns[0] = new DataGridViewTextBoxColumn();
            columns[0].HeaderText = "Kölcsönzés ID";
            columns[0].DataPropertyName = "kolcson_id";
            columns[0].Visible = false;

            columns[1] = new DataGridViewComboBoxColumn();
            columns[1].HeaderText = "Tag neve";
            columns[1].DataPropertyName = "tag_id";
            ((DataGridViewComboBoxColumn)columns[1]).DataSource = _bindingSourceTagok;
            ((DataGridViewComboBoxColumn)columns[1]).ValueMember = "tag_id";
            ((DataGridViewComboBoxColumn)columns[1]).DisplayMember = "nev";

            columns[2] = new DataGridViewComboBoxColumn();
            columns[2].HeaderText = "Könyv címe";
            columns[2].DataPropertyName = "konyv_id";
            ((DataGridViewComboBoxColumn)columns[2]).DataSource = _bindingSourceKonyvek;
            ((DataGridViewComboBoxColumn)columns[2]).ValueMember = "konyv_id";
            ((DataGridViewComboBoxColumn)columns[2]).DisplayMember = "cim";

            columns[3] = new DataGridViewTextBoxColumn();
            columns[3].HeaderText = "Kezdet";
            columns[3].DataPropertyName = "kezdte";

            columns[4] = new DataGridViewTextBoxColumn();
            columns[4].HeaderText = "Vissza";
            columns[4].DataPropertyName = "Vissza";

            columns[5] = new DataGridViewCheckBoxColumn();
            columns[5].HeaderText = "Lejárt";
            
                        
            foreach (DataGridViewColumn col in columns)
            {
               _dataGridViewKolcsonzesek.Columns.Add(col);
            }            
        }

        private void FormatTagokTable()
        {
            _dataSet.Tables[1].Columns[0].AutoIncrement = true;
            SQLiteCommand command = _con.CreateCommand();
            command.CommandText = "SELECT MAX(tag_id) FROM tagok";
            _dataSet.Tables[1].Columns[0].AutoIncrementSeed = Convert.ToInt32(command.ExecuteScalar()) + 1;
            _dataSet.Tables[1].Columns[0].AutoIncrementStep = 1;

            _adapStatuszok = new SQLiteDataAdapter("SELECT * FROM statuszok", _con);
            _adapStatuszok.Fill(_dataSet, "statuszok");
            _bindingSourceStatuszok = new BindingSource();
            _bindingSourceStatuszok.DataSource = _dataSet.Tables["statuszok"]; 

            //tábla fejlécének formázása
            DataGridViewColumn[] columns = new DataGridViewColumn[6];

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

            foreach (DataGridViewColumn col in columns)
            {
                _dataGridViewTagok.Columns.Add(col);
            }        
        }

        private void FormatKonyvekTable()
        {
            _dataSet.Tables[2].Columns[0].AutoIncrement = true;
            SQLiteCommand command = _con.CreateCommand();
            command.CommandText = "SELECT MAX(konyv_id) FROM konyvek";
            _dataSet.Tables[2].Columns[0].AutoIncrementSeed = Convert.ToInt32(command.ExecuteScalar()) + 1;
            _dataSet.Tables[2].Columns[0].AutoIncrementStep = 1;

            //tábla fejlécének formázása
            DataGridViewColumn[] columns = new DataGridViewColumn[6];

            columns[0] = new DataGridViewTextBoxColumn();
            columns[0].HeaderText = "Könyv ID";
            columns[0].DataPropertyName = "konyv_id";
            columns[0].Visible = false;

            columns[1] = new DataGridViewTextBoxColumn();
            columns[1].HeaderText = "Azonosító";
            columns[1].DataPropertyName = "azon";

            columns[2] = new DataGridViewTextBoxColumn();
            columns[2].HeaderText = "Cím";
            columns[2].DataPropertyName = "cim";

            columns[3] = new DataGridViewTextBoxColumn();
            columns[3].HeaderText = "Szerző";
            columns[3].DataPropertyName = "szerzo";

            columns[4] = new DataGridViewTextBoxColumn();
            columns[4].HeaderText = "Példányszám";
            columns[4].DataPropertyName = "peldanyszam";

            columns[5] = new DataGridViewTextBoxColumn();
            columns[5].HeaderText = "Szabad példányszám";
            columns[5].DataPropertyName = "szabad_peldanyszam";

            foreach (DataGridViewColumn col in columns)
            {
                _dataGridViewKonyvek.Columns.Add(col);
            }        
        }

        private void dataGridViewTagok_SelectionChanged(object sender, EventArgs e)
        {
            String s="";

            for (int i = 0; i < _dataGridViewTagok.SelectedRows.Count; i++)
            {
                s += "tag_id = " + _dataSet.Tables["tagok"].Rows[_dataGridViewTagok.SelectedRows[i].Index][0] + " OR ";
            }

            if (s.Length > 4)
            {
                s = s.Substring(0, s.Length - 4);
            }

            _bindingSourceKolcsonzesek.Filter = s;

            setCheckBox();
        }

        private void setCheckBox()
        {
            for (int i = 0; i < _dataGridViewKolcsonzesek.Rows.Count-1; i++)
            {            
                DateTime _time = Convert.ToDateTime(_dataGridViewKolcsonzesek.Rows[i].Cells[3].Value);
                if (_time < (DateTime.Now - new TimeSpan(30, 0, 0, 0)))
                {
                    _dataGridViewKolcsonzesek.Rows[i].Cells[5].Value = true;
                }
            }
        }

        private void _kolcsonzesButton_Click(object sender, EventArgs e)
        {
            bool l = true; 
            bool k = true;
            bool j = true;
            string error_msg="";            

            //ellenőrzöm, hogy kijelölünk-e egy tagot
            if (_dataGridViewTagok.SelectedRows.Count != 1)
            {
                error_msg += "A kölcsönéshez egy tagot jelölj ki!\n";
                l = false;
            }

            //ellenőrzöm, hogy kijelöltünk-e egy könyvet
            if (_dataGridViewKonyvek.SelectedRows.Count != 1)
            {
                error_msg += "A kölcsönéshez egy könyvet jelölj ki!\n";
                k = false;
            }

            //ellenőrzöm, hogy van-e szabad pédány a könyvből
            if (_dataGridViewKonyvek.SelectedRows.Count > 0)
            {
                if (Convert.ToInt32(_dataGridViewKonyvek.SelectedCells[5].FormattedValue) < 1)
                {
                    error_msg += "A könyv példányszám hiány miatt nem kikölcsönözhető!\n";
                    j = false;
                }
            }
                
            //ha hiba történ kiírom
            if (!k || !l  ||!j)
            {
                MessageBox.Show(error_msg, "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }
            
            //csak akkor végzem el a kölcsönzést, ha nem volt hiba
            if (k && l && j)
            {
                //kölcsönzés id, tag id, könyv id
                kolcson_id += 1;


                Validate();
                _bindingSourceKolcsonzesek.EndEdit();
                _bindingSourceKonyvek.EndEdit();
                

                DataRow row = _dataSet.Tables["kolcsonzesek"].NewRow();
                row["kolcson_id"] = kolcson_id;
                row["tag_id"] = Convert.ToInt32(_dataGridViewTagok.SelectedCells[0].FormattedValue);
                row["konyv_id"] = Convert.ToInt32(_dataGridViewKonyvek.SelectedCells[0].FormattedValue);
                row["kezdte"] = DateTime.Now;
            
                _dataSet.Tables["kolcsonzesek"].Rows.Add(row);

                //példányszám csökkentése
                int index_of_the_book=_dataGridViewKonyvek.SelectedRows[0].Index;
                _dataGridViewKonyvek[5, index_of_the_book].Value = Convert.ToInt32(_dataGridViewKonyvek[5, index_of_the_book].Value)-1;              
            } 
        }

        private void _visszaadasButton_Click(object sender, EventArgs e)
        {
            Validate();
            _bindingSourceKolcsonzesek.EndEdit();
            _bindingSourceKonyvek.EndEdit();

            //ellenőrzöm, hogy kijelöltünk-e egy kölcsönzést
            if (_dataGridViewKolcsonzesek.SelectedRows.Count != 1)
            {
                MessageBox.Show("A visszadáshoz egy bejegyzést jelölj ki!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }

            //könyv visszadása
            int current_row = _dataGridViewKolcsonzesek.SelectedRows[0].Index; //aktuális sor indexe
            _dataGridViewKolcsonzesek[4, current_row].Value = DateTime.Now;

            //példányszám növelése
            //megkeresem melyik sorban van a könyv amit vissza szeretnék adni
            int index_of_the_book = 0;
            for (int j = 0; j < _dataGridViewKonyvek.Rows.Count; j++)
            {
                if (Convert.ToInt32( _dataGridViewKonyvek.Rows[j].Cells[0].Value) ==Convert.ToInt32( _dataGridViewKolcsonzesek.SelectedCells[2].Value))
                {
                    index_of_the_book = j; 
                }
            }
            _dataGridViewKonyvek[5, index_of_the_book].Value = Convert.ToInt32(_dataGridViewKonyvek[5, index_of_the_book].Value) + 1;
        }

        private void _rogzitesButton_Click(object sender, EventArgs e)
        {

            Validate();
            _bindingSourceKolcsonzesek.EndEdit();
            _bindingSourceKonyvek.EndEdit();

            if (MessageBox.Show("Rögzítsem a változásokat?", "Rögzítés", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    DataSet changes = _dataSet.GetChanges(); // lekérjük a változtatásokat
                    if (changes == null)
                    {
                        return; // ha nem történt változás, visszatérünk
                    }

                    DataRow[] badRows = changes.Tables["kolcsonzesek"].GetErrors(); // lekérdezzük a hibás sorokat
                    DataRow[] badRows2 = changes.Tables["konyvek"].GetErrors();

                    if (badRows.Length == 0 && badRows2.Length == 0) // ha nem volt hiba
                    {
                        _adapKolcsonzesek.Update(_dataSet, "kolcsonzesek");
                        _adapKonyvek.Update(_dataSet, "konyvek");
                    }
                    else // ha voltak hibák
                    {
                        String errorText = String.Empty; // egy szövegben összegyűjtjük
                        foreach (DataRow row in badRows)
                        {
                            foreach (DataColumn col in row.GetColumnsInError()) // oszlopokban lévő hibák összegyűjtése
                            {
                                errorText += row.GetColumnError(col) + System.Environment.NewLine;
                            }
                        }

                        foreach (DataRow row in badRows2)
                        {
                            foreach (DataColumn col in row.GetColumnsInError()) // oszlopokban lévő hibák összegyűjtése
                            {
                                errorText += row.GetColumnError(col) + System.Environment.NewLine;
                            }
                        }
                        MessageBox.Show("A következő hibák léptek fel, kérem javítsa:" + System.Environment.NewLine + errorText, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Váratlan hiba lépett fel mentéskor, a változtatásokat visszavonom!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _dataSet.RejectChanges(); // változtatások visszavonása
                }
            }
        }             
    }
}
