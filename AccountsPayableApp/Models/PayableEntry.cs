using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsPayableApp.Models
{
    public class PayableEntry
    {
        public int Id { get; set; } // Unique ID of the entry
        public DateTime Date { get; set; } // Date of the record
        public string Link { get; set; } // Associated link (e.g., invoice)
        public decimal Amount { get; set; } // Amount to be paid
        public string CreatedBy { get; set; } // User who created the entry
        public int LedgerId { get; set; } // Foreign key linking the entry to a specific ledger

        // 🔹 Payments logic
        public List<Payment> Payments { get; set; } = new List<Payment>();

        public decimal TotalPaid => Payments.Sum(p => p.Amount);
        public decimal RemainingBalance => Amount - TotalPaid;
        public bool IsFullyPaid => RemainingBalance <= 0;

        // 🔹 Manual override for payment status
        public bool IsPaid { get; set; } // True if manually marked as paid
    }
}