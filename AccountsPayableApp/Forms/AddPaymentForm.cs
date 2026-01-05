using AccountsPayableApp.Data;
using AccountsPayableApp.Helpers;
using AccountsPayableApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AccountsPayableApp.Data;

namespace AccountsPayableApp.Forms
{
    public partial class AddPaymentForm : Form
    {
        public AddPaymentForm()
        {
            InitializeComponent();

            LoadEntries(); // Populate ComboBox with real entries
            buttonSave.Click += buttonSave_Click;
            this.Load += AddPaymentForm_Load; // Placeholder behavior
        }

        /// <summary>
        /// Populates comboBoxEntries with real entries from the database.
        /// </summary>
        private void LoadEntries()
        {
            List<PayableEntry> entries = Database.GetEntries();

            comboBoxEntries.Items.Clear();

            foreach (var entry in entries)
            {
                string displayText = $"#{entry.Id} - {entry.Amount:C} - {entry.Date:dd/MM/yyyy}";
                comboBoxEntries.Items.Add(new ComboBoxItem
                {
                    Text = displayText,
                    Value = entry.Id
                });
            }

            if (comboBoxEntries.Items.Count > 0)
                comboBoxEntries.SelectedIndex = 0;
        }

        /// <summary>
        /// Simulates placeholder text behavior for the amount textbox.
        /// </summary>
        private void AddPaymentForm_Load(object sender, EventArgs e)
        {
            textBoxAmount.Text = "Enter amount...";
            textBoxAmount.ForeColor = Color.Gray;

            textBoxAmount.GotFocus += (s, args) =>
            {
                if (textBoxAmount.Text == "Enter amount...")
                {
                    textBoxAmount.Text = "";
                    textBoxAmount.ForeColor = Color.Black;
                }
            };

            textBoxAmount.LostFocus += (s, args) =>
            {
                if (string.IsNullOrWhiteSpace(textBoxAmount.Text))
                {
                    textBoxAmount.Text = "Enter amount...";
                    textBoxAmount.ForeColor = Color.Gray;
                }
            };
        }

        /// <summary>
        /// Handles Save button click: validates input and saves payment to Firebase.
        /// </summary>
        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxEntries.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an entry.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBoxAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid payment amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ComboBoxItem selectedItem = comboBoxEntries.SelectedItem as ComboBoxItem;
            int entryId = selectedItem?.Value ?? 0;

            DateTime paymentDate = dateTimePicker.Value;
            string notes = textBoxNotes.Text;

            var firebase = new FirebaseServiceLite
                ("accountpagebug"); // 🔧 Replace with actual project ID
            await firebase.AddPaymentAsync((double)amount, entryId.ToString(), SessionManager.CurrentUserId, notes);

            MessageBox.Show("Payment saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }

    /// <summary>
    /// Helper class for ComboBox binding: displays text, stores entry ID.
    /// </summary>
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}