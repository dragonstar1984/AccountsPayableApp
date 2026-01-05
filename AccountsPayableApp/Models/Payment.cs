namespace AccountsPayableApp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int EntryId { get; set; } // legătură cu factura
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string AddedBy { get; set; } // emailul utilizatorului
        public string Notes { get; set; } // opțional: motiv, referință, etc.
    }
}
