namespace AccountsPayableApp.Forms 
{
    partial class LoginForm : Form 
    {
        // Required designer variable
        private System.ComponentModel.IContainer components = null;

        #region Release resources
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lblError = new Label();
            lblTitle = new Label();
            lblNoAccount = new Label();
            linkCreateAccount = new LinkLabel();
            pnLoginContainer = new Panel();
            picTogglePassword = new PictureBox();
            pnLoginContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picTogglePassword).BeginInit();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10.2F);
            txtEmail.ForeColor = Color.Black;
            txtEmail.Location = new Point(33, 60);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(253, 30);
            txtEmail.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10.2F);
            txtPassword.ForeColor = SystemColors.WindowText;
            txtPassword.Location = new Point(33, 110);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(253, 30);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnLogin.Location = new Point(33, 170);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(253, 40);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblError
            // 
            lblError.Font = new Font("Segoe UI", 9F);
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(33, 220);
            lblError.Name = "lblError";
            lblError.Size = new Size(253, 33);
            lblError.TabIndex = 3;
            lblError.Visible = false;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(309, 47);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Login to your account";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNoAccount
            // 
            lblNoAccount.AutoSize = true;
            lblNoAccount.Font = new Font("Segoe UI", 9F);
            lblNoAccount.ForeColor = Color.White;
            lblNoAccount.Location = new Point(33, 270);
            lblNoAccount.Name = "lblNoAccount";
            lblNoAccount.Size = new Size(163, 20);
            lblNoAccount.TabIndex = 5;
            lblNoAccount.Text = "Don't have an account?";
            lblNoAccount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linkCreateAccount
            // 
            linkCreateAccount.AutoSize = true;
            linkCreateAccount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            linkCreateAccount.LinkBehavior = LinkBehavior.HoverUnderline;
            linkCreateAccount.LinkColor = Color.DeepSkyBlue;
            linkCreateAccount.Location = new Point(190, 270);
            linkCreateAccount.Name = "linkCreateAccount";
            linkCreateAccount.Size = new Size(113, 20);
            linkCreateAccount.TabIndex = 6;
            linkCreateAccount.TabStop = true;
            linkCreateAccount.Text = "Create account";
            linkCreateAccount.TextAlign = ContentAlignment.MiddleLeft;
            linkCreateAccount.LinkClicked += linkCreateAccount_LinkClicked;
            // 
            // pnLoginContainer
            // 
            pnLoginContainer.Anchor = AnchorStyles.None;
            pnLoginContainer.BackColor = Color.Transparent;
            pnLoginContainer.Controls.Add(lblTitle);
            pnLoginContainer.Controls.Add(txtEmail);
            pnLoginContainer.Controls.Add(txtPassword);
            pnLoginContainer.Controls.Add(picTogglePassword);
            pnLoginContainer.Controls.Add(btnLogin);
            pnLoginContainer.Controls.Add(lblError);
            pnLoginContainer.Controls.Add(lblNoAccount);
            pnLoginContainer.Controls.Add(linkCreateAccount);
            pnLoginContainer.Location = new Point(316, 92);
            pnLoginContainer.Name = "pnLoginContainer";
            pnLoginContainer.Size = new Size(309, 346);
            pnLoginContainer.TabIndex = 7;
            // 
            // picTogglePassword
            // 
            picTogglePassword.Cursor = Cursors.Hand;
            picTogglePassword.Image = Properties.Resources.hidden;
            picTogglePassword.Location = new Point(262, 115);
            picTogglePassword.Name = "picTogglePassword";
            picTogglePassword.Size = new Size(24, 24);
            picTogglePassword.SizeMode = PictureBoxSizeMode.Zoom;
            picTogglePassword.TabIndex = 7;
            picTogglePassword.TabStop = false;
            picTogglePassword.Click += picTogglePassword_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(970, 450);
            Controls.Add(pnLoginContainer);
            Name = "LoginForm";
            Text = "Login";
            Load += LoginForm_Load;
            pnLoginContainer.ResumeLayout(false);
            pnLoginContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picTogglePassword).EndInit();
            ResumeLayout(false);
        }
        #endregion

        // Control declarations
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNoAccount;
        private System.Windows.Forms.LinkLabel linkCreateAccount;
        private System.Windows.Forms.Panel pnLoginContainer;
        private System.Windows.Forms.PictureBox picTogglePassword;
    }
}