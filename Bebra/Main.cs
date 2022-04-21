using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;
namespace Bebra
{
    public partial class Main : Form
    {
        private static ApplicationDbContext applicationContext = new ApplicationDbContext();
        private static List<Busines> Busines = null;
        public Main()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            datagridUpdate("");
        }
        public void datagridUpdate(string text)
        {
            dataGridView1.Rows.Clear();
            applicationContext = new ApplicationDbContext();
            Busines = applicationContext.Businesses.AsNoTracking().Include(x => (x.City)).Include(x => (x.Activity)).Where(x => x.Name.ToLower().Contains(text.ToLower())).ToList();
            SortableBindingList<Busines> Busineses = new SortableBindingList<Busines>(Busines);
            dataGridView1.DataSource = Busineses;
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditValue editForm = new EditValue(-1);
            editForm.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            datagridUpdate(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (dataGridView1.SelectedRows.Count > 0 && confirmResult == DialogResult.Yes)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                long id = Int32.Parse(dataGridView1[0, index].Value.ToString());
                Busines busines = applicationContext.Businesses.Find(id);
                applicationContext.Businesses.Remove(busines);
                applicationContext.SaveChanges();
                datagridUpdate("");
                MessageBox.Show("Object has been deleted");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = Int32.Parse(dataGridView1[0, index].Value.ToString());
                EditValue editForm = new EditValue(id);
                editForm.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cities citiesForm = new Cities();
            citiesForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                long id = Int32.Parse(dataGridView1[0, index].Value.ToString());
                Busines busines = applicationContext.Businesses.Find(id);
                WorkloadForm citiesForm = new WorkloadForm(busines);
                citiesForm.Show();
            }
        }
        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }


    }
}
  
