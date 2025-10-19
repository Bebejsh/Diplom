using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;



namespace Raschet
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost;Database=document_processing;port=3306;username=root;password=root;";
        private string pdfPath = string.Empty;
        private string excelPath = string.Empty;

        public Form1()
        {
            InitializeComponent();
            InitializeLabels();
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            MessageBox.Show("Нет данных", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeLabels();
            this.Font = new Font("Segoe UI", 9);
            btnBrowsePdf.BackColor = Color.FromArgb(240, 240, 240);
            btnBrowsePdf.FlatStyle = FlatStyle.Standard;
            btnSaveExel.BackColor = Color.FromArgb(240, 240, 240);
            btnSaveExel.FlatStyle = FlatStyle.Standard;
            btnConvert.BackColor = Color.FromArgb(78, 126, 179); // Мягкий синий  
            btnConvert.ForeColor = Color.White;
            btnConvert.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnConvert.FlatStyle = FlatStyle.Flat;
            label1.ForeColor = Color.FromArgb(51, 51, 51); // Тёмно-серый  
            label2.ForeColor = Color.FromArgb(51, 51, 51);
            lblStatus.ForeColor = Color.FromArgb(0, 100, 0); // Тёмно-зелёный для статуса  
        }
        private void InitializeLabels()
        {
            lblStatus.Text = string.Empty;
            lblStatus2.Text = string.Empty;
            lblStatus3.Text = string.Empty;
            label1.Text = "Путь к PDF файлу:";
            label2.Text = "Путь к Excel файлу:";

        }

        private bool UpdateExcel(string excelPath, Dictionary<string, (decimal Services, decimal VAT, decimal Total)> phoneData)
        {
            try
            {
                using (var workbook = new XLWorkbook(excelPath))
                {
                    var worksheet = workbook.Worksheet("МТС");
                    if (worksheet == null)
                    {
                        MessageBox.Show("Лист 'МТС' не найден в файле Excel.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; //false, лист не найден
                    }

                    UpdateExistingRows(worksheet, phoneData);
                    AddNewRows(worksheet, phoneData);
                    FormatNumberRange(worksheet);

                    workbook.Save();
                    return true; //true при успешном
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Ошибка при записи в Excel");
                return false;
            }
        }

        private void UpdateExistingRows(IXLWorksheet worksheet, Dictionary<string, (decimal Services, decimal VAT, decimal Total)> phoneData)
        {
            for (int row = 2; row <= worksheet.LastRowUsed().RowNumber(); row++)
            {
                var phoneCell = worksheet.Cell(row, 4);
                string phoneNumber = phoneCell.GetFormattedString();

                if (phoneData.TryGetValue(phoneNumber, out var data))
                {
                    worksheet.Cell(row, 5).Value = Math.Round(data.Services, 2);
                    worksheet.Cell(row, 6).Value = Math.Round(data.Total, 2);
                    worksheet.Cell(row, 7).Value = Math.Round(data.VAT, 2);
                    phoneData.Remove(phoneNumber);
                }
                // выравнивание по правому
                phoneCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            }
        }

        private void AddNewRows(IXLWorksheet worksheet, Dictionary<string, (decimal Services, decimal VAT, decimal Total)> phoneData)
        {
            var emptyRows = GetEmptyRows(worksheet);
            int emptyRowIndex = 0;

            while (phoneData.Count > 0 && emptyRowIndex < emptyRows.Count)
            {
                var phone = phoneData.First();
                int row = emptyRows[emptyRowIndex];

                worksheet.Cell(row, 4).Value = phone.Key;
                worksheet.Cell(row, 5).Value = Math.Round(phone.Value.Services, 2);
                worksheet.Cell(row, 6).Value = Math.Round(phone.Value.Total, 2);
                worksheet.Cell(row, 7).Value = Math.Round(phone.Value.VAT, 2);

                phoneData.Remove(phone.Key);
                emptyRowIndex++;
            }

            if (phoneData.Count > 0)
            {
                int newRow = worksheet.LastRowUsed().RowNumber() + 1;
                foreach (var phone in phoneData)
                {
                    worksheet.Cell(newRow, 4).Value = phone.Key;
                    worksheet.Cell(newRow, 5).Value = Math.Round(phone.Value.Services, 2);
                    worksheet.Cell(newRow, 6).Value = Math.Round(phone.Value.Total, 2);
                    worksheet.Cell(newRow, 7).Value = Math.Round(phone.Value.VAT, 2);
                    newRow++;
                }
            }
        }

        private List<int> GetEmptyRows(IXLWorksheet worksheet)
        {
            var emptyRows = new List<int>();
            for (int row = 2; row <= worksheet.LastRowUsed().RowNumber(); row++)
            {
                if (string.IsNullOrWhiteSpace(worksheet.Cell(row, 4).GetString()))
                {
                    emptyRows.Add(row);
                }
            }
            return emptyRows;
        }

        private void FormatNumberRange(IXLWorksheet worksheet)
        {
            var numberRange = worksheet.Range(2, 5, worksheet.LastRowUsed().RowNumber(), 7);
            numberRange.Style.NumberFormat.Format = "0.00";
        }

        private void btnSaveExel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.DefaultExt = "xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    excelPath = saveFileDialog.FileName;
                    lblStatus3.Text = $"Файл будет сохранен: {Path.GetFileName(excelPath)}";
                    label2.Text = $"Путь к Excel файлу: {excelPath}";
                }
            }
        }

        private void btnConvert_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
                {
                    MessageBox.Show("Укажите путь к PDF файлу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Ошибка!";
                    lblStatus.ForeColor = Color.Red;
                    return;
                }

                if (string.IsNullOrEmpty(excelPath))
                {
                    MessageBox.Show("Укажите путь для сохранения Excel файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Ошибка!";
                    lblStatus.ForeColor = Color.Red;
                    return;
                }

                var parser = new PdfParser();
                var phoneData = parser.ParsePdf(pdfPath);

                if (phoneData.Count == 0)
                {
                    MessageBox.Show("Нет данных для записи в Excel.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Нет данных!";
                    lblStatus.ForeColor = Color.Orange;
                    return;
                }

                bool success = UpdateExcel(excelPath, phoneData);

                if (success)
                {
                    
                    InsertProcessedFileRecord(excelPath);

                    lblStatus.Text = "Готово!";
                    lblStatus.ForeColor = Color.Green;
                    MessageBox.Show("Данные успешно записаны в Excel.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Ошибка!";
                    lblStatus.ForeColor = Color.Red;
                }
            }
            catch (IOException ioEx)
            {
                HandleException(ioEx, "Ошибка ввода-вывода");
                lblStatus.Text = "Ошибка ввода-вывода!";
                lblStatus.ForeColor = Color.Red;
            }
            catch (UnauthorizedAccessException unauthEx)
            {
                HandleException(unauthEx, "Ошибка доступа");
                lblStatus.Text = "Ошибка доступа!";
                lblStatus.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                lblStatus.Text = "Ошибка!";
                lblStatus.ForeColor = Color.Red;
            }
        }

        // Метод вставки записи в таблицу processed_files
        private void InsertProcessedFileRecord(string excelFilePath)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string monthYear = DateTime.Now.ToString("MMMM yyyy", CultureInfo.InvariantCulture);
                    string fileName = $"MTC_{monthYear.Replace(" ", "")}_Result.xlsx";

                    string normalizedPath = excelFilePath.Replace('\\', '/');

                    string sql = @"
                INSERT INTO processed_files (source_file_id, file_name, file_path, processed_at) 
                VALUES (NULL, @fileName, @filePath, @processedAt)";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@fileName", fileName);
                        cmd.Parameters.AddWithValue("@filePath", normalizedPath);
                        cmd.Parameters.AddWithValue("@processedAt", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка записи в базу данных: {ex.Message}", "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogError(ex);
            }
        }

        private void HandleException(Exception ex, string customMessage = "")
        {
            string message = !string.IsNullOrEmpty(customMessage) ? customMessage : "Ошибка";
            MessageBox.Show($"{message}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblStatus.ForeColor = Color.Red;
            LogError(ex);
        }

        private void LogError(Exception ex)
        {
            string logFilePath = "error_log.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {ex.Message}");
                    writer.WriteLine(ex.StackTrace);
                    writer.WriteLine();
                }
            }
            catch (Exception logEx)
            {
                MessageBox.Show($"Ошибка при записи лога: {logEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pdfPath = openFileDialog.FileName;
                    lblStatus2.Text = $"Выбран файл: {Path.GetFileName(pdfPath)}";
                    label1.Text = $"Путь к PDF файлу: {pdfPath}";

                }
            }
        }
    }
}