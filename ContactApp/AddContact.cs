using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactApp
{
    public partial class AddContact : Form
    {
     
        public AddContact()
        {
            InitializeComponent();
          
        }

        public static Boolean isUpdate = false;
        public static string getcontactId;
        public Boolean uploadclicked = false;
        public string imagepath;
        ContactAppDataContext doAction = new ContactAppDataContext();
        private void btnADD_Click(object sender, EventArgs e)
        {
         if (isUpdate == false)
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\ContactAppDB.mdf;Integrated Security=True");

                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tblContacts (name, number, email, image, address, facebook, instagram) VALUES (@name, @number, @email,@image, @address, @facebook, @instagram)", conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@number", txtNumber.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@image", imagepath);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@facebook", txtFacebook.Text);
                cmd.Parameters.AddWithValue("@instagram", txtInstagram.Text);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Inserting Data Successfully");
                conn.Close();

                resetmainform();

            } else
                if(isUpdate == true)
            {
                if (uploadclicked == true)
                {
                    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\ContactAppDB.mdf;Integrated Security=True");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE tblcontacts SET name = @name, number = @number, email = @email, image = @image, address = @address, facebook = @facebook, instagram = @instagram WHERE contact_id = @contact_id", conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@contact_id", getcontactId);
                    cmd.Parameters.AddWithValue("@number", txtNumber.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@image", imagepath);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@facebook", txtFacebook.Text);
                    cmd.Parameters.AddWithValue("@instagram", txtInstagram.Text);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Inserting Data Successfully");
                    conn.Close();
                    resetmainform();
                } else
                    if (uploadclicked == false)
                {
                    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\ContactAppDB.mdf;Integrated Security=True");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE tblcontacts SET name = @name, number = @number, email = @email, address = @address, facebook = @facebook, instagram = @instagram WHERE contact_id = @contact_id", conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@number", txtNumber.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@contact_id", getcontactId);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@facebook", txtFacebook.Text);
                    cmd.Parameters.AddWithValue("@instagram", txtInstagram.Text);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Inserting Data Successfully");
                    conn.Close();
                    resetmainform();
                }
            }


        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            uploadclicked = true;
            var OFD = new OpenFileDialog();
            OFD.Filter = "JPG|*.jpg|PNG|*.png*";


            if (OFD.ShowDialog() == DialogResult.OK)
            {
                imagepath = OFD.FileName;
                
            }



        }

        public void resetmainform()
        {
            foreach (Control item in Contacts.flpMain.Controls.OfType<ComboBox>())
            {
                Contacts.flpMain.Controls.Remove(item);
            }

            Contacts.flpMain.Controls.Clear();
            var principalForm = Application.OpenForms.OfType<Contacts>().FirstOrDefault();
            principalForm.LoadContacts();
        }

        private void AddContact_Load(object sender, EventArgs e)
        {
                if (isUpdate == true)
            {
                btnADD.Text = "Update";
                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\ContactAppDB.mdf;Integrated Security=True");
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblContacts WHERE contact_id = @contact_id", conn);
                cmd.Parameters.AddWithValue("@contact_id", getcontactId);
                using (SqlDataReader myreader = cmd.ExecuteReader())
                {

                    while (myreader.Read())
                    {

                        txtName.Text = myreader["name"].ToString();
                        txtNumber.Text = myreader["number"].ToString();
                        txtEmail.Text = myreader["email"].ToString();
                        txtAddress.Text = myreader["address"].ToString();
                        txtFacebook.Text = myreader["facebook"].ToString();
                        txtInstagram.Text = myreader["instagram"].ToString();
                    }
                }
                conn.Close();
            } else
                if (isUpdate == false)
            {
                btnADD.Text = "Add";
            }
        }
    }
}
