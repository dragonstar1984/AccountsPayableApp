using System;

namespace AccountsPayableApp.Models
{
    public class Ledger
    {
        public int Id { get; set; }                  // Primary key
        public string Name { get; set; }             // Ledger name
        public string Code { get; set; }             // Unique join code
        public string OwnerEmail { get; set; }       // Email of creator
        public DateTime CreatedAt { get; set; }      // Creation timestamp
        public bool IsArchived { get; set; }         // Archive flag
    }
}