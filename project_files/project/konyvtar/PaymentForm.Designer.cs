namespace konyvtar
{
    partial class PaymentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._dataGridViewTagok = new System.Windows.Forms.DataGridView();
            this._dataSet = new System.Data.DataSet();
            this._bindingSourceTagok = new System.Windows.Forms.BindingSource(this.components);
            this._bindingSourceStatuszok = new System.Windows.Forms.BindingSource(this.components);
            this._dataGridViewBefizetesek = new System.Windows.Forms.DataGridView();
            this._bindingSourceBefizetesek = new System.Windows.Forms.BindingSource(this.components);
            this._numericUpDown = new System.Windows.Forms.NumericUpDown();
            this._buttonRemoveFilter = new System.Windows.Forms.Button();
            this._buttonBefizetes = new System.Windows.Forms.Button();
            this._textBoxBefizetes = new System.Windows.Forms.TextBox();
            this._groupBox = new System.Windows.Forms.GroupBox();
            this._buttonSztorno = new System.Windows.Forms.Button();
            this._buttonRogzites = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewTagok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceTagok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceStatuszok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewBefizetesek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceBefizetesek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDown)).BeginInit();
            this._groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _dataGridViewTagok
            // 
            this._dataGridViewTagok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewTagok.Location = new System.Drawing.Point(12, 12);
            this._dataGridViewTagok.Name = "_dataGridViewTagok";
            this._dataGridViewTagok.Size = new System.Drawing.Size(711, 244);
            this._dataGridViewTagok.TabIndex = 0;
            // 
            // _dataSet
            // 
            this._dataSet.DataSetName = "NewDataSet";
            // 
            // _dataGridViewBefizetesek
            // 
            this._dataGridViewBefizetesek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewBefizetesek.Location = new System.Drawing.Point(12, 272);
            this._dataGridViewBefizetesek.Name = "_dataGridViewBefizetesek";
            this._dataGridViewBefizetesek.Size = new System.Drawing.Size(563, 180);
            this._dataGridViewBefizetesek.TabIndex = 1;
            // 
            // _numericUpDown
            // 
            this._numericUpDown.Location = new System.Drawing.Point(729, 35);
            this._numericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this._numericUpDown.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this._numericUpDown.Name = "_numericUpDown";
            this._numericUpDown.Size = new System.Drawing.Size(60, 20);
            this._numericUpDown.TabIndex = 2;
            this._numericUpDown.Value = new decimal(new int[] {
            2010,
            0,
            0,
            0});
            this._numericUpDown.ValueChanged += new System.EventHandler(this._numericUpDown_ValueChanged);
            // 
            // _buttonRemoveFilter
            // 
            this._buttonRemoveFilter.Location = new System.Drawing.Point(729, 61);
            this._buttonRemoveFilter.Name = "_buttonRemoveFilter";
            this._buttonRemoveFilter.Size = new System.Drawing.Size(85, 23);
            this._buttonRemoveFilter.TabIndex = 3;
            this._buttonRemoveFilter.Text = "Szűrő törlése";
            this._buttonRemoveFilter.UseVisualStyleBackColor = true;
            this._buttonRemoveFilter.Click += new System.EventHandler(this._buttonRemoveFilter_Click);
            // 
            // _buttonBefizetes
            // 
            this._buttonBefizetes.Location = new System.Drawing.Point(112, 18);
            this._buttonBefizetes.Name = "_buttonBefizetes";
            this._buttonBefizetes.Size = new System.Drawing.Size(75, 23);
            this._buttonBefizetes.TabIndex = 4;
            this._buttonBefizetes.Text = "Befizetés";
            this._buttonBefizetes.UseVisualStyleBackColor = true;
            this._buttonBefizetes.Click += new System.EventHandler(this._buttonBefizetes_Click);
            // 
            // _textBoxBefizetes
            // 
            this._textBoxBefizetes.Location = new System.Drawing.Point(6, 20);
            this._textBoxBefizetes.Name = "_textBoxBefizetes";
            this._textBoxBefizetes.Size = new System.Drawing.Size(100, 20);
            this._textBoxBefizetes.TabIndex = 5;
            // 
            // _groupBox
            // 
            this._groupBox.Controls.Add(this._buttonRogzites);
            this._groupBox.Controls.Add(this._buttonSztorno);
            this._groupBox.Controls.Add(this._buttonBefizetes);
            this._groupBox.Controls.Add(this._textBoxBefizetes);
            this._groupBox.Location = new System.Drawing.Point(589, 272);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Size = new System.Drawing.Size(200, 108);
            this._groupBox.TabIndex = 6;
            this._groupBox.TabStop = false;
            this._groupBox.Text = "Befizetések";
            // 
            // _buttonSztorno
            // 
            this._buttonSztorno.Location = new System.Drawing.Point(112, 47);
            this._buttonSztorno.Name = "_buttonSztorno";
            this._buttonSztorno.Size = new System.Drawing.Size(75, 23);
            this._buttonSztorno.TabIndex = 6;
            this._buttonSztorno.Text = "Sztornó";
            this._buttonSztorno.UseVisualStyleBackColor = true;
            this._buttonSztorno.Click += new System.EventHandler(this._buttonSztorno_Click);
            // 
            // _buttonRogzites
            // 
            this._buttonRogzites.Location = new System.Drawing.Point(112, 76);
            this._buttonRogzites.Name = "_buttonRogzites";
            this._buttonRogzites.Size = new System.Drawing.Size(75, 23);
            this._buttonRogzites.TabIndex = 7;
            this._buttonRogzites.Text = "Rögzítés";
            this._buttonRogzites.UseVisualStyleBackColor = true;
            this._buttonRogzites.Click += new System.EventHandler(this._buttonRogzites_Click);
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 464);
            this.Controls.Add(this._groupBox);
            this.Controls.Add(this._buttonRemoveFilter);
            this.Controls.Add(this._numericUpDown);
            this.Controls.Add(this._dataGridViewBefizetesek);
            this.Controls.Add(this._dataGridViewTagok);
            this.Name = "PaymentForm";
            this.Text = "Tagdíj fizetés";
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewTagok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceTagok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceStatuszok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewBefizetesek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceBefizetesek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDown)).EndInit();
            this._groupBox.ResumeLayout(false);
            this._groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _dataGridViewTagok;
        private System.Data.DataSet _dataSet;
        private System.Windows.Forms.BindingSource _bindingSourceTagok;
        private System.Windows.Forms.BindingSource _bindingSourceStatuszok;
        private System.Windows.Forms.DataGridView _dataGridViewBefizetesek;
        private System.Windows.Forms.BindingSource _bindingSourceBefizetesek;
        private System.Windows.Forms.NumericUpDown _numericUpDown;
        private System.Windows.Forms.Button _buttonRemoveFilter;
        private System.Windows.Forms.Button _buttonBefizetes;
        private System.Windows.Forms.TextBox _textBoxBefizetes;
        private System.Windows.Forms.GroupBox _groupBox;
        private System.Windows.Forms.Button _buttonRogzites;
        private System.Windows.Forms.Button _buttonSztorno;
    }
}