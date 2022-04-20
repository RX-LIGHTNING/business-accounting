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
    public partial class WorkloadForm : Form
    {
        private static ApplicationDbContext applicationContext = new ApplicationDbContext();
        private Busines busines;
        public WorkloadForm(Busines busines)
        {
            InitializeComponent();
            this.busines = busines;
            datagridUpdate("");
        }

        public void datagridUpdate(string year)
        {
            dataGridView1.Rows.Clear();
            SortableBindingList<Workload> workloads = new SortableBindingList<Workload>(applicationContext.Workloads.Where(x => x.Busines == busines).ToList());
            dataGridView1.DataSource = workloads;
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void Workload_Load(object sender, EventArgs e)
        {

        }

        private void add_Click(object sender, EventArgs e)
        {
            if (!WorkBox.Text.Equals("") && !YearBox.Text.Equals(""))
            {
                try
                {
                    if (Int32.Parse(YearBox.Text) > 1800 && Int32.Parse(YearBox.Text) < 3000)
                {
                    Workload temp = new Workload();
                    temp.Year = Int32.Parse(YearBox.Text);
                    temp.WorkloadValue = Int32.Parse(WorkBox.Text);
                    temp.Busines = busines;
                    busines.Workloads.Add(temp);
                    applicationContext.Businesses.Update(busines);
                    applicationContext.SaveChanges();
                    datagridUpdate("");
                }
            }
                catch (Exception)
            {
                MessageBox.Show("Input Correct data");
            }
        }
            else
            {
                MessageBox.Show("Input Correct data");
            }
        }

        private void YearBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (dataGridView1.SelectedRows.Count > 0 && confirmResult == DialogResult.Yes)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                long id = Int32.Parse(dataGridView1[0, index].Value.ToString());
                Workload workload = applicationContext.Workloads.Find(id);
                applicationContext.Workloads.Remove(workload);
                applicationContext.SaveChanges();
                datagridUpdate("");
                MessageBox.Show("Object has been deleted");
            }
        }
    }
}
  