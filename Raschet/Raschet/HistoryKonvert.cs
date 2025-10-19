using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Raschet
{
    public partial class HistoryKonvert : Form
    {
        private string connectionString = "Server=localhost;Database=document_processing;port=3306;username=root;password=root;";

        public HistoryKonvert()
        {
            InitializeComponent();
            SetupDataGrid();
            LoadHistoryFiles();

        }
        private void SetupDataGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("FileName", "Имя файла");
            dataGridView1.Columns.Add("ProcessedDate", "Дата");
            dataGridView1.Columns.Add("FilePath", "Путь к файлу");
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.Columns["FileName"].Width = 200;     
            dataGridView1.Columns["ProcessedDate"].Width = 120; 
            dataGridView1.Columns["FilePath"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadHistoryFiles()
        {
            dataGridView1.Rows.Clear();

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        SELECT file_name, processed_at, file_path
                        FROM processed_files
                        WHERE LOWER(file_name) LIKE 'rostelecom_%'
                        ORDER BY processed_at DESC";

                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool anyRows = false;

                        while (reader.Read())
                        {
                            anyRows = true;

                            string fileName = reader.GetString("file_name");
                            DateTime processedAt = reader.GetDateTime("processed_at");
                            string filePath = reader.GetString("file_path");

                            string dateStr = processedAt.ToString("dd.MM.yyyy HH:mm", CultureInfo.CurrentCulture);

                            dataGridView1.Rows.Add(fileName, dateStr, filePath);
                        }

                        if (!anyRows)
                        {
                            MessageBox.Show("Записи Ростелеком не найдены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке истории: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
