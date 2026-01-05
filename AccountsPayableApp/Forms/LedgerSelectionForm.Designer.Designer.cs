namespace AccountsPayableApp.Forms
{
    partial class LedgerSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // 🔹 Declare UI controls
        private Label lblSelectLedger;
        private ComboBox cmbLedgers;
        private Button btnContinue;
        private TextBox txtJoinCode;
        private Button btnJoin;
        private Button btnAddLedger;
        private TextBox txtLedgerName;
        private TextBox txtLedgerCode;
        private Button btnAddDemoInvoice;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // 🔧 Initialize UI controls
            lblSelectLedger = new Label();
            cmbLedgers = new ComboBox();
            btnContinue = new Button();
            txtJoinCode = new TextBox();
            btnJoin = new Button();
            btnAddLedger = new Button();
            txtLedgerName = new TextBox();
            txtLedgerCode = new TextBox();
            btnAddDemoInvoice = new Button();

            SuspendLayout();

            // 🔹 Shared font for buttons
            Font buttonFont = new Font("Segoe UI", 10F, FontStyle.Bold);

            // 🔹 Label: "Select a ledger"
            lblSelectLedger.AutoSize = true;
            lblSelectLedger.Location = new Point(30, 60);
            lblSelectLedger.Text = "Select a ledger:";

            // 🔹 ComboBox: Ledger list
            cmbLedgers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLedgers.Location = new Point(30, 90);
            cmbLedgers.Size = new Size(300, 31);

            // 🔹 TextBox: Join ledger by code
            txtJoinCode.Location = new Point(30, 20);
            txtJoinCode.Size = new Size(200, 30);
            txtJoinCode.Text = "Enter Ledger Code";

            // 🔹 Button: Join ledger
            btnJoin.Location = new Point(240, 20);
            btnJoin.Size = new Size(120, 40);
            btnJoin.Text = "Join Ledger";
            btnJoin.Font = buttonFont;

            // 🔹 Button: Continue to dashboard
            btnContinue.Location = new Point(30, 130);
            btnContinue.Size = new Size(120, 40);
            btnContinue.Text = "Continue";
            btnContinue.Font = buttonFont;

            // 🔹 Button: Add new ledger
            btnAddLedger.Location = new Point(30, 180);
            btnAddLedger.Size = new Size(120, 40);
            btnAddLedger.Text = "Add Ledger";
            btnAddLedger.Font = buttonFont;

            // 🔹 TextBox: Ledger name input
            txtLedgerName.Location = new Point(30, 230);
            txtLedgerName.Size = new Size(200, 30);
            txtLedgerName.PlaceholderText = "Ledger Name";

            // 🔹 TextBox: Ledger code input
            txtLedgerCode.Location = new Point(30, 270);
            txtLedgerCode.Size = new Size(200, 30);
            txtLedgerCode.PlaceholderText = "Ledger Code";

            // 🔹 Button: Add demo invoice
            btnAddDemoInvoice.Location = new Point(248, 230);
            btnAddDemoInvoice.Size = new Size(140, 60);
            btnAddDemoInvoice.Text = "Add Demo Invoice";
            btnAddDemoInvoice.Font = buttonFont;

            // 🔹 Add controls to form
            Controls.Add(lblSelectLedger);
            Controls.Add(cmbLedgers);
            Controls.Add(btnContinue);
            Controls.Add(txtJoinCode);
            Controls.Add(btnJoin);
            Controls.Add(btnAddLedger);
            Controls.Add(txtLedgerName);
            Controls.Add(txtLedgerCode);
            Controls.Add(btnAddDemoInvoice);

            // 🔹 Form settings
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 340);
            Font = new Font("Segoe UI", 10F);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Select Ledger";

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}