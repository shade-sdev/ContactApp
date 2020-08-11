using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactApp
{
    public partial class ContactItem : UserControl
    {
        public ContactItem()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler Del;
        public event EventHandler Edit;
        public event EventHandler View;
        public string ControlName
        {
            get
            {
                return this.Name;

            }

            set { this.Name = value; }
        }

        public string Lblnumber
        {
            get {return lblContactPhone.Text; }
            set { lblContactPhone.Text = value; }

        }

        public Guna.UI.WinForms.GunaCirclePictureBox ContactImage
        {
            get { return ContactPicture; }
            set { ContactPicture = value; }
        }

        public string LblName

        {
            get { return lblContactName.Text; }
            set { lblContactName.Text = value; }
        }

      

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            this.Del?.Invoke(this, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.Edit?.Invoke(this, e);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.View?.Invoke(this, e);
        }
    }
}
