using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactApp
{
    public partial class Contacts : Form
    {
        public Contacts()
        {
            InitializeComponent();
        }
        public string fb;
        public string insta;

        private void SetContact(string controlname, string contactname, string contactphone, string image)
        {
            ContactItem user;
          
            user = new ContactItem();
            user.Del += new EventHandler(user_Delete);
            user.Edit += new EventHandler(user_Edit);
            user.View += new EventHandler(user_View);
            user.ControlName = controlname;
            user.LblName = contactname;
            user.Lblnumber = contactphone;
            user.ContactImage.Image = Image.FromFile(@image);
            flpMain.Controls.Add(user);
        }

        private void user_View(object sender, EventArgs e)
        {
         
            gunaElipsePanel2.Visible = true;
            gunaCirclePictureBox1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;

            var button = sender as ContactItem;
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\ContactAppDB.mdf;Integrated Security=True");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblContacts WHERE contact_id = @contact_id", conn);
            cmd.Parameters.AddWithValue("@contact_id", button.Name);

            using (SqlDataReader myreader = cmd.ExecuteReader())
            {

                while (myreader.Read())
                {
                    gunaCirclePictureBox1.Image = Image.FromFile(myreader["image"].ToString());
                    label1.Text = myreader["name"].ToString();
                    label2.Text = myreader["number"].ToString();
                    label3.Text = myreader["email"].ToString();
                    label4.Text = myreader["address"].ToString();
                    label5.Text = myreader["date_added"].ToString();
                    fb = myreader["facebook"].ToString();
                    insta = myreader["instagram"].ToString();
                }
            }
        }
        private void user_Edit(object sender, EventArgs e)
        {

            AddContact.isUpdate = true;
            var button = sender as ContactItem;
            AddContact.getcontactId = button.Name;
            AddContact ac = new AddContact();
            ac.ShowDialog();
        }

        private void user_Delete(object sender, EventArgs e)
        {



            var button = sender as ContactItem;
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\ContactAppDB.mdf;Integrated Security=True");

            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM tblContacts WHERE contact_id = @contact_id", conn);
            cmd.Parameters.AddWithValue("@contact_id", button.Name);
           
            cmd.ExecuteNonQuery();
            Console.WriteLine("Inserting Data Successfully");
            conn.Close();
            flpMain.Controls.Remove(button);
        }

     

        private void btnAdd_Click(object sender, EventArgs e)
        {
        
            AddContact addform = new AddContact();
            addform.Show();
        }

        private void Contacts_Load(object sender, EventArgs e)
        {
       
            LoadContacts();
        }


        public void LoadContacts()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\ContactAppDB.mdf;Integrated Security=True");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblContacts", conn);

            using (SqlDataReader myreader = cmd.ExecuteReader())
            {

                while (myreader.Read())
                {

                    SetContact(myreader["contact_id"].ToString(), myreader["name"].ToString(), myreader["number"].ToString(), myreader["image"].ToString());
                }
            }
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(fb);
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(insta);
        }

        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {
         
            gunaElipsePanel2.Visible = false;
      
        }
    }
}
