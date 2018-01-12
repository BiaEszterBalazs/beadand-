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

        private SQLiteDataAdapter _adap;
        private SQLiteDataAdapter _adapTagok;
        private SQLiteDataAdapter _adapKonyvek;
        private SQLiteDataAdapter _adapKolcsonzesek;
        private SQLiteDataAdapter _adapOlvasasok;
        private SQLiteDataAdapter _adapStatuszok;
        private SQLiteDataAdapter _adapBefizetesek;

        private BindingSource _bindingSourceTagok;
        private BindingSource _bindingSourceKonyvek;
        private BindingSource _bindingSourceKolcsonzesek;
        private BindingSource _bindingSourceOlvasasok;
        private BindingSource _bindingSourceStatuszok;
        private BindingSource _bindingSourceBefizetesek;

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

            // hibakezelés cellára:
            _dataGridView.CellValidating += new DataGridViewCellValidatingEventHandler(DataGridViewCellValidating);
            // hibakezelés sorra:
            _dataGridView.RowValidating += new DataGridViewCellCancelEventHandler(DataGridView_RowValidating);
            //törés ellenőrzés
            _dataGridView.UserDeletingRow += new DataGridViewRowCancelEventHandler(_dataGridView_UserDeletingRow);

        }

        //adatok betöltése a fizikai adatbázisból a logikai adatbázisba
        private void LoadData()
        {
            _adap = new SQLiteDataAdapter("SELECT * FROM " + _tn, _con);
            SQLiteCommandBuilder builder = new SQLiteCommandBuilder(_adap);
            _adap.InsertCommand = builder.GetInsertCommand();
            _adap.DeleteCommand = builder.GetDeleteCommand();
            _adap.UpdateCommand = builder.GetUpdateCommand();

            _dataSet = new DataSet(_tn);
            _adap.Fill(_dataSet, _tn);
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
                        ((DataGridViewComboBoxColumn)columns[1]).DataSource = _bindingSourceTagok;
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
                        _adapKonyvek = new SQLiteDataAdapter("SELECT * FROM konyvek", _con);
                        _adapKonyvek.Fill(_dataset, "konyvek");
                        _bindingSourceKonyvek = new BindingSource();
                        _bindingSourceKonyvek.DataSource = _dataset.Tables["konyvek"];
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

        //hibakezelés cellára
        private void DataGridViewCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (_dataSet.Tables[_tn].TableName)
            {
                case "befizetesek":
                    {
                        switch (e.ColumnIndex)
                        {
                            case 2: //összeg oszlop
                                {
                                    CheckNumber(e);
                                    break;
                                }
                            case 3: //dátum oszlop
                                {
                                    CheckDateFormat(e);
                                    break;
                                }
                            default: break;
                        }
                        break;
                    }
                case "kolcsonzesek":
                    {
                        switch (e.ColumnIndex)
                        {
                            case 3://kedted oszlop
                                {
                                    CheckDateFormat(e);
                                    break;
                                }
                            case 4://vissza oszlop
                                {
                                    CheckDateFormat(e);
                                    break;
                                }
                            default: break;
                        }
                        break;
                    }
                case "konyvek":
                    {
                        switch (e.ColumnIndex)
                        {
                            case 1://azonosító oszlop
                                {
                                    CheckIdentifer(e, 12);
                                    break;
                                }
                            case 4:// példányszám
                                {
                                    CheckPositive(e);
                                    if (Convert.ToInt32(e.FormattedValue) < (Convert.ToInt32(_dataSet.Tables[_tn].Rows[e.RowIndex][4])))
                                    {
                                        MessageBox.Show("hiba, csak kissebb lehet", "hiba");
                                        e.Cancel = true;
                                    }


                                    break;
                                }
                            case 5://szabad példányszám
                                {
                                    CheckPositive(e);
                                    break;
                                }

                            default:
                                break;
                        }
                        break;
                    }
                case "olvasasok":
                    {
                        switch (e.ColumnIndex)
                        {
                            case 2:
                                {
                                    CheckDateFormat(e);
                                    break;
                                }
                            case 3:
                                {
                                    CheckDateFormat(e);
                                    break;
                                }
                            default: break;
                        }
                        break;
                    }
                case "statuszok":
                    {
                        switch (e.ColumnIndex)
                        {
                            case 1:
                                {
                                    break;
                                }
                            case 2:
                                {
                                    CheckPositive(e);
                                    break;
                                }
                            default: break;
                        }
                        break;
                    }
                case "tagok":
                    {
                        switch (e.ColumnIndex)
                        {
                            case 3:
                                {
                                    CheckIdentifer(e, 6);
                                    break;
                                }
                            case 4:
                                {
                                    CheckPositive(e);
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        //hibakezelés sorra
        private void DataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                // megpróbáljuk menteni az adatokat
                _bindingSource.EndEdit();
            }
            catch (NoNullAllowedException ex) // valahova nem írtunk adatot, ahova kellett volna
            {
                MessageBox.Show(ex.Message.Substring(0, 15) + " oszlop kitöltése kötelező!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true; // akkor ne mentse az adatokat
            }
        }

        //ellenőrzi, hogy a praméter pozitív szám-e
        private void CheckPositive(DataGridViewCellValidatingEventArgs e)
        {
            Int32 n;
            if (!Int32.TryParse(e.FormattedValue.ToString(), out n) || n <= 0) // ha nem pozitív számot írtunk be
            {
                MessageBox.Show("Hiba! A mezőben csak pozitív szám szerepelhet!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true; //          
            }
        }

        //ellenőrzi, hogy a paraméter szám-e
        private void CheckNumber(DataGridViewCellValidatingEventArgs e)
        {
            Int32 n;
            if (!Int32.TryParse(e.FormattedValue.ToString(), out n)) // ha nem pozitív számot írtunk be
            {
                MessageBox.Show("Hiba! A mezőben csak  szám szerepelhet!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true; //          
            }
        }

        //ellenőrzi, hogy a paraméter azonosító-e
        private void CheckIdentifer(DataGridViewCellValidatingEventArgs e, int length)
        {
            if (e.FormattedValue.ToString().Length != length)
            {
                MessageBox.Show("Hiba! Az azonosítónak " + length.ToString() + " karakter hosszúnak kell lennie!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        //adapter létrehozása a tagok táblára
        private void CreateTagokAdapterForForeignKeys(DataSet _dataset)
        {
            _adapTagok = new SQLiteDataAdapter("SELECT * FROM tagok", _con);
            _adapTagok.Fill(_dataset, "tagok");
            _bindingSourceTagok = new BindingSource();
            _bindingSourceTagok.DataSource = _dataset.Tables["tagok"];
        }

        //ellenőrzi, hogy a paraméter megfelelő dátumtípus-e
        private void CheckDateFormat(DataGridViewCellValidatingEventArgs e)
        {
            System.IFormatProvider format = new System.Globalization.CultureInfo("hu-HU", true);
            DateTime outDate;
            bool isDate = DateTime.TryParseExact(e.FormattedValue.ToString(), "yyyy.MM.dd. HH:mm", format, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out outDate);

            if (!isDate)
            {
                MessageBox.Show("Hiba! Helytelen dátum formátum!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        //törlés ellenőrzése
        private void _dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //olyan tag nem törölhető akinél van könyv,tartozik tagdíjjal és az olvasótermeben ül
            //olyan státusz törölhető ami nincs hozzárendelve egy taghoz sem
            //csak olyan könyv törölhető ami nincs kikölcsönözve 

            if (!e.Row.IsNewRow)
            {
                DialogResult res = MessageBox.Show("Biztos vagy benne, hogy törölni szeretnéd ezt a sort?", "Törlés megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            #region switch
            switch (_dataSet.Tables[_tn].TableName)
            {
                case "tagok":
                    {
                        //csak olyan könyvtári tag törölhető, akinél nincs kikölcsönzött könyv
                        //itt az kell ellenőrizni, hogy a tagnál van e könyv tehát az id-ja szerepel a kölcsönzések táblában
                        _adapKolcsonzesek = new SQLiteDataAdapter("SELECT * FROM kolcsonzesek", _con);
                        _adapKolcsonzesek.Fill(_dataSet, "kolcsonzesek");
                        _bindingSourceKolcsonzesek = new BindingSource()
                        {
                            DataSource = _dataSet.Tables["kolcsonzesek"]
                        };
                        DataTable dt = new DataTable();
                        dt = _dataSet.Tables["kolcsonzesek"];

                        String value = e.Row.Cells[0].FormattedValue.ToString();
                        bool found = dt.Select("tag_id=" + value).Length > 0;
                        if (found)
                        {
                            MessageBox.Show("Hiba! A tag nem törölhető, mert még van nála könyv!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }

                        //csak olyan könyvtári tag törölhető, aki nem tartozik tagdíjjal
                        _adapBefizetesek = new SQLiteDataAdapter("SELECT * FROM befizetesek", _con);
                        _adapBefizetesek.Fill(_dataSet, "befizetesek");
                        _bindingSourceBefizetesek = new BindingSource();
                        _bindingSourceBefizetesek.DataSource = _dataSet.Tables["befizetesek"];
                        DataTable dt3 = new DataTable();
                        dt3 = _dataSet.Tables["befizetesek"];


                        bool k = dt3.Select("tag_id=" + value + "AND osszeg < 0").Length > 0;

                        if (k)
                        {
                            MessageBox.Show("Hiba! A tag nem törölhető, mert tartozása van!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        //csak olyan könyvtári tag törölhető, aki nem ül az olvasó teremben
                        _adapOlvasasok = new SQLiteDataAdapter("SELECT * FROM olvasasok", _con);
                        _adapOlvasasok.Fill(_dataSet, "olvasasok");
                        _bindingSourceOlvasasok = new BindingSource();
                        _bindingSourceOlvasasok.DataSource = _dataSet.Tables["olvasasok"];
                        DataTable dt2 = new DataTable();
                        dt2 = _dataSet.Tables["olvasasok"];
                        bool l = dt2.Select("tag_id=" + value).Length > 0;
                        if (l)
                        {
                            MessageBox.Show("Hiba! A tag nem törölhető, mert az olvasóteremben ül!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }

                        break;
                    }
                case "statuszok":
                    {
                        //csak olyan státusz törölhető, amely egyik könyvtári taghoz sincs hozzárendelve

                        String value = e.Row.Cells[0].FormattedValue.ToString();
                        DataTable dt = new DataTable();
                        dt = _dataSet.Tables["statuszok"];
                        bool l = dt.Select("statusz_id=" + value).Length > 0;

                        if (l)
                        {
                            MessageBox.Show("Hiba! A státusz nem törölhető, mert hozzá van rendelve egy taghoz!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }


                        break;
                    }
                case "konyvek":
                    {
                        //csak olyan könyv törölhető, amely nincs kikölcsönözve
                        _adapKolcsonzesek = new SQLiteDataAdapter("SELECT * FROM kolcsonzesek", _con);
                        _adapKolcsonzesek.Fill(_dataSet, "kolcsonzesek");
                        _bindingSourceKolcsonzesek = new BindingSource();
                        _bindingSourceKolcsonzesek.DataSource = _dataSet.Tables["kolcsonzesek"];
                        String value = e.Row.Cells[0].FormattedValue.ToString();
                        DataTable dt = new DataTable();
                        dt = _dataSet.Tables["kolcsonzesek"];
                        bool l = dt.Select("konyv_id=" + value).Length > 0;
                        if (l)
                        {
                            MessageBox.Show("Hiba! A könyv nem törölhető, mert ki van kölcsönözve!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        break;
                    }
                default:
                    break;
            }
            #endregion

        }
    }
}