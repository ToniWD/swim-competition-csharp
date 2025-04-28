using Service.Utils;
using log4net;
using System;
using System.Windows.Forms;
using Service.Interfaces;
using Model.Domain;

namespace UI
{
    public partial class LoginForm : Form, IObserver
    {
        private IServices services;
        private static readonly ILog logger = LogManager.GetLogger(typeof(LoginForm));

        public LoginForm(IServices services)
        {
            InitializeComponent();
            this.services = services;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {

                if (services.login(username, password, this))
                {
                    usernameTextBox.Text = "";
                    passwordTextBox.Text = "";
                    this.Hide();
                    User user = new User(username,password);
                    MainPageForm mainPageForm = new MainPageForm(this, services, user);
                    services.setClient(mainPageForm);
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
