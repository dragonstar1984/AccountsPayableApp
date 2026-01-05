namespace AccountsPayableApp.Helpers
{
    /// <summary>
    /// Stores session-level data for the currently authenticated user.
    /// Used across forms to maintain context and access control.
    /// </summary>
    public static class SessionManager
    {
        /// <summary>
        /// The email address of the currently logged-in user.
        /// </summary>
        public static string CurrentUserEmail { get; set; }

        /// <summary>
        /// The Firebase UID (localId) of the authenticated user.
        /// Used for identifying ownership in Firestore documents.
        /// </summary>
        public static string CurrentUserId { get; set; }

        /// <summary>
        /// The Firebase ID token returned after successful login.
        /// Can be used for secure API calls or Firestore access.
        /// </summary>
        public static string CurrentUserToken { get; set; }

        /// <summary>
        /// The Firebase Web API Key used for authentication requests.
        /// You can find this in your Firebase project settings.
        /// </summary>
        public static string ApiKey = "AIzaSyByv_IHsCF7fwjOKTuAsiyYVvzSpE863lM"; 
    }
}
