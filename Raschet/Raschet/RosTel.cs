using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;

namespace Raschet
{
    public partial class RosTel : Form
    {
        private string connectionString = "Server=localhost;Database=document_processing;port=3306;username=root;password=root;";
        private string pdfPath = string.Empty;
        private string excelPath = string.Empty;
        public RosTel()
        {
            InitializeComponent();
            SetupDataGrid();
            LoadFiles();

        }
        private void SetupDataGrid()
        {
            dataGridView1.Columns.Clear();

          
            dataGridView1.Columns.Add("FileName", "Имя файла");

       
            dataGridView1.Columns.Add("Date", "Дата");

           
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = "Select";
            btnColumn.HeaderText = "";
            btnColumn.Text = "Выбрать";
            btnColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnColumn);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = false; 
        }

        private void LoadFiles()
        {
            dataGridView1.Rows.Clear();

            // Пример 2 файлов HTML, имена и даты
            dataGridView1.Rows.Add("Rostelecom_February2025.html", "28.02.2025", "");
            dataGridView1.Rows.Add("Rostelecom_March2025.html", "31.03.2025", "");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, что нажата кнопка
            if (e.ColumnIndex == dataGridView1.Columns["Select"].Index && e.RowIndex >= 0)
            {
                string fileName = dataGridView1.Rows[e.RowIndex].Cells["FileName"].Value.ToString();
                string date = dataGridView1.Rows[e.RowIndex].Cells["Date"].Value.ToString();

                MessageBox.Show($"Вы выбрали файл:\n{fileName}\nДата: {date}", "Файл выбран", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Тут можно добавить логику открытия файла или запуска обработки
            }

        }
        private void LoadFilesFromDatabase()
        {
            //dgvFiles.Columns.Clear();
            //dgvFiles.Rows.Clear();

            //using (var connection = new MySqlConnection(connectionString))
            //{
            //    connection.Open();
            //    string query = "SELECT file_name, received_at FROM mail_files WHERE file_type = 'html' AND file_name LIKE '%Rostelecom%'";
            //    using (var command = new MySqlCommand(query, connection))
            //    using (var reader = command.ExecuteReader())
            //    {
            //        dgvFiles.Columns.Add("file_name", "Имя файла");
            //        dgvFiles.Columns.Add("received_at", "Дата получения");


            //        var btnColumn = new DataGridViewButtonColumn
            //        {
            //            HeaderText = "Действие",
            //            Text = "Выбрать",
            //            UseColumnTextForButtonValue = true
            //        };
            //        dgvFiles.Columns.Add(btnColumn);

            //        while (reader.Read())
            //        {
            //            string fileName = reader.GetString("file_name");
            //            DateTime receivedAt = reader.GetDateTime("received_at");
            //            dgvFiles.Rows.Add(fileName, receivedAt.ToString("yyyy-MM-dd"));
            //        }
            //    }
            //}

            //dgvFiles.Visible = true; 
            //dgvFiles.RowHeadersVisible = false;
            //dgvFiles.AllowUserToAddRows = false;
            //dgvFiles.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvFiles.Columns[0].Width = 300;
            MessageBox.Show("Нет данных", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dgvFiles.Visible = false;


        }

        private void panelFileSelector_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Укажите путь к HTML файлу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblStatus.Text = "Ошибка!";
            lblStatus.ForeColor = Color.Red;
            return;

        }
        private void dgvFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2) // кнопка "Выбрать"
            {
                string fileName = dgvFiles.Rows[e.RowIndex].Cells[0].Value.ToString();
                pdfPath = Path.Combine(@"F:\VizStud\raschetfail", fileName);

                label1.Text = $"Путь к PDF файлу: C:/files/Rostelecom_February2025.html";
                lblStatus2.Text = $"Выбран файл: {fileName}";

                dgvFiles.Visible = false;
            }
        }


        private void btnBrowsePdf_Click(object sender, EventArgs e)
        {
            LoadFilesFromDatabase();

        }
        private void InitializeLabels()
        {
            lblStatus.Text = string.Empty;
            lblStatus2.Text = string.Empty;
            lblStatus3.Text = string.Empty;
            label1.Text = "Путь к HTML файлу:";
            label2.Text = "Путь к Excel файлу:";
        }
        private void RosTel_Load(object sender, EventArgs e)
        {
            InitializeLabels();

            this.Font = new Font("Segoe UI", 9);
            btnBrowsePdf.BackColor = Color.FromArgb(240, 240, 240);
            btnBrowsePdf.FlatStyle = FlatStyle.Standard;
            btnSaveExel.BackColor = Color.FromArgb(240, 240, 240);
            btnSaveExel.FlatStyle = FlatStyle.Standard;
            btnConvert.BackColor = Color.FromArgb(78, 126, 179);
            btnConvert.ForeColor = Color.White;
            btnConvert.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnConvert.FlatStyle = FlatStyle.Flat;
            label1.ForeColor = Color.FromArgb(51, 51, 51);
            label2.ForeColor = Color.FromArgb(51, 51, 51);
            lblStatus.ForeColor = Color.FromArgb(0, 100, 0);

            dgvFiles.Visible = false;
            dgvFiles.CellContentClick += dgvFiles_CellContentClick;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnSaveExel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls|Все файлы (*.*)|*.*";

                while (true) // Цикл, чтобы можно было повторно выбрать, если отменил перезапись
                {
                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        
                        return;
                    }

                    string selectedFile = openFileDialog.FileName;

                    var confirmResult = MessageBox.Show(
                        $"Вы действительно хотите перезаписать файл?\n{selectedFile}",
                        "Подтверждение перезаписи",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        
                        excelPath = selectedFile;
                        lblStatus3.Text = $"Выбран файл: {Path.GetFileName(excelPath)}";
                        label2.Text = $"Путь к Excel файлу: {excelPath}";
                        break;
                    }
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "HTML файлы (*.html;*.htm)|*.html;*.htm|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите HTML файл";

                while (true) 
                {
                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                    {
                       
                        return;
                    }

                    string selectedFile = openFileDialog.FileName;
                    string extension = Path.GetExtension(selectedFile).ToLower();

                    if (extension == ".html" || extension == ".htm")
                    {
                       
                        MessageBox.Show($"Выбран файл:\n{selectedFile}", "Файл выбран", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        label1.Text = $"Путь к HTML файлу: {selectedFile}";
                        pdfPath = selectedFile;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите файл с расширением .html или .htm", "Неверный файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                       
                    }
                }
            }
        }
    }
}
