namespace AccountsPayableApp.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        // 🔧 UI Controls
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.DataGridView dgvEntries;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.NumericUpDown numMinAmount;
        private System.Windows.Forms.Label labelUnpaidCount;
        private System.Windows.Forms.Label labelUnpaidTotal;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.dgvEntries = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.numMinAmount = new System.Windows.Forms.NumericUpDown();
            this.labelUnpaidCount = new System.Windows.Forms.Label();
            this.labelUnpaidTotal = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAmount)).BeginInit();
            this.SuspendLayout();

            // topPanel
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 60;
            this.topPanel.BackColor = System.Drawing.Color.LightGray;

            // bottomPanel
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Height = 40;
            this.bottomPanel.BackColor = System.Drawing.Color.WhiteSmoke;

            // dgvEntries
            this.dgvEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEntries.AllowUserToAddRows = false;
            this.dgvEntries.ReadOnly = true;

            // btnRefresh
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new System.Drawing.Point(10, 15);
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // btnImport
            this.btnImport.Text = "Import CSV";
            this.btnImport.Location = new System.Drawing.Point(100, 15);
            this.btnImport.Size = new System.Drawing.Size(100, 30);
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

            // btnFilter
            this.btnFilter.Text = "Apply Filter";
            this.btnFilter.Location = new System.Drawing.Point(210, 15);
            this.btnFilter.Size = new System.Drawing.Size(100, 30);
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);

            // cmbMonth
            this.cmbMonth.Location = new System.Drawing.Point(320, 15);
            this.cmbMonth.Size = new System.Drawing.Size(100, 30);
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.Items.AddRange(new object[] {
                "All", "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            });

            // cmbStatus
            this.cmbStatus.Location = new System.Drawing.Point(430, 15);
            this.cmbStatus.Size = new System.Drawing.Size(100, 30);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new object[] {
                "All", "Paid", "Unpaid"
            });

            // numMinAmount
            this.numMinAmount.Location = new System.Drawing.Point(540, 15);
            this.numMinAmount.Size = new System.Drawing.Size(80, 30);
            this.numMinAmount.Minimum = 0;
            this.numMinAmount.Maximum = 100000;

            // labelUnpaidCount
            this.labelUnpaidCount.Text = "Unpaid Entries: 0";
            this.labelUnpaidCount.Location = new System.Drawing.Point(10, 10);
            this.labelUnpaidCount.AutoSize = true;

            // labelUnpaidTotal
            this.labelUnpaidTotal.Text = "Total Unpaid: 0.00";
            this.labelUnpaidTotal.Location = new System.Drawing.Point(200, 10);
            this.labelUnpaidTotal.AutoSize = true;

            // Add controls to topPanel
            this.topPanel.Controls.Add(this.btnRefresh);
            this.topPanel.Controls.Add(this.btnImport);
            this.topPanel.Controls.Add(this.btnFilter);
            this.topPanel.Controls.Add(this.cmbMonth);
            this.topPanel.Controls.Add(this.cmbStatus);
            this.topPanel.Controls.Add(this.numMinAmount);

            // Add controls to bottomPanel
            this.bottomPanel.Controls.Add(this.labelUnpaidCount);
            this.bottomPanel.Controls.Add(this.labelUnpaidTotal);

            // Add panels to form
            this.Controls.Add(this.dgvEntries);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);

            // Form settings
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "DashboardForm";
            this.Text = "Dashboard";

            ((System.ComponentModel.ISupportInitialize)(this.dgvEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAmount)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}