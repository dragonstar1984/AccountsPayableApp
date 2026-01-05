using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;
using AccountsPayableApp.Helpers;

namespace AccountsPayableApp
{
    public class AddEntryForm : Form
    {
        private Label lblAmount;
        private TextBox txtAmount;
        private Label lblLink;
        private TextBox txtLink;
        private Button btnUploadPdf;
        private Button btnSaveEntry;

        public AddEntryForm()
        {
            InitializeCustomControls();
        }

        private void InitializeCustomControls()
        {
            // Form settings
            this.Text = "Add Ledger Entry";
            this.Size = new Size(500, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Label: Amount
            lblAmount = new Label
            {
                Name = "lblAmount",
                Text = "Amount:",
                Location = new Point(30, 30),
                Size = new Size(100, 25),
                AutoSize = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            this.Controls.Add(lblAmount);

            // TextBox: Amount
            txtAmount = new TextBox
            {
                Name = "txtAmount",
                Location = new Point(140, 30),
                Size = new Size(150, 25),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(txtAmount);

            // Label: PDF Link
            lblLink = new Label
            {
                Name = "lblLink",
                Text = "PDF Link:",
                Location = new Point(30, 70),
                Size = new Size(100, 25),
                AutoSize = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            this.Controls.Add(lblLink);

            // TextBox: PDF Link
            txtLink = new TextBox
            {
                Name = "txtLink",
                Location = new Point(140, 70),
                Size = new Size(300, 25),
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(txtLink);

            // Button: Upload PDF
            btnUploadPdf = new Button
            {
                Name = "btnUploadPdf",
                Text = "Upload PDF",
                Location = new Point(140, 110),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            btnUploadPdf.Click += btnUploadPdf_Click;
            this.Controls.Add(btnUploadPdf);

            // Button: Save Entry
            btnSaveEntry = new Button
            {
                Name = "btnSaveEntry",
                Text = "Save Entry",
                Location = new Point(140, 160),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            btnSaveEntry.Click += btnSaveEntry_Click;
            this.Controls.Add(btnSaveEntry);
        }

        private async void btnUploadPdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Select PDF to Upload"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(filePath);
                string entryId = Guid.NewGuid().ToString();

                try
                {
                    string bucketName = "accountpagebug.appspot.com"; // Replace with your actual bucket
                    string objectName = $"pdfs/{entryId}/{fileName}";

                    var storageClient = StorageClient.Create();
                    using var fileStream = File.OpenRead(filePath);

                    await storageClient.UploadObjectAsync(bucketName, objectName, "application/pdf", fileStream);

                    string publicUrl = $"https://storage.googleapis.com/{bucketName}/{objectName}";
                    txtLink.Text = publicUrl;

                    MessageBox.Show("PDF uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Upload failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnSaveEntry_Click(object sender, EventArgs e)
        {
            string amountText = txtAmount.Text.Trim();
            string pdfLink = txtLink.Text.Trim();

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Please enter a valid numeric amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(pdfLink))
            {
                MessageBox.Show("Please upload a PDF before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                FirestoreDb db = FirestoreDb.Create("accountpagebug"); // Replace with your actual project ID
                DocumentReference docRef = db.Collection("ledgerEntries").Document();

                Dictionary<string, object> entryData = new Dictionary<string, object>
                {
                    { "amount", amount },
                    { "pdfLink", pdfLink },
                    { "timestamp", Timestamp.GetCurrentTimestamp() },
                    { "createdBy", SessionManager.CurrentUserId } // ✅ Added for Firebase ownership tracking
                };

                await docRef.SetAsync(entryData);

                MessageBox.Show("Entry saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Text = "";
                txtLink.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save entry: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}