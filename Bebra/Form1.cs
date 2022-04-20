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
    public partial class EditActivity : Form
    {
        private static ApplicationDbContext applicationContext = new ApplicationDbContext();
        private int object_type=0; // 1 = City 2=Activity;
        private long id=-1;        //object id
        public EditActivity(int object_type, long id)
        {
            InitializeComponent();
            this.object_type = object_type;
            this.id = id;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string value="";
            if (object_type == 1) {
                value = applicationContext.Cities.Find(id).name;
            }
            else if(object_type==2){
                value = applicationContext.Activities.Find(id).name;
            }
            textBox1.Text = value;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                var mainForm = Application.OpenForms.OfType<Cities>().Single();

                string value = textBox1.Text;
                if (object_type == 1)
                {
                    City city = applicationContext.Cities.Find(id);
                    city.name = value;
                    applicationContext.Cities.Update(city);
                    applicationContext.SaveChanges();
                    mainForm.datagrid1Update("");
                }
                else if (object_type == 2)
                {
                    Activity activity =  applicationContext.Activities.Find(id);
                    activity.name = value;
                    applicationContext.Activities.Update(activity);
                    applicationContext.SaveChanges();
                    mainForm.datagrid2Update("");
                }

                var editForm = Application.OpenForms.OfType<EditActivity>().Single();
                editForm.Close();
            }
            else {
                MessageBox.Show("Editable Value is Null");
            }
        }
    }
}
