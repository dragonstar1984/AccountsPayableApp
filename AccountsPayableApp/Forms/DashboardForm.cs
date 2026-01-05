using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountsPayableApp.Data;
using AccountsPayableApp.Models;
using System.IO;

namespace AccountsPayableApp.Forms
{
    public partial class DashboardForm : Form
    {
        private int activeLedgerId; // Stores the currently selected ledger ID
        private string code;

        public DashboardForm(int ledgerId)
        {
            InitializeComponent();
            activeLedgerId = ledgerId;

            // Load entries for the selected ledger
            LoadEntriesByLedger();

            // Connect cell formatting event for visual highlighting
            dgvEntries.CellFormatting += dgvEntries_CellFormatting;
        }

        public DashboardForm(string code)
        {
            this.code = code;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            LoadEntriesByLedger();
        }

        /// <summary>
        /// Loads payable entries that belong to the active ledger.
        /// </summary>
        private void LoadEntriesByLedger()
        {
            try
            {
                var entries = Database.GetEntriesByLedger(activeLedgerId);
                dgvEntries.DataSource = entries;

                // ✅ Actualizează sumarul neplătit
                int unpaidCount = entries.Count(e => !e.IsPaid);
                decimal unpaidTotal = entries.Where(e => !e.IsPaid).Sum(e => e.Amount);

                labelUnpaidCount.Text = $"Unpaid Entries: {unpaidCount}";
                labelUnpaidTotal.Text = $"Total Unpaid: {unpaidTotal:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading entries: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadEntriesByLedger();
        }

        /// <summary>
        /// Highlights rows with large amounts and paid status.
        /// </summary>
        private void dgvEntries_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEntries.Columns[e.ColumnIndex].Name == "Amount")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal amount))
                {
                    if (amount > 1000)
                    {
                        dgvEntries.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        dgvEntries.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvEntries.Font, FontStyle.Bold);
                    }
                }
            }

            if (dgvEntries.Columns[e.ColumnIndex].Name == "IsPaid")
            {
                if (e.Value != null && (bool)e.Value == true)
                {
                    dgvEntries.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dgvEntries.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }

        /// <summary>
        /// Imports entries from a CSV file and saves them to the database.
        /// </summary>
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var lines = File.ReadAllLines(openFileDialog.FileName);
                    var entries = new List<PayableEntry>();

                    foreach (var line in lines.Skip(1)) // skip header
                    {
                        var parts = line.Split(',');

                        if (parts.Length >= 4)
                        {
                            entries.Add(new PayableEntry
                            {
                                Date = DateTime.Parse(parts[0]),
                                Link = parts[1],
                                Amount = decimal.Parse(parts[2]),
                                CreatedBy = parts[3],
                                LedgerId = activeLedgerId
                            });
                        }
                    }

                    Database.BulkInsertEntries(entries);
                    MessageBox.Show("Entries imported successfully.");
                    LoadEntriesByLedger();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error importing entries: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Filters entries by selected status (Paid/Unpaid/All).
        /// </summary>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                var entries = Database.GetEntriesByLedger(activeLedgerId);

                // Filter by month
                string selectedMonth = cmbMonth.SelectedItem.ToString();
                if (selectedMonth != "All")
                {
                    int monthNumber = cmbMonth.SelectedIndex;
                    entries = entries.Where(e => e.Date.Month == monthNumber).ToList();
                }

                // Filter by minimum amount
                decimal minAmount = numMinAmount.Value;
                entries = entries.Where(e => e.Amount >= minAmount).ToList();

                // Filter by status
                string selectedStatus = cmbStatus.SelectedItem.ToString();
                if (selectedStatus == "Paid")
                {
                    entries = entries.Where(e => e.IsPaid).ToList();
                }
                else if (selectedStatus == "Unpaid")
                {
                    entries = entries.Where(e => !e.IsPaid).ToList();
                }

                dgvEntries.DataSource = entries;

                // ✅ Actualizează sumarul neplătit
                int unpaidCount = entries.Count(e => !e.IsPaid);
                decimal unpaidTotal = entries.Where(e => !e.IsPaid).Sum(e => e.Amount);

                labelUnpaidCount.Text = $"Unpaid Entries: {unpaidCount}";
                labelUnpaidTotal.Text = $"Total Unpaid: {unpaidTotal:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering entries: " + ex.Message);
            }
        }
    }
}