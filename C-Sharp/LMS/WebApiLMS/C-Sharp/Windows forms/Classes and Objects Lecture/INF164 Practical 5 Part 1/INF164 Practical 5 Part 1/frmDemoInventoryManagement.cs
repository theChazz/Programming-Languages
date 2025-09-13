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
    public partial class frmDemoInventoryManagement : Form
    {
        public frmDemoInventoryManagement()
        {
            InitializeComponent();
        }

        // STEP 1: Declare binding list using created class
        BindingList<Products> productsList = new BindingList<Products>();

        private void frmDemoInventoryManagement_Load(object sender, EventArgs e)
        {
            // STEP 1: Create and add instruments to the binding list
            Products item = new Products("Pro001", "Aniseed oil", 10.00, 0);
            productsList.Add(item);

                    item = new Products("Prod002", "Uncle bob's organic dried pears", 15.00, 0);
            productsList.Add(item);

            // STEP 1: Make the binding list the data grid source
            dataGridView1.DataSource = productsList;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // STEP 2: Create an AddForm object
            frmAddProduct myAddForm = new frmAddProduct();
            // STEP 2: Open the AddForm object
            myAddForm.ShowDialog();

            // STEP 2: Add the add form objects newInstrument object to the instrument list
            productsList.Add(myAddForm.newproductItem);

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // STEP 3: //get the selected row index
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            // STEP 3: //create an instrument object and make it equal to the object in the list at the selected index
            Products productToEdit = productsList[selectedIndex];
            
            // STEP 3: Create an AddForm object
            frmEditProduct myEditForm = new frmEditProduct();
            // STEP 3: Set the EditForm objects instrumentToEdit equal the the one created above
            myEditForm.itemToEdit = productToEdit;
            // STEP 3: Open the AddForm object
            myEditForm.ShowDialog();

            // STEP 3: Set the object in the list at the slected index equal to the edit forms instrumentToEdit object
            // STEP 3: This will only run aftyer the edit form (opened above) closes and the object in the edit form has been updated
            productsList[selectedIndex] = myEditForm.itemToEdit;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //check if a cell is selected
            if (dataGridView1.CurrentCell != null)
            {
                // STEP 4: Get the selected row index
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                // STEP 4: Create an instrument object and make it equal to the object in the list at the selected index
                Products instrumentToRemove = productsList[selectedIndex];
                // STEP 4: Remove the object created above from the instrument list
                productsList.Remove(instrumentToRemove);

                // STEP 4: How a success message (using the created object to show the name)
                MessageBox.Show(instrumentToRemove.ProductName + " has been removed!");
            }
            else
            {
                //error message if no cell selected
                MessageBox.Show("Please make a selection");
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
                // Check if a cell is selected
                if (dataGridView1.CurrentCell != null)
                {
                    // Get the selected row index
                    int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                
                    //dataGridView1.Refresh();
                
                    if (productsList[selectedIndex].CheckStock() == true)
                    { 
                        MessageBox.Show("Product is low on stock : Unit stock is below 20."); 
                    }
                    else 
                    { 
                        MessageBox.Show("Product is okay on stock"); 
                    }

                }
                else
                {
                    // Error message if no cell selected
                    MessageBox.Show("Please make a selection");
                }
            
            
        }
    }
}
