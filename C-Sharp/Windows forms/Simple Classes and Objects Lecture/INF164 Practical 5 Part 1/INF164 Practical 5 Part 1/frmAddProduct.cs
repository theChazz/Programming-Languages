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
    public partial class frmAddProduct : Form
    {
        public frmAddProduct()
        {
            InitializeComponent();
        }

        // STEP 2: Create a public object accessable from other forms
        public Products newproductItem = new Products();
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Check for user input
            if (txtProductName.Text != "" && txtProductCode.Text != "")
            {
                // Set the public product objects attributes equal to the users input
                newproductItem.ProductName = txtProductName.Text;
                newproductItem.ProductCode = txtProductCode.Text;
                newproductItem.UnitPrice = Convert.ToDouble(txtProductPrice.Text);
                newproductItem.UnitsinStock = Convert.ToInt32(nudUnitsOnHand.Value);

                // Close the form
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in al fields");
            }
        }
    }
}
