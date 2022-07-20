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
    public partial class frmCellularExpress : Form
    {
        
        public frmCellularExpress()
        {
            InitializeComponent();

        }

        // STEP 1: Declare binding list using created class
        BindingList<Contracts> contractsList = new BindingList<Contracts>();

        private void frmCelularExpress_Load(object sender, EventArgs e)
        {
            // STEP 1: Create and add instruments to the binding list
            Contracts item = new Contracts("Chazz", "Princeton", "Samsung", 12, Convert.ToDateTime("2020-10-01"));
            contractsList.Add(item);

            item = new Contracts("Chazz", "Princeton", "Samsung", 12, Convert.ToDateTime("2021-10-01"));
            contractsList.Add(item);

            // STEP 1: Make the binding list the data grid source
            dataGridView1.DataSource = contractsList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // STEP 3: //get the selected row index
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            // STEP 3: //create an instrument object and make it equal to the object in the list at the selected index
            Contracts contractToEdit = contractsList[selectedIndex];

            // STEP 3: Create an AddForm object
            frmUpdateDetails myEditForm = new frmUpdateDetails();
            // STEP 3: Set the EditForm objects instrumentToEdit equal the the one created above
            myEditForm.itemToUpdate = contractToEdit;
            // STEP 3: Open the AddForm object
            myEditForm.ShowDialog();

            // STEP 3: Set the object in the list at the slected index equal to the edit forms instrumentToEdit object
            // STEP 3: This will only run aftyer the edit form (opened above) closes and the object in the edit form has been updated
            contractsList[selectedIndex] = myEditForm.itemToUpdate;
        }

        // STEP 2: Create a public object accessable from other forms
        public Contracts newContract = new Contracts();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Check for user input
            if (txtName.Text != "" && txtSurname.Text != "")
            {
                //Add new book entry to Binding list using an object with this form
                Contracts newContractEntry = new Contracts(txtName.Text, txtSurname.Text, txtDeviceName.Text, Convert.ToInt32(nupRemaining.Value), dateTimePicker1.Value.Date);
                contractsList.Add(newContractEntry);

            }
            else
            {
                MessageBox.Show("Please fill in all fields");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //check if a cell is selected
            if (dataGridView1.CurrentCell != null)
            {
                // STEP 4: Get the selected row index
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                // STEP 4: Create an instrument object and make it equal to the object in the list at the selected index
                Contracts contractToRemove = contractsList[selectedIndex];
                // STEP 4: Remove the object created above from the instrument list
                contractsList.Remove(contractToRemove);

            }
            else
            {
                //error message if no cell selected
                MessageBox.Show("Please make a selection");
            }
        }

        private void btnExtendContract_Click(object sender, EventArgs e)
        {
            //check if a cell is selected
            if (dataGridView1.CurrentCell != null)
            {
                //get the selected row index
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                //call the add vote button of the item in the list at the slected index
                contractsList[selectedIndex].ExtendContract();
                //set the selected cell to null
                dataGridView1.CurrentCell = null;
            }
            else
            {
                //error message if no cell selected
                MessageBox.Show("Please make a selection");
            }
        }
    }
}
