using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccountsPayableApp.Helpers
{
    public static class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyByv_IHsCF7fwjOKTuAsiyYVvzSpE863lM";

        public static async Task<(bool success, string localId, string idToken, string errorMessage)> AuthenticateAsync(string email, string password)
        {
            var client = new HttpClient();
            var payload = new
            {
                email = email,
                password = password,
                returnSecureToken = true
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={ApiKey}", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(result);
                string localId = json.localId;
                string idToken = json.idToken;

                return (true, localId, idToken, null);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, null, error);
            }
        }
    }
}