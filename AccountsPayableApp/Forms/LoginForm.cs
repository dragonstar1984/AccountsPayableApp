using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AccountsPayableApp.Data;
using AccountsPayableApp.Helpers;
using AccountsPayableApp.Forms;

namespace AccountsPayableApp.Forms
{
    public partial class LoginForm : Form
    {
        private bool isPasswordVisible = false;

        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
            this.Paint += new PaintEventHandler(LoginForm_Paint);
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            };
        }

        private void CenterUI()
        {
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int x = (screen.Width - this.Width) / 2;
            int y = (screen.Height - this.Height) / 2;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CenterUI();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            ResetFields();

            txtEmail.Text = "Email address";
            txtEmail.ForeColor = Color.Gray;
            txtEmail.GotFocus += txtEmail_GotFocus;
            txtEmail.LostFocus += txtEmail_LostFocus;
            txtEmail.KeyPress += txtEmail_KeyPress;

            txtPassword.Text = "Password";
            txtPassword.ForeColor = Color.Gray;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.GotFocus += txtPassword_GotFocus;
            txtPassword.LostFocus += txtPassword_LostFocus;
            txtPassword.KeyPress += txtPassword_KeyPress;

            
            picTogglePassword.Visible = true;
            picTogglePassword.Size = new Size(24, 24);
            picTogglePassword.BackColor = Color.Transparent;
            picTogglePassword.BringToFront();

           
            int iconX = txtPassword.Right - picTogglePassword.Width - 5;
            int iconY = txtPassword.Top + (txtPassword.Height - picTogglePassword.Height) / 2;
            picTogglePassword.Location = new Point(iconX, iconY);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (!ValidateInputs(email, password))
                return;

            if (Authenticate(email, password))
            {
                lblError.Visible = false;
                MessageBox.Show("Login successful!", "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NavigateToDashboard();
            }
            else
            {
                lblError.Text = "Invalid credentials.";
                lblError.Visible = true;
            }
        }

        private async void btnLogin_Click_Async(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (!ValidateInputs(email, password))
                return;

            var result = await FirebaseAuthService.AuthenticateAsync(email, password);

            if (result.success)
            {
                SessionManager.CurrentUserEmail = email;
                SessionManager.CurrentUserId = result.localId;
                SessionManager.CurrentUserToken = result.idToken;

                MessageBox.Show("Login successful via Firebase!", "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NavigateToDashboard();
            }
            else
            {
                lblError.Text = "Firebase login failed:\n" + result.errorMessage;
                lblError.Visible = true;
            }
        }

        private void linkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Redirecting to account creation...", "Create Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OpenRegistrationForm();
        }

        private bool ValidateInputs(string email, string password)
        {
            if (email == "Email address" || password == "Password" ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Please enter both email and pwd";
                lblError.Visible = true;
                return false;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                lblError.Text = "Please enter a valid email address.";
                lblError.Visible = true;
                return false;
            }

            return true;
        }

        private bool Authenticate(string email, string password)
        {
            return Database.ValidateUser(email, password);
        }

        private void NavigateToDashboard()
        {
            SessionManager.CurrentUserEmail = txtEmail.Text.Trim();
            LedgerSelectionForm selectionForm = new LedgerSelectionForm(SessionManager.CurrentUserEmail);
            selectionForm.Show();
            this.Hide();
        }

        private void OpenRegistrationForm()
        {
            // TODO: Replace with actual registration logic
            // Example: new RegisterForm().Show(); this.Hide();
        }

        private void ResetFields()
        {
            txtEmail.Text = "";
            txtPassword.Text = "";
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                Color.MediumSlateBlue, Color.DeepSkyBlue, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void txtEmail_GotFocus(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email address")
            {
                txtEmail.SelectionStart = 0;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmail.Text == "Email address")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void txtEmail_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Text = "Email address";
                txtEmail.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_GotFocus(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.SelectionStart = 0;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Gray;
            }
        }

        private void picTogglePassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtPassword.UseSystemPasswordChar = !isPasswordVisible;

            picTogglePassword.Image = isPasswordVisible
                ? Properties.Resources.eye
                : Properties.Resources.hidden;
        }
    }
}