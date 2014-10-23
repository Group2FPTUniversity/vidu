using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace MainForm
{
    public partial class Login : Form
    {
        static String userName = "";
        String passWords = "";
        SqlConnection con = new SqlConnection();
        public Login()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            con.Open();
            //check();
        }
        
        private bool check()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "login_User";
                SqlParameter param = new SqlParameter("@user", txt_user.Text);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@pass", txt_passWords.Text);
                cmd.Parameters.Add(param);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    userName = dr[0].ToString();
                    passWords = dr[1].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(passWords))
            {
                
                return true;
            }
            return false;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (check())
            {
                Main m = new Main();
                m.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("User or Passwords incorrect!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static String getUser()
        {
            return userName;
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    
    }
}
