using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MedioSoft
{
    public partial class LoginForm : Form
    {

        
        public LoginForm()
        {
            InitializeComponent();
        }

        
        //connection string
        SqlConnection con = new SqlConnection("Data Source=LAJIM\\SQLEXPRESS;Initial Catalog=mediosoft;Integrated Security=True");

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string welcomeUser = loginUsername.Text;


            //application default login authentication
            if (loginUsername.Text == "admin" && loginPass.Text == "admin")
            {
                Admin ad = new Admin(welcomeUser);
                ad.Show();
                this.Hide();

                //sending username to ManageMedicines Form
                ManageMedicines mm = new ManageMedicines(welcomeUser);
            }
            else
            {
                //database user login authentication
                string loginUserName = loginUsername.Text;
                string loginPassword = loginPass.Text;

                string query = "SELECT user_role FROM Users WHERE user_name = '"+ loginUserName + "' AND password = '"+ loginPassword + "'";

                

                SqlDataAdapter sda = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                sda.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    string userRole = dt.Rows[0]["user_role"].ToString();

                    if (userRole == "Admin")
                    {
                        Admin ad = new Admin(welcomeUser);  //sending username to admin form
                        ad.Show();
                        this.Hide();



                    }
                    else if(userRole == "Pharmacist")
                    {
                        Pharmacists ph = new Pharmacists(welcomeUser);
                        ph.Show();
                        this.Hide();
                    }
                    else if(userRole == "Customer")
                    {
                        Customers ct = new Customers(welcomeUser);
                        ct.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Login Credentials!");
                    }
                    
                }


                

            }




        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            loginUsername.Clear();
            loginPass.Clear();
        }

        private void btn_user_registration_Click(object sender, EventArgs e)
        {
            
            uesrsRegistration ur = new uesrsRegistration();
            ur.Show();
            this.Hide();
        }

        private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }
    }
}
