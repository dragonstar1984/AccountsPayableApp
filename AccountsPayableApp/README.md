# AccountsPayableApp

A modern C# desktop application for managing accounts payable, featuring secure login, Firebase integration, and a clean, responsive UI. Developed as part of a software development program, with premium delivery standards and modular architecture.

---

## 🔐 Features

- Email and password login  
- Password visibility toggle (eye icon)  
- Local and Firebase authentication  
- Dynamically centered UI  
- Custom gradient background  
- Clear error messaging  
- Navigation to dashboard after login  
- Account creation link (placeholder)

---

## 🧰 Technical Details

- **Language:** C# (.NET Framework)  
- **UI:** Windows Forms  
- **Database:** SQL Server + Firebase Firestore  
- **Architecture:** Modular — Forms, Helpers, Models  
- **Password toggle:** Dynamically positioned `PictureBox` next to `txtPassword`  
- **Gradient:** `LinearGradientBrush` in `Paint` event  
- **Email validation:** Basic check for `@` and `.`  
- **Firebase login:** `FirebaseAuthService.AuthenticateAsync`  
- **Session management:** `SessionManager.cs` stores user email, token, and ID

---

## 🚀 How to Run

1. Open the solution in Visual Studio  
2. Ensure the `Resources` folder contains `eye.png` and `hidden.png`  
3. Firebase is already configured — no setup required  
4. Press `F5` or click `Start` to run the application

---

## 🔧 Firebase Setup (Preconfigured Demo)

This project is already connected to a Firebase backend.  
The following services are preconfigured and ready to use:

- ✅ Firebase Authentication (Email/Password)  
- ✅ Firestore Database (Production mode)  
- ✅ API access via `FirebaseAuthService.cs`  
- ✅ Linked to Google Cloud Console for monitoring  
- ✅ Firebase CLI is installed and authenticated

> ⚠️ **Note:** The Firebase account used is a demo instance for development and testing purposes only.  
> You will need to add your own users manually in the Firebase Console under **Authentication > Users**.

---

## 🔑 Default Demo Credentials (for testing only)

- Email: testuser@app.com  
- Password: Test1234!

> These credentials are for demonstration only. You should create your own users for production use.

---

## 🔁 Migrating to Your Own Firebase Project (Optional)

If you prefer to use your own Firebase account:

### 1. Firebase Console
- Go to [Firebase Console](https://console.firebase.google.com/)
- Create a new project (e.g., `AccountsPayableApp`)
- Enable **Email/Password Authentication** under **Authentication > Sign-in method**
- Create a Firestore database in **Production mode**
- Add a Web App and copy the config values (`apiKey`, `authDomain`, `projectId`, etc.)
- Replace the values in `FirebaseAuthService.cs` or your config file
- Add your own users under **Authentication > Users**
- Populate Firestore with your ledger and payment data

### 2. Firebase CLI
Install and authenticate Firebase CLI:
```bash
npm install -g firebase-tools
firebase login
firebase init
Understood, Dragos — here’s your complete README.md, from top to bottom, in English, including everything you’ve built and configured:
✅ Firebase integration
✅ Google Cloud Console linkage
✅ Firebase CLI setup
✅ Demo account disclaimer
✅ Full app structure and instructions
You can copy and paste this entire block into your  file:


3. Google Cloud Console
• 	Go to Google Cloud Console
• 	Link your Firebase project
• 	Enable APIs like Firestore, Identity Toolkit, etc.
• 	Use IAM & Admin to manage roles and permissions
• 	Monitor usage under API & Services > Dashboard
Once configured, the app will connect to your Firebase instance and operate with your live data.

Data Usage Disclaimer
This application is fully functional and ready to operate with real data — but currently runs in demo mode using a test Firebase project.
What’s included:
• 	A preconfigured Firebase backend (Firestore + Authentication)
• 	A demo user account for testing login functionality
• 	Sample structure for ledger and payment entries
Important:
• 	No real financial data is stored or used by default
• 	The current Firebase instance is for demonstration only
• 	You must add your own users and data to make the app production-ready

AccountsPayableApp/
│
├── Forms/
│   ├── LoginForm.cs              # Login UI and logic
│   ├── LedgerSelectionForm.cs    # Dashboard after login
│   ├── AddPaymentForm.cs         # Payment entry form
│
├── Helpers/
│   ├── FirebaseAuthService.cs    # Firebase authentication logic
│   ├── FirestoreService.cs       # Firestore data operations
│   └── SessionManager.cs         # Stores current user session info
│
├── Models/
│   ├── Ledger.cs                 # Ledger header model
│   ├── LedgerDetail.cs           # Ledger detail model
│   ├── PayableEntry.cs           # Payable entry model
│   └── Payment.cs                # Payment model
│
├── Resources/
│   ├── eye.png                   # Password visible icon
│   └── hidden.png                # Password hidden icon
│
├── Program.cs                    # Application entry point
└── README.md                     # Project documentation

Testing
• 	Eye icon toggles password visibility correctly
• 	Password field switches between masked and plain text
• 	Error messages display on invalid input
• 	Login validates both locally and via Firebase
• 	UI is centered and responsive
• 	Firestore operations tested with mock data

🧠 Notes
• 	All code is documented in English
• 	UI is clean, intuitive, and modern
• 	picTogglePassword is dynamically repositioned in LoginLoad_Form
• 	Firebase integration is modular and easy to extend
• 	Premium delivery: no redundant code, no scope creep
• 	Architecture supports future modules (e.g., reports, analytics)

For easier evaluation of the application, the project includes a dedicated  folder.
It contains relevant images of the interface and key workflows, allowing reviewers to understand the functionality without running the project locally.

📌 Status
✅ Completed and tested
📦 Ready for delivery
🛠️ Easy to extend
