namespace WindowsFormsApp1
{
    partial class FormSensorChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tCycle = new System.Windows.Forms.Timer(this.components);
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.serialNumberTextBox = new System.Windows.Forms.TextBox();
            this.serialNumberLabel = new System.Windows.Forms.Label();
            this.addInfoTextBox = new System.Windows.Forms.TextBox();
            this.addInfoLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.freonQuantityTextBox = new System.Windows.Forms.TextBox();
            this.freonQuantityLabel = new System.Windows.Forms.Label();
            this.externalTempCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.timePickerTo = new System.Windows.Forms.DateTimePicker();
            this.comboBoxFreonMark = new System.Windows.Forms.ComboBox();
            this.averageTemperatureLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisX2.IsLabelAutoFit = false;
            chartArea1.AxisX2.LabelStyle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisX2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY2.IsLabelAutoFit = false;
            chartArea1.AxisY2.LabelStyle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.LineWidth = 2;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            legend1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(13, 116);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(872, 337);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Горох";
            this.chart1.Titles.Add(title1);
            // 
            // tCycle
            // 
            this.tCycle.Enabled = true;
            this.tCycle.Interval = 5000;
            this.tCycle.Tick += new System.EventHandler(this.Refresher);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 15);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(131, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Наименование изделия:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(181, 12);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(704, 20);
            this.nameTextBox.TabIndex = 2;
            this.nameTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // serialNumberTextBox
            // 
            this.serialNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serialNumberTextBox.Location = new System.Drawing.Point(181, 38);
            this.serialNumberTextBox.Name = "serialNumberTextBox";
            this.serialNumberTextBox.Size = new System.Drawing.Size(704, 20);
            this.serialNumberTextBox.TabIndex = 4;
            this.serialNumberTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // serialNumberLabel
            // 
            this.serialNumberLabel.AutoSize = true;
            this.serialNumberLabel.Location = new System.Drawing.Point(12, 41);
            this.serialNumberLabel.Name = "serialNumberLabel";
            this.serialNumberLabel.Size = new System.Drawing.Size(96, 13);
            this.serialNumberLabel.TabIndex = 3;
            this.serialNumberLabel.Text = "Серийный номер:";
            // 
            // addInfoTextBox
            // 
            this.addInfoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addInfoTextBox.Location = new System.Drawing.Point(181, 90);
            this.addInfoTextBox.Name = "addInfoTextBox";
            this.addInfoTextBox.Size = new System.Drawing.Size(704, 20);
            this.addInfoTextBox.TabIndex = 8;
            this.addInfoTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // addInfoLabel
            // 
            this.addInfoLabel.AutoSize = true;
            this.addInfoLabel.Location = new System.Drawing.Point(12, 93);
            this.addInfoLabel.Name = "addInfoLabel";
            this.addInfoLabel.Size = new System.Drawing.Size(163, 13);
            this.addInfoLabel.TabIndex = 7;
            this.addInfoLabel.Text = "Дополнительная информация:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Марка фреона:";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(770, 492);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(115, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Сохранить обкатку";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(572, 492);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(192, 23);
            this.buttonStart.TabIndex = 10;
            this.buttonStart.Text = "Начать обкатку";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // freonQuantityTextBox
            // 
            this.freonQuantityTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.freonQuantityTextBox.Location = new System.Drawing.Point(553, 64);
            this.freonQuantityTextBox.Name = "freonQuantityTextBox";
            this.freonQuantityTextBox.Size = new System.Drawing.Size(332, 20);
            this.freonQuantityTextBox.TabIndex = 12;
            this.freonQuantityTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // freonQuantityLabel
            // 
            this.freonQuantityLabel.AutoSize = true;
            this.freonQuantityLabel.Location = new System.Drawing.Point(437, 67);
            this.freonQuantityLabel.Name = "freonQuantityLabel";
            this.freonQuantityLabel.Size = new System.Drawing.Size(110, 13);
            this.freonQuantityLabel.TabIndex = 11;
            this.freonQuantityLabel.Text = "Количество фреона:";
            // 
            // externalTempCheckBox
            // 
            this.externalTempCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.externalTempCheckBox.AutoSize = true;
            this.externalTempCheckBox.Checked = true;
            this.externalTempCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.externalTempCheckBox.Location = new System.Drawing.Point(15, 498);
            this.externalTempCheckBox.Name = "externalTempCheckBox";
            this.externalTempCheckBox.Size = new System.Drawing.Size(260, 17);
            this.externalTempCheckBox.TabIndex = 13;
            this.externalTempCheckBox.Text = "Показывать температуру окружающей среды";
            this.externalTempCheckBox.UseVisualStyleBackColor = true;
            this.externalTempCheckBox.CheckedChanged += new System.EventHandler(this.ExternalTempCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 467);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Время начала:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 467);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Время окончания:";
            // 
            // timePickerFrom
            // 
            this.timePickerFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timePickerFrom.CustomFormat = "HH:MM";
            this.timePickerFrom.Location = new System.Drawing.Point(100, 465);
            this.timePickerFrom.Name = "timePickerFrom";
            this.timePickerFrom.Size = new System.Drawing.Size(200, 20);
            this.timePickerFrom.TabIndex = 16;
            this.timePickerFrom.ValueChanged += new System.EventHandler(this.TimePicker_ValueChanged);
            // 
            // timePickerTo
            // 
            this.timePickerTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timePickerTo.CustomFormat = "HH:MM";
            this.timePickerTo.Location = new System.Drawing.Point(411, 465);
            this.timePickerTo.Name = "timePickerTo";
            this.timePickerTo.Size = new System.Drawing.Size(200, 20);
            this.timePickerTo.TabIndex = 17;
            this.timePickerTo.ValueChanged += new System.EventHandler(this.TimePicker_ValueChanged);
            // 
            // comboBoxFreonMark
            // 
            this.comboBoxFreonMark.FormattingEnabled = true;
            this.comboBoxFreonMark.Items.AddRange(new object[] {
            "R134a",
            "R404",
            "R290"});
            this.comboBoxFreonMark.Location = new System.Drawing.Point(181, 65);
            this.comboBoxFreonMark.Name = "comboBoxFreonMark";
            this.comboBoxFreonMark.Size = new System.Drawing.Size(250, 21);
            this.comboBoxFreonMark.TabIndex = 18;
            this.comboBoxFreonMark.SelectedIndexChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // averageTemperatureLabel
            // 
            this.averageTemperatureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.averageTemperatureLabel.AutoSize = true;
            this.averageTemperatureLabel.Location = new System.Drawing.Point(617, 467);
            this.averageTemperatureLabel.Name = "averageTemperatureLabel";
            this.averageTemperatureLabel.Size = new System.Drawing.Size(0, 13);
            this.averageTemperatureLabel.TabIndex = 19;
            this.averageTemperatureLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // FormSensorChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 527);
            this.Controls.Add(this.averageTemperatureLabel);
            this.Controls.Add(this.comboBoxFreonMark);
            this.Controls.Add(this.timePickerTo);
            this.Controls.Add(this.timePickerFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.externalTempCheckBox);
            this.Controls.Add(this.freonQuantityTextBox);
            this.Controls.Add(this.freonQuantityLabel);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.addInfoTextBox);
            this.Controls.Add(this.addInfoLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.serialNumberTextBox);
            this.Controls.Add(this.serialNumberLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.chart1);
            this.Name = "FormSensorChart";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer tCycle;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox serialNumberTextBox;
        private System.Windows.Forms.Label serialNumberLabel;
        private System.Windows.Forms.TextBox addInfoTextBox;
        private System.Windows.Forms.Label addInfoLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox freonQuantityTextBox;
        private System.Windows.Forms.Label freonQuantityLabel;
        private System.Windows.Forms.CheckBox externalTempCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker timePickerFrom;
        private System.Windows.Forms.DateTimePicker timePickerTo;
        private System.Windows.Forms.ComboBox comboBoxFreonMark;
        private System.Windows.Forms.Label averageTemperatureLabel;
    }
}