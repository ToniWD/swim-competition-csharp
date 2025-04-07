using Concurs_Inot_WinForms.Service;
using Concurs_Inot_WinForms.Service.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concurs_Inot_WinForms.UI
{
    public partial class LoginForm : Form
    {
        private IAuthService authService;
        private IMainService mainService;
        private static readonly ILog logger = LogManager.GetLogger(typeof(LoginForm));

        public LoginForm(IAuthService authService, IMainService mainService)
        {
            InitializeComponent();
            this.authService = authService;
            this.mainService = mainService;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                if (authService.authentificate(username, password))
                {
                    usernameTextBox.Text = "";
                    passwordTextBox.Text = "";
                    this.Hide();
                    MainPageForm mainPageForm = new MainPageForm(this, mainService);
                    mainPageForm.Show();
                }
                else
                {
                    MessageBox.Show("Username or Password incorrect.");
                }
            }
            catch (ServiceException ex)
            {
                MessageBox.Show("Username or Password incorrect.");
                logger.Error(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.");
                logger.Error(ex);
            }


        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}
