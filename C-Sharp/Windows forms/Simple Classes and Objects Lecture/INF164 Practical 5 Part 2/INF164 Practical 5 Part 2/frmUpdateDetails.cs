using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INF164_Practical_5_Part_2
{
    public partial class frmUpdateDetails : Form
    {
        public frmUpdateDetails()
        {
            InitializeComponent();
        }

        // STEP 3: Create a public object accessable from other forms
        public Contracts itemToUpdate;

        private void frmUpdateDetails_Load(object sender, EventArgs e)
        {
            // Set inputs up from the passed object
            txtName.Text = Convert.ToString(itemToUpdate.ClientName);
            txtSurname.Text = itemToUpdate.ClientSurname;
            txtDeviceName.Text = itemToUpdate.DeviceName;
            dateTimePicker1.Value = Convert.ToDateTime(itemToUpdate.ContractDate);
            nupRemaining.Value = itemToUpdate.MonthsRemaining;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // STEP 3: Check for user input
            if (txtName.Text != "" && txtSurname.Text != "")
            {
                // Set the public product objects attributes equal to the users input
                itemToUpdate.ClientName = txtName.Text;
                itemToUpdate.ClientSurname = txtSurname.Text;
                itemToUpdate.DeviceName = txtDeviceName.Text;
                itemToUpdate.ContractDate = Convert.ToDateTime(dateTimePicker1.Value);
                itemToUpdate.MonthsRemaining = Convert.ToInt32(nupRemaining.Value);

                // STEP 3: Close the form
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in all fields");
            }
        }
    }
}
