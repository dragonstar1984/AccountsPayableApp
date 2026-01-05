using AccountsPayableApp.Data;
using Google.Cloud.Firestore;
using System;
using System.Drawing;
using System.Windows.Forms;
using AccountsPayableApp.Data; 

namespace AccountsPayableApp.Forms
{
    public class PaymentListForm : Form
    {
        private readonly string _entryId;
        private DataGridView dgvPayments;

        public PaymentListForm(string entryId)
        {
            _entryId = entryId;
            InitializeControls();
            LoadPayments();
        }

        /// <summary>
        /// Initializes UI controls for displaying payments.
        /// </summary>
        private void InitializeControls()
        {
            this.Text = "Payments for Entry";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10F);

            dgvPayments = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvPayments.Columns.Add("Amount", "Amount");
            dgvPayments.Columns.Add("Date", "Date");
            dgvPayments.Columns.Add("AddedBy", "Added By");
            dgvPayments.Columns.Add("Notes", "Notes");

            this.Controls.Add(dgvPayments);
        }

        /// <summary>
        /// Loads payments from Firebase and populates the DataGridView.
        /// </summary>
        private async void LoadPayments()
        {
            try
            {
                var firebase = new FirebaseServiceLite
                    ("accountpagebug"); // Replace with actual project ID
                QuerySnapshot snapshot = await firebase.GetPaymentsForEntryAsync(_entryId);

                dgvPayments.Rows.Clear();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    var data = doc.ToDictionary();

                    string amount = data.ContainsKey("amount") ? data["amount"].ToString() : "";
                    string date = data.ContainsKey("date") ? data["date"].ToString() : "";
                    string addedBy = data.ContainsKey("addedBy") ? data["addedBy"].ToString() : "";
                    string notes = data.ContainsKey("notes") ? data["notes"].ToString() : "";

                    dgvPayments.Rows.Add(amount, date, addedBy, notes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load payments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
