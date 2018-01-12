namespace konyvtar
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this._tablesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.karbantartásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.könyvekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.státuszokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._tablesMenu_kolcsonzesek = new System.Windows.Forms.ToolStripMenuItem();
            this._tablesMenu_olvasasok = new System.Windows.Forms.ToolStripMenuItem();
            this._tablesMenu_befizetesek = new System.Windows.Forms.ToolStripMenuItem();
            this._kilepes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tablesMenu,
            this._kilepes});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(771, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // _tablesMenu
            // 
            this._tablesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.karbantartásToolStripMenuItem,
            this._tablesMenu_kolcsonzesek,
            this._tablesMenu_olvasasok,
            this._tablesMenu_befizetesek});
            this._tablesMenu.Name = "_tablesMenu";
            this._tablesMenu.Size = new System.Drawing.Size(45, 20);
            this._tablesMenu.Text = "Menü";
            // 
            // karbantartásToolStripMenuItem
            // 
            this.karbantartásToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.könyvekToolStripMenuItem,
            this.tagokToolStripMenuItem,
            this.státuszokToolStripMenuItem});
            this.karbantartásToolStripMenuItem.Name = "karbantartásToolStripMenuItem";
            this.karbantartásToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.karbantartásToolStripMenuItem.Text = "Karbantartás";
            // 
            // könyvekToolStripMenuItem
            // 
            this.könyvekToolStripMenuItem.Name = "könyvekToolStripMenuItem";
            this.könyvekToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.könyvekToolStripMenuItem.Text = "Könyvek";
            this.könyvekToolStripMenuItem.Click += new System.EventHandler(this.MenuTable_Konyvek_Clicked);
            // 
            // tagokToolStripMenuItem
            // 
            this.tagokToolStripMenuItem.Name = "tagokToolStripMenuItem";
            this.tagokToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.tagokToolStripMenuItem.Text = "Tagok";
            this.tagokToolStripMenuItem.Click += new System.EventHandler(this.MenuTable_tagok_Clicked);
            // 
            // státuszokToolStripMenuItem
            // 
            this.státuszokToolStripMenuItem.Name = "státuszokToolStripMenuItem";
            this.státuszokToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.státuszokToolStripMenuItem.Text = "Státuszok";
            this.státuszokToolStripMenuItem.Click += new System.EventHandler(this.MenuTable_statuszok_Clicked);
            // 
            // _tablesMenu_kolcsonzesek
            // 
            this._tablesMenu_kolcsonzesek.Name = "_tablesMenu_kolcsonzesek";
            this._tablesMenu_kolcsonzesek.Size = new System.Drawing.Size(148, 22);
            this._tablesMenu_kolcsonzesek.Text = "Kölcsönzések";
            this._tablesMenu_kolcsonzesek.Click += new System.EventHandler(this.MenuTable_kolcsonzesek_Clicked);
            // 
            // _tablesMenu_olvasasok
            // 
            this._tablesMenu_olvasasok.Name = "_tablesMenu_olvasasok";
            this._tablesMenu_olvasasok.Size = new System.Drawing.Size(148, 22);
            this._tablesMenu_olvasasok.Text = "Olvasások";
            this._tablesMenu_olvasasok.Click += new System.EventHandler(this.MenuTable_olvasasok_Clicked);
            // 
            // _tablesMenu_befizetesek
            // 
            this._tablesMenu_befizetesek.Name = "_tablesMenu_befizetesek";
            this._tablesMenu_befizetesek.Size = new System.Drawing.Size(148, 22);
            this._tablesMenu_befizetesek.Text = "Befizetések";
            this._tablesMenu_befizetesek.Click += new System.EventHandler(this.MenuTable_befizetesek_Clicked);
            // 
            // _kilepes
            // 
            this._kilepes.Name = "_kilepes";
            this._kilepes.Size = new System.Drawing.Size(52, 20);
            this._kilepes.Text = "Kilépés";
            this._kilepes.Click += new System.EventHandler(this.MenuTable_kilepes_Clicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 437);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Könyvtári nyivántartás";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _tablesMenu;
        private System.Windows.Forms.ToolStripMenuItem _tablesMenu_befizetesek;
        private System.Windows.Forms.ToolStripMenuItem _tablesMenu_kolcsonzesek;
        private System.Windows.Forms.ToolStripMenuItem _tablesMenu_olvasasok;
        private System.Windows.Forms.ToolStripMenuItem _kilepes;
        private System.Windows.Forms.ToolStripMenuItem karbantartásToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem könyvekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagokToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem státuszokToolStripMenuItem;
    }
}

