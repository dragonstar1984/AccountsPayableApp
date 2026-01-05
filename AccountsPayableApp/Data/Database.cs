using AccountsPayableApp.Helpers;
using AccountsPayableApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace AccountsPayableApp.Data
{
    public static class Database
    {
        private const string V = "accountpagebug";
        private static string connectionString = @"Server=DRAGOS\SQLEXPRESS;Database=AccountsPayableDB;Trusted_Connection=True;";
        public static bool UseFirebaseOnly = false;

        // 🔐 USER AUTHENTICATION
        // Validates user credentials using Firebase Authentication and stores session tokens.
        public static bool ValidateUser(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var payload = new
                {
                    email = email,
                    password = password,
                    returnSecureToken = true
                };

                var content = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={SessionManager.ApiKey}";
                var response = client.PostAsync(url, content).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    SessionManager.CurrentUserId = result.localId;
                    SessionManager.CurrentUserToken = result.idToken;
                    return true;
                }

                return false;
            }
        }

        // 📘 LEDGER MANAGEMENT
        // Adds a new ledger to the SQL database.


        public static void AddLedger(Ledger ledger)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Ledgers (Name, Code, OwnerEmail, CreatedAt, IsArchived)
                                 VALUES (@Name, @Code, @OwnerEmail, @CreatedAt, @IsArchived)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", ledger.Name);
                    command.Parameters.AddWithValue("@Code", ledger.Code);
                    command.Parameters.AddWithValue("@OwnerEmail", ledger.OwnerEmail);
                    command.Parameters.AddWithValue("@CreatedAt", ledger.CreatedAt);
                    command.Parameters.AddWithValue("@IsArchived", ledger.IsArchived);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Ledger> GetLedgersByEmail(string email)
        {
            var ledgers = new List<Ledger>();
            string query = "SELECT Id, Name, Code, OwnerEmail, CreatedAt, IsArchived FROM Ledgers WHERE OwnerEmail = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ledgers.Add(new Ledger
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Code = reader["Code"].ToString(),
                            OwnerEmail = reader["OwnerEmail"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            IsArchived = Convert.ToBoolean(reader["IsArchived"])
                        });
                    }
                }
            }

            return ledgers;
        }

        public static bool JoinLedgerByCode(string email, string code)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO LedgerUsers (Email, LedgerId, IsOwner)
                                 SELECT @Email, Id, 0 FROM Ledgers WHERE Code = @Code
                                 AND NOT EXISTS (
                                     SELECT 1 FROM LedgerUsers WHERE Email = @Email AND LedgerId = Id
                                 )";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Code", code);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool DeleteLedger(int ledgerId, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM Ledgers WHERE Id = @LedgerId AND OwnerEmail = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LedgerId", ledgerId);
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // ───────────────────────────────────────────────────────────────
        // PAYABLE ENTRIES
        // ───────────────────────────────────────────────────────────────

        public static List<PayableEntry> GetEntries()
        {
            List<PayableEntry> entries = new List<PayableEntry>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Date, Link, Amount, CreatedBy, LedgerId FROM Entries";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new PayableEntry
                            {
                                Id = (int)reader["Id"],
                                Date = (DateTime)reader["Date"],
                                Link = reader["Link"].ToString(),
                                Amount = (decimal)reader["Amount"],
                                CreatedBy = reader["CreatedBy"].ToString(),
                                LedgerId = (int)reader["LedgerId"]
                            });
                        }
                    }
                }
            }

            return entries;
        }

        public static List<PayableEntry> GetEntriesByLedger(int ledgerId)
        {
            List<PayableEntry> entries = new List<PayableEntry>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Date, Link, Amount, CreatedBy, LedgerId FROM Entries WHERE LedgerId = @LedgerId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LedgerId", ledgerId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new PayableEntry
                            {
                                Id = (int)reader["Id"],
                                Date = (DateTime)reader["Date"],
                                Link = reader["Link"].ToString(),
                                Amount = (decimal)reader["Amount"],
                                CreatedBy = reader["CreatedBy"].ToString(),
                                LedgerId = (int)reader["LedgerId"]
                            });
                        }
                    }
                }
            }

            return entries;
        }

        public static void BulkInsertEntries(List<PayableEntry> entries)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var entry in entries)
                {
                    string query = @"INSERT INTO Entries (Date, Link, Amount, CreatedBy, LedgerId)
                                     VALUES (@Date, @Link, @Amount, @CreatedBy, @LedgerId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Date", entry.Date);
                        command.Parameters.AddWithValue("@Link", entry.Link ?? "");
                        command.Parameters.AddWithValue("@Amount", entry.Amount);
                        command.Parameters.AddWithValue("@CreatedBy", entry.CreatedBy);
                        command.Parameters.AddWithValue("@LedgerId", entry.LedgerId);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        // 🔄 Firebase fallback
        public static async Task BulkInsertEntries_Firebase(List<PayableEntry> entries)
        {
            FirebaseServiceLite firebase = new(V);

            foreach (var entry in entries)
            {
                await firebase.AddEntryAsync(entry.Date, (double)entry.Amount, entry.Link ?? "", entry.CreatedBy, entry.LedgerId.ToString());
            }
        }

        public static int ArchivePaidEntries(int ledgerId, int year)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Entries
                                 SET IsArchived = 1
                                 WHERE LedgerId = @LedgerId
                                   AND YEAR(Date) = @Year
                                   AND Amount > 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LedgerId", ledgerId);
                    command.Parameters.AddWithValue("@Year", year);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // 🔄 Firebase fallback (simulated)
        // This method mimics SQL behavior using Firebase, but may not fully replicate all logic or constraints.
        public static async Task<int> ArchivePaidEntries_Firebase(int ledgerId, int year)
        {
            var firebase = new FirebaseServiceLite("accountpagebug");
            var snapshot = await firebase.GetEntriesForLedgerAsync(ledgerId.ToString());
            int count = 0;

            foreach (var doc in snapshot.Documents)
            {
                var data = doc.ToDictionary();
                var date = ((Google.Cloud.Firestore.Timestamp)data["date"]).ToDateTime();
                var amount = Convert.ToDecimal(data["amount"]);

                if (date.Year == year && amount > 0)
                {
                    await firebase.ArchiveEntryAsync(doc.Id); 
                    count++;
                }
            }

            return count;
        }

        // ───────────────────────────────────────────────────────────────
        // PAYMENTS
        // ───────────────────────────────────────────────────────────────

        public static void InsertPayment(Payment payment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Payments (EntryId, Amount, Date, AddedBy, Notes)
                                 VALUES (@EntryId, @Amount, @Date, @AddedBy, @Notes)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EntryId", payment.EntryId);
                    command.Parameters.AddWithValue("@Amount", payment.Amount);
                    command.Parameters.AddWithValue("@Date", payment.Date);
                    command.Parameters.AddWithValue("@AddedBy", payment.AddedBy);
                    command.Parameters.AddWithValue("@Notes", payment.Notes ?? "");

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // 🔄 Firebase fallback
        public static async Task InsertPayment_Firebase(Payment payment)
        {
            var firebase = new FirebaseServiceLite
                ("accountpagebug");
            await firebase.AddPaymentAsync((double)payment.Amount, payment.EntryId.ToString(), payment.AddedBy, payment.Notes ?? "");
        }

        public static List<Payment> GetPaymentsForEntry(int entryId)
        {
            List<Payment> payments = new List<Payment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, EntryId, Amount, Date, AddedBy, Notes FROM Payments WHERE EntryId = @EntryId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EntryId", entryId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(new Payment
                            {
                                Id = (int)reader["Id"],
                                EntryId = (int)reader["EntryId"],
                                Amount = (decimal)reader["Amount"],
                                Date = (DateTime)reader["Date"],
                                AddedBy = reader["AddedBy"].ToString(),
                                Notes = reader["Notes"].ToString()
                            });
                        }
                    }
                }
            }

            return payments;
        }

        // 🔄 Firebase fallback
        public static async Task<List<Payment>> GetPaymentsForEntry_Firebase(int entryId)
        {
            var firebase = new FirebaseServiceLite
                ("accountpagebug");
            var snapshot = await firebase.GetPaymentsForEntryAsync(entryId.ToString());
            var payments = new List<Payment>();

            foreach (var doc in snapshot.Documents)
            {
                var data = doc.ToDictionary();
                payments.Add(new Payment
                {
                    Id = 0,
                    EntryId = entryId,
                    Amount = Convert.ToDecimal(data["amount"]),
                    Date = ((Google.Cloud.Firestore.Timestamp)data["date"]).ToDateTime(),
                    AddedBy = data["addedBy"].ToString(),
                    Notes = data["notes"].ToString()
                });
            }

            return payments;
        }

        // ───────────────────────────────────────────────────────────────
        // LEDGER DETAILS (TRANSACTIONS)
        // ───────────────────────────────────────────────────────────────

        public static List<LedgerDetail> GetLedgerDetailsByLedgerId(int ledgerId)
        {
            List<LedgerDetail> details = new List<LedgerDetail>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT DetailId, LedgerId, MerchantName, Description 
                                 FROM LedgerDetails 
                                 WHERE LedgerId = @LedgerId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LedgerId", ledgerId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            details.Add(new LedgerDetail
                            {
                                DetailId = (int)reader["DetailId"],
                                LedgerId = (int)reader["LedgerId"],
                                MerchantName = reader["MerchantName"].ToString(),
                                Description = reader["Description"].ToString()
                            });
                        }
                    }
                }
            }

            return details;
        }

        // 🔄 Firebase fallback (simulation)
        public static async Task<List<LedgerDetail>> GetLedgerDetailsByLedgerId_Firebase(int ledgerId)
        {
            var firebase = new FirebaseServiceLite
                ("accountpagebug");
            var snapshot = await firebase.GetLedgerDetailsAsync(ledgerId.ToString());
            var details = new List<LedgerDetail>();

            foreach (var doc in snapshot.Documents)
            {
                var data = doc.ToDictionary();
                details.Add(new LedgerDetail
                {
                    DetailId = 0,
                    LedgerId = ledgerId,
                    MerchantName = data["merchantName"].ToString(),
                    Description = data["description"].ToString()
                });
            }

            return details;
        }

        // ───────────────────────────────────────────────────────────────
        // EXPORT TO CSV
        // ───────────────────────────────────────────────────────────────

        public static void ExportEntriesToCsv(List<PayableEntry> entries, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Id,Date,Link,Amount,CreatedBy,LedgerId");

                foreach (var entry in entries)
                {
                    string line = $"{entry.Id},{entry.Date:yyyy-MM-dd},{entry.Link},{entry.Amount},{entry.CreatedBy},{entry.LedgerId}";
                    writer.WriteLine(line);
                }
            }
        }

        // ───────────────────────────────────────────────────────────────
        // SUPPORT CLASS FOR UI COMBOBOX BINDING
        // ───────────────────────────────────────────────────────────────

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
}
