using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Raschet
{
    public partial class OsnovForm : Form
    {
        public OsnovForm()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RosTel rosTel = new RosTel();
            rosTel.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            HistoryKonvert1 historyForm1 = new HistoryKonvert1();
            historyForm1.ShowDialog();
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
                MessageBox.Show("Нет доступа к почте", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //  MessageBox.Show("Обновление успешно, новые файлы были добавлены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // MessageBox.Show("Ошибка загрузки файлов с почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HistoryEmail historyemail = new HistoryEmail();
            historyemail.ShowDialog();
        }

        private void OsnovForm_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            HistoryKonvert historyForm = new HistoryKonvert();
            historyForm.ShowDialog();
        }
    }
}
