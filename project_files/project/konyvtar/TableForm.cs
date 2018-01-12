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
    public partial class TableForm : Form
    {
        private SQLiteConnection _con;
        private string _tn;
        private SQLiteCommand sql_cmd;

        private void SetConnection()
        {
            _con = new SQLiteConnection("Data Source=Konyvtar.db;Version=3;");
        }

        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            _con.Open();
            sql_cmd = _con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            _con.Close();
        }

        //konstruktorban megadjuk az sql kapcsolatot és a tábla nevét
        public TableForm(SQLiteConnection c, string s) 
        {
            InitializeComponent();
            Text = s;
            _con = c;
            _tn = s;
            _dataGridView.AutoGenerateColumns = false;
            _dataGridView.AllowUserToDeleteRows = true;
            LoadData();
                        
        }

        //adatok betöltése a fizikai adatbázisból a logikai adatbázisba
        private void LoadData()
        {
            _dataSet = new DataSet(_tn);
            _bindingSource.DataSource = _dataSet.Tables[_tn];
            FormatTables(_dataSet);
        }

        //táblák kézzel történő formázása
        private void FormatTables(DataSet _dataset)
        {
            switch (_dataset.Tables[_tn].TableName)
            {
                case "befizetesek":
                {
                    CreateTagokAdapterForForeignKeys(_dataset);
                    
                    //azonosító automatikus növelése
                    autoIdIncrease(_dataset, "bizonylatszam", "befizetesek");
                    
                    //tábla fejlécének formázása
                    DataGridViewColumn[] columns = new DataGridViewColumn[4];
                    
                    columns[0] = new DataGridViewTextBoxColumn();
                    columns[0].HeaderText = "Bizonylatszám";
                    columns[0].DataPropertyName = "bizonylatszam";
                    columns[0].Visible = false;
                    
                    //ezt kellene idegenkulcsként megjeleníteni
                    columns[1] = new DataGridViewComboBoxColumn();
                    columns[1].HeaderText = "Tag neve";
                    columns[1].DataPropertyName = "tag_id";                        

                    ((DataGridViewComboBoxColumn)columns[1]).ValueMember = "tag_id";
                    ((DataGridViewComboBoxColumn)columns[1]).DisplayMember = "nev";
                                                
                    columns[2] = new DataGridViewTextBoxColumn();
                    columns[2].HeaderText = "Összeg";
                    columns[2].DataPropertyName = "osszeg";
                    
                    columns[3] = new DataGridViewTextBoxColumn();
                    columns[3].HeaderText = "Dátum";
                    columns[3].DataPropertyName = "datum";
                    
                    addColumnsToDataGridView(columns);
                    break;
                }
                case "kolcsonzesek":
                {
                    CreateTagokAdapterForForeignKeys(_dataset);                  
                    //azonosító automatikus növelése
                    autoIdIncrease(_dataset, "kolcson_id", "kolcsonzesek");

                    //tábla fejlécének formázása
                    DataGridViewColumn[] columns = new DataGridViewColumn[5];
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

                    addColumnsToDataGridView(columns);
                    break;
                }
                case "konyvek":
                {
                    //azonosító automatikus növelése
                    autoIdIncrease(_dataset, "konyv_id", "konyvek");
                    
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

                    addColumnsToDataGridView(columns);
                    break;
                }
                case "olvasasok":
                {
                    //adatpter az indegen kulcshot
                    CreateTagokAdapterForForeignKeys(_dataset);

                    //azonosító automatikus növelése
                    autoIdIncrease(_dataset, "olvas_id", "olvasasok");

                    DataGridViewColumn[] columns = new DataGridViewColumn[4];
                    columns[0] = new DataGridViewTextBoxColumn();
                    columns[0].HeaderText = "Olvasás ID";
                    columns[0].DataPropertyName = "olvas_id";
                    columns[0].Visible = false;
                                                
                    columns[1] = new DataGridViewComboBoxColumn();
                    columns[1].HeaderText = "Tag neve";
                    columns[1].DataPropertyName = "tag_id";
                    //oszlop adatainak feltöltése másik táblából
                    ((DataGridViewComboBoxColumn)columns[1]).DataSource = _bindingSourceTagok;
                    ((DataGridViewComboBoxColumn)columns[1]).ValueMember = "tag_id";
                    ((DataGridViewComboBoxColumn)columns[1]).DisplayMember = "nev";

                    columns[2] = new DataGridViewTextBoxColumn();
                    columns[2].HeaderText = "Belépés";
                    columns[2].DataPropertyName = "belepes";
                    
                    columns[3] = new DataGridViewTextBoxColumn();
                    columns[3].HeaderText = "Kilépés";
                    columns[3].DataPropertyName = "kilepes";

                    addColumnsToDataGridView(columns);
                    break;
                }
                case "statuszok":
                {
                    //azonosító automatikus növelése
                    autoIdIncrease(_dataset, "statusz_id", "statuszok");
                    
                    //tábla fejlécének formázása
                    DataGridViewColumn[] columns = new DataGridViewColumn[3];

                    columns[0] = new DataGridViewTextBoxColumn();
                    columns[0].HeaderText = "Státusz ID";
                    columns[0].DataPropertyName = "statusz_id";
                    columns[0].Visible = false;
                    
                    
                    columns[1] = new DataGridViewTextBoxColumn();
                    columns[1].HeaderText = "Státusz";
                    columns[1].DataPropertyName = "statusz";
                    
                    columns[2] = new DataGridViewTextBoxColumn();
                    columns[2].HeaderText = "Éves Tagdíj";
                    columns[2].DataPropertyName = "tagdij";
                    
                    addColumnsToDataGridView(columns);
                    break;
                }
                case "tagok":
                {
                    //azonosító automatikus növelése
                    autoIdIncrease(_dataset, "tag_id", "tagok");

                    _adapStatuszok = new SQLiteDataAdapter("SELECT * FROM statuszok", _con);
                    _adapStatuszok.Fill(_dataset, "statuszok");
                    _bindingSourceStatuszok = new BindingSource();
                    _bindingSourceStatuszok.DataSource = _dataset.Tables["statuszok"];                      

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

                    addColumnsToDataGridView(columns);
                    break;
                }
                default:
                    break;
            }
        }

        private void CreateTagokAdapterForForeignKeys(DataSet _dataset)
        {
            throw new NotImplementedException();
        }

        //belső azonosítók automatikus növelése
        private void autoIdIncrease(DataSet _dataset, String _id, String _table)
        {
            _dataset.Tables[0].Columns[0].AutoIncrement = true;
            SQLiteCommand command = _con.CreateCommand();
            command.CommandText = "SELECT MAX(" + _id + ") FROM " + _table;
            _dataset.Tables[0].Columns[0].AutoIncrementSeed = Convert.ToInt32(command.ExecuteScalar()) + 1;
            _dataset.Tables[0].Columns[0].AutoIncrementStep = 1;
        }

        //sorok hozzáadás az adatrácshoz
        private void addColumnsToDataGridView(DataGridViewColumn[] columns)
        {
            foreach (DataGridViewColumn col in columns)
            {
                _dataGridView.Columns.Add(col);
            }
        }

        //navigátor mentés gombjára kattintás
        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            _bindingSource.EndEdit();
            try
            {
                _adap.Update(_dataSet, _tn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nAz adatok mentése sikertelen!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //navigátor törlés gombjára kattintás
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            //object o = new object();
            DataGridViewRowCancelEventArgs a = new DataGridViewRowCancelEventArgs(_dataGridView.CurrentRow);
                        
            //olyan tag nem törölhető akinél van könyv,tartozik tagdíjjal és az olvasótermeben ül
            //olyan státusz törölhető ami nincs hozzárendelve egy taghoz sem
            //csak olyan könyv törölhető ami nincs kikölcsönözve
            _dataGridView_UserDeletingRow(sender, a);           
        }

        private void _dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs a)
        {
            throw new NotImplementedException();
        }
    }
}