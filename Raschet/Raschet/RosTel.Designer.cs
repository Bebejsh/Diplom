namespace Raschet
{
    partial class RosTel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RosTel));
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus3 = new System.Windows.Forms.Label();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.btnSaveExel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnBrowsePdf = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panelFileSelector = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Имя = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Выбор = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.panelFileSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Путь к Exel файлу:";
            // 
            // lblStatus3
            // 
            this.lblStatus3.AutoSize = true;
            this.lblStatus3.Location = new System.Drawing.Point(184, 155);
            this.lblStatus3.Name = "lblStatus3";
            this.lblStatus3.Size = new System.Drawing.Size(42, 13);
            this.lblStatus3.TabIndex = 15;
            this.lblStatus3.Text = "Готово";
            // 
            // lblStatus2
            // 
            this.lblStatus2.AutoSize = true;
            this.lblStatus2.Location = new System.Drawing.Point(184, 43);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(42, 13);
            this.lblStatus2.TabIndex = 14;
            this.lblStatus2.Text = "Готово";
            // 
            // btnSaveExel
            // 
            this.btnSaveExel.Location = new System.Drawing.Point(13, 149);
            this.btnSaveExel.Name = "btnSaveExel";
            this.btnSaveExel.Size = new System.Drawing.Size(140, 23);
            this.btnSaveExel.TabIndex = 13;
            this.btnSaveExel.Text = "Выбрать Exel файл";
            this.btnSaveExel.UseVisualStyleBackColor = true;
            this.btnSaveExel.Click += new System.EventHandler(this.btnSaveExel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(184, 258);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Готово";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(13, 253);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(141, 23);
            this.btnConvert.TabIndex = 11;
            this.btnConvert.Text = "Конвертировать";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnBrowsePdf
            // 
            this.btnBrowsePdf.Location = new System.Drawing.Point(12, 36);
            this.btnBrowsePdf.Name = "btnBrowsePdf";
            this.btnBrowsePdf.Size = new System.Drawing.Size(141, 27);
            this.btnBrowsePdf.TabIndex = 10;
            this.btnBrowsePdf.Text = "Выбрать Html файл";
            this.btnBrowsePdf.UseVisualStyleBackColor = true;
            this.btnBrowsePdf.Click += new System.EventHandler(this.btnBrowsePdf_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Путь к HTML файлу:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 19);
            this.button1.TabIndex = 17;
            this.button1.Text = "локальный файл";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.RosyBrown;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(518, 373);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Назад";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelFileSelector
            // 
            this.panelFileSelector.Controls.Add(this.button3);
            this.panelFileSelector.Controls.Add(this.dataGridView1);
            this.panelFileSelector.Location = new System.Drawing.Point(250, 412);
            this.panelFileSelector.Name = "panelFileSelector";
            this.panelFileSelector.Size = new System.Drawing.Size(341, 295);
            this.panelFileSelector.TabIndex = 21;
            this.panelFileSelector.Paint += new System.Windows.Forms.PaintEventHandler(this.panelFileSelector_Paint);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.RosyBrown;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(244, 269);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "Закрыть";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Имя,
            this.Выбор});
            this.dataGridView1.Location = new System.Drawing.Point(3, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(335, 256);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Имя
            // 
            this.Имя.HeaderText = "Имя";
            this.Имя.Name = "Имя";
            // 
            // Выбор
            // 
            this.Выбор.HeaderText = "";
            this.Выбор.Name = "Выбор";
            // 
            // dgvFiles
            // 
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Location = new System.Drawing.Point(269, 9);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.Size = new System.Drawing.Size(322, 199);
            this.dgvFiles.TabIndex = 22;
            // 
            // RosTel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(603, 407);
            this.Controls.Add(this.dgvFiles);
            this.Controls.Add(this.panelFileSelector);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblStatus3);
            this.Controls.Add(this.lblStatus2);
            this.Controls.Add(this.btnSaveExel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnBrowsePdf);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RosTel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Расчёт услуг РосТелеком";
            this.Load += new System.EventHandler(this.RosTel_Load);
            this.panelFileSelector.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus3;
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.Button btnSaveExel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnBrowsePdf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panelFileSelector;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Имя;
        private System.Windows.Forms.DataGridViewTextBoxColumn Выбор;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dgvFiles;
    }
}