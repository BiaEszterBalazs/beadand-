namespace konyvtar
{
    partial class LendingFrom
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
            this._dataGridViewKolcsonzesek = new System.Windows.Forms.DataGridView();
            this._dataSet = new System.Data.DataSet();
            this._bindingSourceKolcsonzesek = new System.Windows.Forms.BindingSource(this.components);
            this._dataGridViewTagok = new System.Windows.Forms.DataGridView();
            this._bindingSourceTagok = new System.Windows.Forms.BindingSource(this.components);
            this._dataGridViewKonyvek = new System.Windows.Forms.DataGridView();
            this._bindingSourceKonyvek = new System.Windows.Forms.BindingSource(this.components);
            this._kolcsonzesButton = new System.Windows.Forms.Button();
            this._visszaadasButton = new System.Windows.Forms.Button();
            this._rogzitesButton = new System.Windows.Forms.Button();
            this._groupBoxKolcsonzes = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewKolcsonzesek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceKolcsonzesek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewTagok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceTagok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewKonyvek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceKonyvek)).BeginInit();
            this._groupBoxKolcsonzes.SuspendLayout();
            this.SuspendLayout();
            // 
            // _dataGridViewKolcsonzesek
            // 
            this._dataGridViewKolcsonzesek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewKolcsonzesek.Location = new System.Drawing.Point(5, 209);
            this._dataGridViewKolcsonzesek.Name = "_dataGridViewKolcsonzesek";
            this._dataGridViewKolcsonzesek.Size = new System.Drawing.Size(676, 172);
            this._dataGridViewKolcsonzesek.TabIndex = 0;
            // 
            // _dataSet
            // 
            this._dataSet.DataSetName = "NewDataSet";
            // 
            // _dataGridViewTagok
            // 
            this._dataGridViewTagok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewTagok.Location = new System.Drawing.Point(5, 9);
            this._dataGridViewTagok.Name = "_dataGridViewTagok";
            this._dataGridViewTagok.Size = new System.Drawing.Size(676, 163);
            this._dataGridViewTagok.TabIndex = 1;
            // 
            // _dataGridViewKonyvek
            // 
            this._dataGridViewKonyvek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewKonyvek.Location = new System.Drawing.Point(687, 9);
            this._dataGridViewKonyvek.Name = "_dataGridViewKonyvek";
            this._dataGridViewKonyvek.Size = new System.Drawing.Size(572, 163);
            this._dataGridViewKonyvek.TabIndex = 2;
            // 
            // _kolcsonzesButton
            // 
            this._kolcsonzesButton.Location = new System.Drawing.Point(6, 19);
            this._kolcsonzesButton.Name = "_kolcsonzesButton";
            this._kolcsonzesButton.Size = new System.Drawing.Size(75, 23);
            this._kolcsonzesButton.TabIndex = 3;
            this._kolcsonzesButton.Text = "Kölcsönzés";
            this._kolcsonzesButton.UseVisualStyleBackColor = true;
            this._kolcsonzesButton.Click += new System.EventHandler(this._kolcsonzesButton_Click);
            // 
            // _visszaadasButton
            // 
            this._visszaadasButton.Location = new System.Drawing.Point(6, 48);
            this._visszaadasButton.Name = "_visszaadasButton";
            this._visszaadasButton.Size = new System.Drawing.Size(75, 23);
            this._visszaadasButton.TabIndex = 4;
            this._visszaadasButton.Text = "Visszaadás";
            this._visszaadasButton.UseVisualStyleBackColor = true;
            this._visszaadasButton.Click += new System.EventHandler(this._visszaadasButton_Click);
            // 
            // _rogzitesButton
            // 
            this._rogzitesButton.Location = new System.Drawing.Point(6, 77);
            this._rogzitesButton.Name = "_rogzitesButton";
            this._rogzitesButton.Size = new System.Drawing.Size(75, 23);
            this._rogzitesButton.TabIndex = 5;
            this._rogzitesButton.Text = "Rögzítés";
            this._rogzitesButton.UseVisualStyleBackColor = true;
            this._rogzitesButton.Click += new System.EventHandler(this._rogzitesButton_Click);
            // 
            // _groupBoxKolcsonzes
            // 
            this._groupBoxKolcsonzes.Controls.Add(this._kolcsonzesButton);
            this._groupBoxKolcsonzes.Controls.Add(this._rogzitesButton);
            this._groupBoxKolcsonzes.Controls.Add(this._visszaadasButton);
            this._groupBoxKolcsonzes.Location = new System.Drawing.Point(698, 209);
            this._groupBoxKolcsonzes.Name = "_groupBoxKolcsonzes";
            this._groupBoxKolcsonzes.Size = new System.Drawing.Size(94, 112);
            this._groupBoxKolcsonzes.TabIndex = 6;
            this._groupBoxKolcsonzes.TabStop = false;
            this._groupBoxKolcsonzes.Text = "Kölcsönzés";
            // 
            // LendingFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 556);
            this.Controls.Add(this._groupBoxKolcsonzes);
            this.Controls.Add(this._dataGridViewKonyvek);
            this.Controls.Add(this._dataGridViewTagok);
            this.Controls.Add(this._dataGridViewKolcsonzesek);
            this.Name = "LendingFrom";
            this.Text = "Kölcsönzés";
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewKolcsonzesek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceKolcsonzesek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewTagok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceTagok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewKonyvek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSourceKonyvek)).EndInit();
            this._groupBoxKolcsonzes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _dataGridViewKolcsonzesek;
        private System.Data.DataSet _dataSet;
        private System.Windows.Forms.BindingSource _bindingSourceKolcsonzesek;
        private System.Windows.Forms.DataGridView _dataGridViewTagok;
        private System.Windows.Forms.BindingSource _bindingSourceTagok;
        private System.Windows.Forms.DataGridView _dataGridViewKonyvek;
        private System.Windows.Forms.BindingSource _bindingSourceKonyvek;
        private System.Windows.Forms.Button _kolcsonzesButton;
        private System.Windows.Forms.Button _visszaadasButton;
        private System.Windows.Forms.Button _rogzitesButton;
        private System.Windows.Forms.GroupBox _groupBoxKolcsonzes;
    }
}