using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Windows.Forms;


namespace Raschet
{
    public class PdfParser
    {
        private const string NetworkResource = "Сетевой ресурс";
        private const string PeriodicServices = "Периодические услуги";
        private const string VAT = "НДС";
        private const string Total = "Итого:";
        private const string OneTimeServices = "Разовые услуги";
        private const string PhoneServices = "Телефонные услуги";

        public Dictionary<string, (decimal Services, decimal VAT, decimal Total)> ParsePdf(string pdfPath)
        {
            var phoneDataDictionary = new Dictionary<string, (decimal Services, decimal VAT, decimal Total)>();

            try
            {
                using (PdfReader pdfReader = new PdfReader(pdfPath))
                {
                    for (int i = 1; i <= pdfReader.NumberOfPages; i++)
                    {
                        string text = PdfTextExtractor.GetTextFromPage(pdfReader, i);
                        var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                        ProcessPageLines(lines, ref phoneDataDictionary);
                    }
                }
            }
            catch (Exception ex)
            {
             
                MessageBox.Show($"Ошибка при чтении PDF: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Указать, данные не были успешно извлечены
                return new Dictionary<string, (decimal Services, decimal VAT, decimal Total)>();
            }

            return phoneDataDictionary;
        }

        private void ProcessPageLines(string[] lines, ref Dictionary<string, (decimal Services, decimal VAT, decimal Total)> phoneDataDictionary)
        {
            string currentPhone = null;
            decimal servicesSum = 0;
            decimal vatSum = 0;
            decimal totalSum = 0;

            foreach (var line in lines)
            {
                if (line.StartsWith(NetworkResource))
                {
                    SaveCurrentPhoneData(ref phoneDataDictionary, ref currentPhone, ref servicesSum, ref vatSum, ref totalSum, line);
                }
                else if (currentPhone != null)
                {
                    ProcessLine(line, ref servicesSum, ref vatSum, ref totalSum, ref phoneDataDictionary, ref currentPhone);
                }
            }

            SaveCurrentPhoneData(ref phoneDataDictionary, ref currentPhone, ref servicesSum, ref vatSum, ref totalSum);
        }

        private void SaveCurrentPhoneData(ref Dictionary<string, (decimal Services, decimal VAT, decimal Total)> phoneDataDictionary,
                                         ref string currentPhone, ref decimal servicesSum, ref decimal vatSum,
                                         ref decimal totalSum, string line = null)
        {
            if (!string.IsNullOrEmpty(currentPhone) && !phoneDataDictionary.ContainsKey(currentPhone))
            {
                phoneDataDictionary[currentPhone] = (servicesSum, vatSum, totalSum);
            }

            if (line != null)
            {
                var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 2)
                {
                    currentPhone = parts[2];
                    servicesSum = 0;
                    vatSum = 0;
                    totalSum = 0;

                    if (line.Contains(PeriodicServices))
                    {
                        ExtractServiceValue(line, PeriodicServices, ref servicesSum);
                    }
                }
            }
        }

        private void ProcessLine(string line, ref decimal servicesSum, ref decimal vatSum, ref decimal totalSum,
                                 ref Dictionary<string, (decimal Services, decimal VAT, decimal Total)> phoneDataDictionary,
                                 ref string currentPhone)
        {
            if (line.Contains(VAT))
            {
                ExtractValue(line, ref vatSum);
            }
            else if (line.Contains(Total))
            {
                ExtractValue(line, ref totalSum);
                phoneDataDictionary[currentPhone] = (servicesSum, vatSum, totalSum);
            }
            else if (line.Contains(OneTimeServices) || line.Contains(PhoneServices))
            {
                ExtractServiceValue(line, ref servicesSum);
            }
        }

        private void ExtractValue(string line, ref decimal value)
        {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = parts.Length - 1; j >= 0; j--)
            {
                if (TryParseDecimal(parts[j], out var parsedValue))
                {
                    value = parsedValue;
                    break;
                }
            }
        }

        private void ExtractServiceValue(string line, string serviceType, ref decimal servicesSum)
        {
            var serviceParts = line.Split(new[] { serviceType }, StringSplitOptions.RemoveEmptyEntries);
            if (serviceParts.Length > 1)
            {
                var valueParts = serviceParts[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in valueParts)
                {
                    if (TryParseDecimal(part, out var service))
                    {
                        servicesSum += service;
                        break;
                    }
                }
            }
        }

        private void ExtractServiceValue(string line, ref decimal servicesSum)
        {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = parts.Length - 1; j >= 0; j--)
            {
                if (TryParseDecimal(parts[j], out var service))
                {
                    servicesSum += service;
                    break;
                }
            }
        }

        private bool TryParseDecimal(string value, out decimal result)
        {
            string cleanValue = new string(value.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
            cleanValue = cleanValue.Replace(",", ".");

            var dotCount = cleanValue.Count(c => c == '.');
            if (dotCount > 1)
            {
                cleanValue = cleanValue.Substring(0, cleanValue.IndexOf('.')) +
                           cleanValue.Substring(cleanValue.IndexOf('.') + 1).Replace(".", "");
            }

            return decimal.TryParse(cleanValue, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }
    }
}