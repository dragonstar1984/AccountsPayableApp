using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AccountsPayableApp.Helpers
{
    public static class FirestoreService
    {
        private const string ProjectId = "accountspayableapp";

        public static async Task<bool> SaveEntryAsync(Dictionary<string, object> entryData)
        {
            string url = $"https://firestore.googleapis.com/v1/projects/{ProjectId}/databases/(default)/documents/entries";

            var fields = new Dictionary<string, object>
            {
                { "createdBy", new { stringValue = SessionManager.CurrentUserId } }
            };

            foreach (var kvp in entryData)
            {
                object valueWrapper = kvp.Value switch
                {
                    string s => new { stringValue = s },
                    double d => new { doubleValue = d },
                    int i => new { integerValue = i },
                    bool b => new { booleanValue = b },
                    _ => new { stringValue = kvp.Value.ToString() }
                };

                fields[kvp.Key] = valueWrapper;
            }

            var payload = new { fields };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SessionManager.CurrentUserToken);

            var response = await client.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }
    }
}