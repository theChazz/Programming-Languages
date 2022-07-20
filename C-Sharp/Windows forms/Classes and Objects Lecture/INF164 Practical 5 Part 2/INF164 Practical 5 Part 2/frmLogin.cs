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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password = "Admin";

            if (password == txtPassword.Text)
            {
                frmCellularExpress open = new frmCellularExpress();
                open.ShowDialog();
            }
            else
            { MessageBox.Show("Incorrect Password! Try again."); }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
