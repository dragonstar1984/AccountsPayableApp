using System;

namespace AccountsPayableApp.Models
{
    /// <summary>
    /// Represents a transaction or detail entry within a ledger.
    /// </summary>
    public class LedgerDetail
    {
        /// <summary>
        /// Primary key from LedgerDetails table.
        /// </summary>
        public int DetailId { get; set; }

        /// <summary>
        /// Foreign key linking to the parent Ledger.
        /// </summary>
        public int LedgerId { get; set; }

        /// <summary>
        /// Date of the transaction.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Merchant or source name.
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// Amount of the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Optional notes or comments.
        /// </summary>
        public string Notes { get; set; }

        public string Description { get; set; }
    }
}

