using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
namespace Bebra
{
    public partial class Cities : Form
    {
        private static ApplicationDbContext applicationContext = new ApplicationDbContext();
        public Cities()
        {
            InitializeComponent();
            datagrid1Update("");
            datagrid2Update("");
        }

        private void Cities_Load(object sender, EventArgs e)
        {
            
        }
        public void datagrid1Update(string text)
        {
            dataGridView1.Rows.Clear();
            SortableBindingList<City> Cities = new SortableBindingList<City>(applicationContext.Cities.Where(x=>x.name.Contains(text)).ToList());
            dataGridView1.DataSource = Cities;
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void datagrid2Update(string text)
        {
            dataGridView2.Rows.Clear();
            SortableBindingList<Activity> activities = new SortableBindingList<Activity>(applicationContext.Activities.Where(x => x.name.Contains(text)).ToList());
            dataGridView2.DataSource = activities;
            this.dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            datagrid1Update(textBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            City temp = new City();
            temp.name = textBox1.Text;
            applicationContext.Cities.Add(temp);
            applicationContext.SaveChanges();
            datagrid1Update(textBox2.Text);
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
                City city = applicationContext.Cities.Find(id);
                try
                {
                    applicationContext.Cities.Remove(city);
                applicationContext.SaveChanges();
                datagrid1Update(textBox2.Text);
               
                    MessageBox.Show("Object has been deleted");
            }
                catch (Exception)
            {
                MessageBox.Show("Delete all bussines with that activity");
            }
        }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                long id = Int32.Parse(dataGridView1[0, index].Value.ToString());
                EditActivity form = new EditActivity(1,id);
                form.Show();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            datagrid2Update(textBox3.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Activity temp = new Activity();
            temp.name = textBox4.Text;
            applicationContext.Activities.Add(temp);
            applicationContext.SaveChanges();
            datagrid2Update(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (dataGridView2.SelectedRows.Count > 0 && confirmResult == DialogResult.Yes)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                long id = Int32.Parse(dataGridView2[0, index].Value.ToString());
                Activity activity = applicationContext.Activities.Find(id);
                applicationContext.Activities.Remove(activity);
                try
                {
                    applicationContext.SaveChanges();
                    datagrid2Update(textBox3.Text);
                    MessageBox.Show("Object has been deleted");
                }
                catch (Exception)
                {
                    MessageBox.Show("Delete all bussines with that activity");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                long id = Int32.Parse(dataGridView2[0, index].Value.ToString());
                EditActivity form = new EditActivity(2, id);
                form.ShowDialog();
            }
        }
    }
}
