namespace AccountsPayableApp.Forms 
{
    partial class AddPaymentForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Initializes all UI controls and layout for the AddPaymentForm.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxEntries = new System.Windows.Forms.ComboBox();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelEntry = new System.Windows.Forms.Label();
            this.labelAmount = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelNotes = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // comboBoxEntries
            this.comboBoxEntries.FormattingEnabled = true;
            this.comboBoxEntries.Location = new System.Drawing.Point(140, 30);
            this.comboBoxEntries.Name = "comboBoxEntries";
            this.comboBoxEntries.Size = new System.Drawing.Size(200, 24);

            // textBoxAmount
            this.textBoxAmount.Location = new System.Drawing.Point(140, 70);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(200, 22);

            // dateTimePicker
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(140, 110);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 22);

            // textBoxNotes
            this.textBoxNotes.Location = new System.Drawing.Point(140, 150);
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(200, 60);
            this.textBoxNotes.Multiline = true;

            // buttonSave
            this.buttonSave.Location = new System.Drawing.Point(140, 220);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(200, 30);
            this.buttonSave.Text = "Save Payment";
            this.buttonSave.UseVisualStyleBackColor = true;

            // 🔗 Link the Save button to its click event handler
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);

            // labelEntry
            this.labelEntry.AutoSize = true;
            this.labelEntry.Location = new System.Drawing.Point(30, 33);
            this.labelEntry.Name = "labelEntry";
            this.labelEntry.Size = new System.Drawing.Size(90, 17);
            this.labelEntry.Text = "Select Entry:";

            // labelAmount
            this.labelAmount.AutoSize = true;
            this.labelAmount.Location = new System.Drawing.Point(30, 73);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(104, 17);
            this.labelAmount.Text = "Payment Amount:";

            // labelDate
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(30, 113);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(94, 17);
            this.labelDate.Text = "Payment Date:";

            // labelNotes
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(30, 153);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(112, 17);
            this.labelNotes.Text = "Notes (optional):";

            // AddPaymentForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 280);
            this.Controls.Add(this.comboBoxEntries);
            this.Controls.Add(this.textBoxAmount);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelEntry);
            this.Controls.Add(this.labelAmount);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelNotes);
            this.Name = "AddPaymentForm";
            this.Text = "Add Payment";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // 🔧 Control declarations
        private System.Windows.Forms.ComboBox comboBoxEntries;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelEntry;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelNotes;
    }
}