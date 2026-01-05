using AccountsPayableApp.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountsPayableApp.Data
{
    public class FirebaseServiceLite
    {
        private readonly FirestoreDb _firestore;

        // 🔧 Constructor: initialize Firestore connection
        public FirebaseServiceLite(string projectId)
        {
            string keyPath = Path.Combine(Application.StartupPath, "firebase-access.json");

            if (!File.Exists(keyPath))
                throw new FileNotFoundException("Firebase credential file not found at: " + keyPath);

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyPath);
            _firestore = FirestoreDb.Create(projectId);
        }

        // 🔍 Get a single Ledger by its document code
        public async Task<Ledger?> GetLedgerByCodeAsync(string code)
        {
            DocumentReference docRef = _firestore.Collection("ledgers").Document(code);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return null;

            var data = snapshot.ToDictionary();
            return new Ledger
            {
                Code = data["code"]?.ToString(),
                Name = data["name"]?.ToString(),
                OwnerEmail = data["ownerEmail"]?.ToString(),
                IsArchived = Convert.ToBoolean(data["isArchived"]),
                CreatedAt = snapshot.CreateTime?.ToDateTime() ?? DateTime.Now
            };
        }

        public async Task AddLedgerAsync(string name, string code, string ownerEmail, bool isArchived)
        {
            DocumentReference docRef = _firestore.Collection("ledgers").Document(code);
            Dictionary<string, object> data = new()
            {
                { "name", name },
                { "code", code },
                { "ownerEmail", ownerEmail },
                { "isArchived", isArchived },
                { "createdAt", Timestamp.GetCurrentTimestamp() }
            };
            await docRef.SetAsync(data);
        }

        public async Task AddEntryAsync(DateTime date, double amount, string link, string createdBy, string ledgerId)
        {
            DocumentReference docRef = _firestore.Collection("entries").Document();
            Dictionary<string, object> data = new()
            {
                { "amount", amount },
                { "link", link },
                { "createdBy", createdBy },
                { "ledgerId", ledgerId },
                { "date", Timestamp.FromDateTime(date.ToUniversalTime()) }
            };
            await docRef.SetAsync(data);
        }

        public async Task AddDemoInvoiceAsync()
        {
            await AddEntryAsync(
                DateTime.Now,
                1200.00,
                "https://example.com/inv-usa-001.pdf",
                "Dragos",
                "ledger-usa-demo"
            );
        }

        public async Task AddPaymentAsync(double amount, string entryId, string addedBy, string notes)
        {
            DocumentReference docRef = _firestore.Collection("payments").Document();
            Dictionary<string, object> data = new()
            {
                { "amount", amount },
                { "entryId", entryId },
                { "addedBy", addedBy },
                { "notes", notes },
                { "date", Timestamp.GetCurrentTimestamp() }
            };
            await docRef.SetAsync(data);
        }

        public async Task AddUserAsync(string email, string name, string role)
        {
            DocumentReference docRef = _firestore.Collection("users").Document();
            Dictionary<string, object> data = new()
            {
                { "email", email },
                { "name", name },
                { "role", role }
            };
            await docRef.SetAsync(data);
        }

        public async Task<List<Ledger>> GetLedgersForUserAsync(string userEmail)
        {
            Query query = _firestore.Collection("ledgers").WhereEqualTo("ownerEmail", userEmail);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<Ledger> ledgers = new();
            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    var data = doc.ToDictionary();
                    ledgers.Add(new Ledger
                    {
                        Code = data["code"]?.ToString(),
                        Name = data["name"]?.ToString(),
                        OwnerEmail = data["ownerEmail"]?.ToString(),
                        IsArchived = Convert.ToBoolean(data["isArchived"]),
                        CreatedAt = doc.CreateTime?.ToDateTime() ?? DateTime.Now
                    });
                }
            }
            return ledgers;
        }

        public async Task<QuerySnapshot> GetEntriesForLedgerAsync(string ledgerId)
        {
            Query query = _firestore.Collection("entries").WhereEqualTo("ledgerId", ledgerId);
            return await query.GetSnapshotAsync();
        }

        public async Task<QuerySnapshot> GetPaymentsForEntryAsync(string entryId)
        {
            Query query = _firestore.Collection("payments").WhereEqualTo("entryId", entryId);
            return await query.GetSnapshotAsync();
        }

        public async Task<QuerySnapshot> GetEntriesByUserAsync(string userId)
        {
            Query query = _firestore.Collection("entries").WhereEqualTo("createdBy", userId);
            return await query.GetSnapshotAsync();
        }

        public async Task<QuerySnapshot> GetEntriesForLedgerByUserAsync(string ledgerId, string userId)
        {
            Query query = _firestore.Collection("entries")
                .WhereEqualTo("ledgerId", ledgerId)
                .WhereEqualTo("createdBy", userId);
            return await query.GetSnapshotAsync();
        }

        public async Task<double> GetTotalPaymentsForEntryAsync(string entryId)
        {
            var query = _firestore.Collection("payments").WhereEqualTo("entryId", entryId);
            var snapshot = await query.GetSnapshotAsync();

            double total = 0;
            foreach (var doc in snapshot.Documents)
            {
                if (doc.TryGetValue("amount", out double amount))
                    total += amount;
            }
            return total;
        }

        public async Task<(int count, double total)> GetUnpaidSummaryAsync(string userId)
        {
            var query = _firestore.Collection("entries")
                .WhereEqualTo("createdBy", userId)
                .WhereEqualTo("isPaid", false);
            var snapshot = await query.GetSnapshotAsync();

            int count = snapshot.Count;
            double total = 0;
            foreach (var doc in snapshot.Documents)
            {
                if (doc.TryGetValue("amount", out double amount))
                    total += amount;
            }
            return (count, total);
        }

        public async Task<QuerySnapshot> GetAllUsersAsync()
        {
            return await _firestore.Collection("users").GetSnapshotAsync();
        }

        public async Task<QuerySnapshot> GetLedgerDetailsAsync(string ledgerId)
        {
            Query query = _firestore.Collection("ledgerDetails").WhereEqualTo("ledgerId", ledgerId);
            return await query.GetSnapshotAsync();
        }

        public async Task ArchiveEntryAsync(string entryDocId)
        {
            DocumentReference docRef = _firestore.Collection("entries").Document(entryDocId);
            await docRef.UpdateAsync("isArchived", true);
        }
    }
}