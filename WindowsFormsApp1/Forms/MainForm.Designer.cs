namespace ObkatkaCom
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьОбкаткуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьРезервнуюКопиюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSensorType = new System.Windows.Forms.ToolStripMenuItem();
            this.сопоставлениеIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.применитьIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portNumberList = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиМашинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.параметрыToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(864, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "MainMenu";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяToolStripMenuItem,
            this.открытьОбкаткуToolStripMenuItem,
            this.открытьРезервнуюКопиюToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.файлToolStripMenuItem.Text = "Обкатка";
            // 
            // новаяToolStripMenuItem
            // 
            this.новаяToolStripMenuItem.Name = "новаяToolStripMenuItem";
            this.новаяToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.новаяToolStripMenuItem.Text = "Начать работу";
            this.новаяToolStripMenuItem.Click += new System.EventHandler(this.NewRunInMenuItem_Click);
            // 
            // открытьОбкаткуToolStripMenuItem
            // 
            this.открытьОбкаткуToolStripMenuItem.Name = "открытьОбкаткуToolStripMenuItem";
            this.открытьОбкаткуToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.открытьОбкаткуToolStripMenuItem.Text = "Открыть обкатку (*.txt)";
            this.открытьОбкаткуToolStripMenuItem.Click += new System.EventHandler(this.OpenRunInToolStripMenuItem_Click);
            // 
            // открытьРезервнуюКопиюToolStripMenuItem
            // 
            this.открытьРезервнуюКопиюToolStripMenuItem.Name = "открытьРезервнуюКопиюToolStripMenuItem";
            this.открытьРезервнуюКопиюToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.открытьРезервнуюКопиюToolStripMenuItem.Text = "Открыть резервную копию";
            this.открытьРезервнуюКопиюToolStripMenuItem.Click += new System.EventHandler(this.OpenReserveCopyToolStripMenuItem_Click);
            // 
            // параметрыToolStripMenuItem
            // 
            this.параметрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиМашинToolStripMenuItem,
            this.toolStripMenuItemSensorType,
            this.сопоставлениеIDToolStripMenuItem,
            this.применитьIDToolStripMenuItem});
            this.параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            this.параметрыToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.параметрыToolStripMenuItem.Text = "Параметры";
            // 
            // toolStripMenuItemSensorType
            // 
            this.toolStripMenuItemSensorType.Name = "toolStripMenuItemSensorType";
            this.toolStripMenuItemSensorType.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemSensorType.Text = "Тип датчика";
            // 
            // сопоставлениеIDToolStripMenuItem
            // 
            this.сопоставлениеIDToolStripMenuItem.Name = "сопоставлениеIDToolStripMenuItem";
            this.сопоставлениеIDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.сопоставлениеIDToolStripMenuItem.Text = "Сопоставление ID";
            this.сопоставлениеIDToolStripMenuItem.Click += new System.EventHandler(this.MatchIDToolStripMenuItem_Click);
            // 
            // применитьIDToolStripMenuItem
            // 
            this.применитьIDToolStripMenuItem.Name = "применитьIDToolStripMenuItem";
            this.применитьIDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.применитьIDToolStripMenuItem.Text = "Применить ID";
            this.применитьIDToolStripMenuItem.Click += new System.EventHandler(this.RenameIDToolStripMenuItem_Click);
            // 
            // portNumberList
            // 
            this.portNumberList.Name = "portNumberList";
            this.portNumberList.Size = new System.Drawing.Size(180, 22);
            this.portNumberList.Text = "Номер порта";
            // 
            // настройкиМашинToolStripMenuItem
            // 
            this.настройкиМашинToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portNumberList});
            this.настройкиМашинToolStripMenuItem.Name = "настройкиМашинToolStripMenuItem";
            this.настройкиМашинToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.настройкиМашинToolStripMenuItem.Text = "Параметры порта";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 501);
            this.Controls.Add(this.MainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Линия обкатки";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новаяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьОбкаткуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSensorType;
        private System.Windows.Forms.ToolStripMenuItem открытьРезервнуюКопиюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сопоставлениеIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem применитьIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиМашинToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portNumberList;
    }
}

