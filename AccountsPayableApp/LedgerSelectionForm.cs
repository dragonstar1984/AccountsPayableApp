using AccountsPayableApp.Data;
using AccountsPayableApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountsPayableApp.Forms
{
    public partial class LedgerSelectionForm : Form
    {
        private readonly string _userEmail;
        private FirebaseServiceLite firebase;

        public LedgerSelectionForm(string userEmail)
        {
            _userEmail = userEmail;
            firebase = new FirebaseServiceLite("accountspayableapp");
            InitializeComponent();
        }

        private async void LedgerSelectionForm_Load(object sender, EventArgs e)
        {
            try
            {
                var ledgers = await firebase.GetLedgersForUserAsync(_userEmail);

                // 🔹 Automatically add demo ledger if none exists
                if (ledgers.Count == 0 && _userEmail == "testuser@app.com")
                {
                    await firebase.AddLedgerAsync(
                        name: "Demo Ledger",
                        code: "demo-testuser",
                        ownerEmail: _userEmail,
                        isArchived: false
                    );

                    MessageBox.Show("Demo ledger created automatically for test user.");
                }

                // 🔹 Reload ledgers after addition
                await LoadLedgers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during ledger initialization: " + ex.Message);
            }
        }

        private async Task LoadLedgers()
        {
            try
            {
                var ledgers = await firebase.GetLedgersForUserAsync(_userEmail);

                if (ledgers == null || ledgers.Count == 0)
                {
                    MessageBox.Show("No ledgers found for this user: " + _userEmail);
                    cmbLedgers.Enabled = false;
                    btnContinue.Enabled = false;
                    return;
                }

                cmbLedgers.DataSource = ledgers;
                cmbLedgers.DisplayMember = "Name";
                cmbLedgers.ValueMember = "Code";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ledgers: " + ex.Message);
            }
        }

        private async void btnAddLedger_Click(object sender, EventArgs e)
        {
            string name = txtLedgerName.Text.Trim();
            string code = txtLedgerCode.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Please enter both name and code.", "Missing Data");
                return;
            }

            try
            {
                await firebase.AddLedgerAsync(name, code, _userEmail, false);
                MessageBox.Show("Ledger added successfully!");
                await LoadLedgers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding ledger: " + ex.Message);
            }
        }

        private async void btnAddDemoInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                await firebase.AddDemoInvoiceAsync();
                MessageBox.Show("Demo invoice added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding demo invoice: " + ex.Message);
            }
        }

        private async void btnJoin_Click(object sender, EventArgs e)
        {
            string joinCode = txtJoinCode.Text.Trim();

            if (string.IsNullOrWhiteSpace(joinCode))
            {
                MessageBox.Show("Please enter a ledger code.");
                return;
            }

            try
            {
                var ledger = await firebase.GetLedgerByCodeAsync(joinCode);

                if (ledger == null)
                {
                    MessageBox.Show("Ledger not found.");
                    return;
                }

                cmbLedgers.Items.Add(ledger);
                cmbLedgers.SelectedItem = ledger;
                MessageBox.Show("Ledger joined successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error joining ledger: " + ex.Message);
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (cmbLedgers.SelectedItem is Ledger selectedLedger)
            {
                DashboardForm dashboard = new DashboardForm(selectedLedger.Code);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a ledger.");
            }
        }
    }
}