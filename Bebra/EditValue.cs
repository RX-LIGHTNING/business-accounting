using Entity;
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
    public partial class EditValue : Form
    {
        private ApplicationDbContext applicationContext = new ApplicationDbContext();
        private int id;
        private Boolean isEdit;
        private Busines busines;
        public EditValue(int id)
        {
            InitializeComponent();
            foreach (Activity a in applicationContext.Activities)
            {
                ActivityBox.Items.Add(a.ToString());
            }
            foreach (City a in applicationContext.Cities)
            {
                CityBox.Items.Add(a.ToString());
            }
            if (id > 0) {
                isEdit = true;
                this.id = id;
                busines = applicationContext.Businesses.Include(x=>x.Activity).Include(x=>x.City).FirstOrDefault(x=>x.Id==id);
                NameText.Text = busines.Name;
                AdressText.Text = busines.Adress;
                DirectorText.Text = busines.Director;
                TimePicker.Value = busines.CreationTime;
                CityBox.SelectedItem = busines.City.ToString();
                ActivityBox.SelectedItem = busines.Activity.ToString();
                WorkersText.Text = busines.WorkersQuantity.ToString();
            }
            else{
                isEdit = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditValue_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isEdit)
            {
                busines.Name = NameText.Text;
                busines.Adress = AdressText.Text;
                busines.Director = DirectorText.Text;
                busines.CreationTime = TimePicker.Value;
                busines.City = applicationContext.Cities.FirstOrDefault(x => x.name.Contains(CityBox.SelectedItem.ToString()));
                busines.Activity = applicationContext.Activities.FirstOrDefault(x => x.name.Contains(ActivityBox.SelectedItem.ToString()));
                busines.WorkersQuantity = Int32.Parse(WorkersText.Text);
                applicationContext.Businesses.Update(busines);
                applicationContext.SaveChanges();
            }
            else{
                Busines busines = new Busines();
   
                busines.Name = NameText.Text;
                busines.Adress = AdressText.Text;
                busines.Director = DirectorText.Text;
                busines.CreationTime = TimePicker.Value;
                busines.City = applicationContext.Cities.FirstOrDefault(x=>x.name.Contains(CityBox.SelectedItem.ToString()));
                busines.Activity = applicationContext.Activities.FirstOrDefault(x => x.name.Contains(ActivityBox.SelectedItem.ToString()));
                busines.WorkersQuantity = Int32.Parse(WorkersText.Text);
                applicationContext.Businesses.Add(busines);
                applicationContext.SaveChanges();
            }
            var mainForm = Application.OpenForms.OfType<Main>().Single();
            mainForm.datagridUpdate("");
            var editForm = Application.OpenForms.OfType<EditValue>().Single();
            editForm.Close();
        }
    }
}
