using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INF164_Practical_5_Part_1
{
    public partial class frmEditProduct : Form
    {
        public frmEditProduct()
        {
            InitializeComponent();
        }

        // STEP 3: Create a public object accessable from other forms
        public Products itemToEdit;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // STEP 3: Check for user input
            if (txtProductName.Text != "" && txtProductCode.Text != "")
            {
                // Set the public product objects attributes equal to the users input
                itemToEdit.ProductName = txtProductName.Text;
                itemToEdit.ProductCode = txtProductCode.Text;
                itemToEdit.UnitPrice = Convert.ToDouble(txtProductPrice.Text);
                itemToEdit.UnitsinStock = Convert.ToInt32(nudUnitsOnHand.Value);

                // STEP 3: Close the form
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in al fields");
            }
        }

        private void frmEditProduct_Load(object sender, EventArgs e)
        {
            // Set inputs up from the passed object
            txtProductPrice.Text = Convert.ToString(itemToEdit.UnitPrice);
            txtProductName.Text = itemToEdit.ProductName;
            txtProductCode.Text = itemToEdit.ProductCode;
            nudUnitsOnHand.Value = itemToEdit.UnitsinStock;
        }
    }
}
