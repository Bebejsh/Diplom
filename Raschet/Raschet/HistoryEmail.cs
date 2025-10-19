using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Raschet
{
    public partial class HistoryEmail : Form
    {
        private string connectionString = "Server=localhost;Database=document_processing;port=3306;username=root;password=root;";

        public HistoryEmail()
        {
            InitializeComponent();
            SetupDataGrid();
            LoadEmailHistory();
        }
        private void SetupDataGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("FileName", "Имя файла");
            dataGridView1.Columns.Add("ReceivedDate", "Дата получения");
            dataGridView1.Columns.Add("FilePath", "Путь к файлу");
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.Columns["FileName"].Width = 200;           // ширина для "Имя файла"
            dataGridView1.Columns["ReceivedDate"].Width = 120;       // ширина для "Дата получения"
            dataGridView1.Columns["FilePath"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 
        }

        private void LoadEmailHistory()
        {
            dataGridView1.Rows.Clear();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT file_name, received_at, file_path FROM mail_files ORDER BY received_at DESC";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string fileName = reader.GetString("file_name");
                        DateTime receivedAt = reader.GetDateTime("received_at");
                        string filePath = reader.GetString("file_path");

                        // Форматируем дату для отображения
                        string receivedDateStr = receivedAt.ToString("dd.MM.yyyy HH:mm");

                        dataGridView1.Rows.Add(fileName, receivedDateStr, filePath);
                    }
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void HistoryEmail_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            //MessageBox.Show("История пуста", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //MessageBox.Show("Ошибка загрузки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
