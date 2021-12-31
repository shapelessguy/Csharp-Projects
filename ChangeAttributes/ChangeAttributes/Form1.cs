using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ChangeAttributes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                DateTime data = File.GetLastWriteTime(textBox1.Text);
                dateTimePicker1.Value = data;
                dateTimePicker2.Value = data;
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime data = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day,
                dateTimePicker2.Value.Hour, dateTimePicker2.Value.Minute, dateTimePicker2.Value.Second);
            Console.WriteLine(data);
            File.SetLastWriteTime(textBox1.Text, data);
            File.SetCreationTime(textBox1.Text, data);
            File.SetLastAccessTime(textBox1.Text, data);
            button1.Enabled = false;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            textBox1.Text = files[0];
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text)) button1.Enabled = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text)) button1.Enabled = true;
        }
    }
}
