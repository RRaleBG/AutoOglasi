# Social Login Flow Diagram

## 🔄 OAuth Authentication Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    AutoOglasi Application                        │
│                                                                  │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │            Login / Register Page                         │   │
│  │                                                          │   │
│  │  [Email & Password Fields]                              │   │
│  │  [Login/Register Button]                                │   │
│  │  ─────────────────────────────────                       │   │
│  │  Or sign in with:                                       │   │
│  │  ┌──────────────────────────────────┐                  │   │
│  │  │ [Google]     [Microsoft] [GitHub] │                  │   │
│  │  └──────────────────────────────────┘                  │   │
│  │                                                          │   │
│  │  [Create Account] / [Sign In Link]                      │   │
│  └──────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────┘
								│
					User clicks social button
								│
								▼
		┌────────────────────────────────────────┐
		│   OAuth Provider (Google/Microsoft)   │
		│                                        │
		│  ┌──────────────────────────────────┐ │
		│  │   OAuth Login Page               │ │
		│  │   - Email/Password               │ │
		│  │   - Consent Screen               │ │
		│  └──────────────────────────────────┘ │
		└────────────────────────────────────────┘
					User authenticates
					and consents
								│
								▼
		┌────────────────────────────────────────┐
		│   OAuth Provider validates            │
		│   and redirects back to app with:      │
		│   - Authorization Code                │
		│   - OAuth Token                       │
		└────────────────────────────────────────┘
								│
								▼
┌─────────────────────────────────────────────────────────────────┐
│            AutoOglasi ExternalLogin Page Handler                 │
│                                                                  │
│  1. Receive OAuth token/authorization code                      │
│  2. Exchange for user profile info (email, name)                │
│  3. Check if user exists in database                            │
│     ├─ If exists: Sign user in                                  │
│     └─ If new: Create new user account + Sign in               │
│  4. Redirect to return URL or home page                         │
└─────────────────────────────────────────────────────────────────┘
								│
								▼
		┌────────────────────────────────────────┐
		│   User signed in                       │
		│   Cookie set in browser                │
		│   Redirect to home page or             │
		│   specified return URL                 │
		└────────────────────────────────────────┘
```

---

## 🏗️ Application Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                        Program.cs                               │
│                                                                  │
│  ├─ AddAuthentication()                                         │
│  ├─ AddGoogle(ClientId, ClientSecret)                          │
│  ├─ AddMicrosoftAccount(ClientId, ClientSecret)                │
│  └─ AddGitHub(ClientId, ClientSecret)                          │
└─────────────────────────────────────────────────────────────────┘
							│
							▼
┌──────────────────────────────────────┐
│     Configuration Sources            │
│                                      │
│  ├─ User Secrets (Dev)              │
│  ├─ appsettings.json                │
│  ├─ appsettings.Development.json    │
│  ├─ Environment Variables           │
│  └─ Azure Key Vault (Production)    │
└──────────────────────────────────────┘
							│
							▼
┌──────────────────────────────────────┐
│     ASP.NET Core Identity            │
│                                      │
│  ├─ SignInManager                   │
│  ├─ UserManager                     │
│  ├─ External Auth Schemes           │
│  └─ Cookie Authentication           │
└──────────────────────────────────────┘
							│
							▼
┌─────────────────────────────────────────────────────────────────┐
│              Razor Pages (Identity)                             │
│                                                                  │
│  ├─ Login.cshtml / Login.cshtml.cs                              │
│  ├─ Register.cshtml / Register.cshtml.cs                        │
│  └─ ExternalLogin.cshtml.cs (Microsoft generated)              │
└─────────────────────────────────────────────────────────────────┘
							│
							▼
┌──────────────────────────────────────┐
│     Database (EF Core)               │
│                                      │
│  ├─ ApplicationUser                 │
│  │  ├─ Email                        │
│  │  ├─ FullName                     │
│  │  ├─ UserLogins (OAuth links)    │
│  │  └─ Claims, Roles, etc.         │
│  │                                  │
│  └─ IdentityUserLogin               │
│     ├─ LoginProvider (Google/etc)  │
│     └─ ProviderKey                 │
└──────────────────────────────────────┘
```

---

## 📊 User Creation Flow (First-Time OAuth)

```
┌─────────────────────────────────────────────┐
│  User logs in with Google (first time)      │
└─────────────────────────────────────────────┘
					│
					▼
	 ┌──────────────────────────────┐
	 │ ASP.NET Core Identity checks │
	 │ if OAuth record exists       │
	 └──────────────────────────────┘
					│
					├─────────────────┬─────────────────┐
					▼                 ▼                 ▼
			Existing User      First-time Login    Error
					│                 │              │
					▼                 ▼              ▼
			Sign in user    Create new user    Handle error
					│                 │
					│          ┌──────┴──────┐
					│          ▼             ▼
					│    Create email-  Create UserLogin
					│    based user    record linking
					│    account       OAuth provider
					│          │             │
					│          └──────┬──────┘
					│                 ▼
					│         User created with:
					│         - Email: OAuth email
					│         - FullName: "" (optional)
					│         - UserLogins entry
					│                 │
					└────────┬────────┘
							 ▼
					┌─────────────────┐
					│ Sign in user    │
					│ Set auth cookie │
					│ Redirect home   │
					└─────────────────┘
```

---

## 🔐 Configuration Resolution Order

```
Configuration Request: "Google:ClientId"
			  │
			  ▼
		┌─────────────────────┐
		│ Check User Secrets  │ ◄─── Priority 1 (Development)
		│ (secretsid.json)    │
		└─────────────────────┘
			  │
		 Found? ├─ YES → Use this value
			  │
		 NO   └─ Continue
			  │
			  ▼
		┌─────────────────────┐
		│ Check Environment   │ ◄─── Priority 2
		│ Variables           │
		└─────────────────────┘
			  │
		 Found? ├─ YES → Use this value
			  │
		 NO   └─ Continue
			  │
			  ▼
		┌─────────────────────┐
		│ Check              │ ◄─── Priority 3
		│ appsettings.       │
		│ Development.json   │
		└─────────────────────┘
			  │
		 Found? ├─ YES → Use this value
			  │
		 NO   └─ Continue
			  │
			  ▼
		┌─────────────────────┐
		│ Check              │ ◄─── Priority 4
		│ appsettings.json   │
		└─────────────────────┘
			  │
		 Found? ├─ YES → Use this value
			  │
		 NO   └─ Not configured
			  │
			  ▼
		┌─────────────────────┐
		│ Provider button     │
		│ NOT displayed       │
		│ (Empty = Hidden)    │
		└─────────────────────┘
```

---

## 🔄 Login Page Render Flow

```
┌──────────────────────┐
│ User requests Login  │
│ page (/login)        │
└──────────────────────┘
		  │
		  ▼
┌──────────────────────────────────┐
│ LoginModel.OnGetAsync()          │
│                                  │
│ 1. Get return URL                │
│ 2. Clear external cookies        │
│ 3. Get external auth schemes     │
│    (SignInManager.               │
│     GetExternalAuth...)          │
│ 4. Populate ExternalLogins list  │
└──────────────────────────────────┘
		  │
		  ▼
┌──────────────────────────────────┐
│ Render Login.cshtml              │
│                                  │
│ - Display form fields            │
│ - Loop through ExternalLogins    │
│ - For each provider:             │
│   ├─ Check if configured         │
│   │  (ClientId not empty)        │
│   ├─ If yes: Show button         │
│   └─ If no: Skip button          │
└──────────────────────────────────┘
		  │
		  ▼
┌──────────────────────────────────┐
│ HTML Response to Browser         │
│                                  │
│ - Email/Password form            │
│ - Social login buttons (if conf) │
│ - Links to Register/Forgot Pass  │
└──────────────────────────────────┘
```

---

## 🚀 Social Login Button Click Flow

```
┌───────────────────────────────────┐
│ User clicks "Google" button       │
│ on Login/Register page            │
└───────────────────────────────────┘
		  │
		  ▼
┌───────────────────────────────────┐
│ Form submits to                   │
│ /Identity/Account/ExternalLogin  │
│ (POST)                            │
│                                   │
│ Parameters:                       │
│ - provider: "Google"              │
│ - returnUrl: /                    │
└───────────────────────────────────┘
		  │
		  ▼
┌───────────────────────────────────┐
│ ExternalLogin.cshtml.cs           │
│ (Microsoft-generated page model)  │
│                                   │
│ 1. Verify provider exists         │
│ 2. Build redirect URL to OAuth    │
│    provider with scopes           │
│ 3. Initiate OAuth challenge       │
└───────────────────────────────────┘
		  │
		  ▼
┌───────────────────────────────────┐
│ Browser redirected to OAuth       │
│ provider login page               │
│                                   │
│ (Google/Microsoft/GitHub login)   │
└───────────────────────────────────┘
		  │
	(User authenticates)
		  │
		  ▼
┌───────────────────────────────────┐
│ OAuth provider redirects back     │
│ to app with authorization code    │
│                                   │
│ URL:                              │
│ /Identity/Account/ExternalLogin  │
│ Callback                          │
└───────────────────────────────────┘
		  │
		  ▼
┌───────────────────────────────────┐
│ Identity platform exchanges       │
│ code for user profile info        │
│                                   │
│ Gets: email, name, profile ID    │
└───────────────────────────────────┘
		  │
		  ▼
┌───────────────────────────────────┐
│ Check if user exists              │
└───────────────────────────────────┘
	│                   │
	▼                   ▼
 EXISTS            NOT EXISTS
	│                   │
	▼                   ▼
Sign in user    Create new user
	│                   │
	│          ┌────────┴────────┐
	│          ▼                 ▼
	│   Create ApplicationUser  Add UserLogin
	│   with email from OAuth   to link OAuth
	│                           account
	│          │                 │
	└────────┬─┴─────────────────┘
			 ▼
┌───────────────────────────────────┐
│ Create auth cookie                │
│ Set user as authenticated         │
│ Redirect to return URL            │
│ (or home page)                    │
└───────────────────────────────────┘
		  │
		  ▼
┌───────────────────────────────────┐
│ User logged in & redirected       │
│ to requested page                 │
└───────────────────────────────────┘
```

---

## 📈 System Dependencies

```
AutoOglasi.Web
	│
	├─ Microsoft.AspNetCore.Authentication
	│
	├─ Microsoft.AspNetCore.Authentication.Google
	│  └─ Handles Google OAuth flow
	│
	├─ Microsoft.AspNetCore.Authentication.MicrosoftAccount
	│  └─ Handles Microsoft Account OAuth flow
	│
	├─ AspNet.Security.OAuth.GitHub
	│  └─ Handles GitHub OAuth flow
	│
	├─ Microsoft.AspNetCore.Identity
	│  └─ User & Sign-in management
	│
	└─ Microsoft.EntityFrameworkCore
	   └─ Database for user storage
```

---

## ✅ Files Modified Summary

```
Web/AutoOglasi.Web/
├─ Areas/Identity/Pages/Account/
│  ├─ Login.cshtml ..................... ✏️ Added social buttons
│  ├─ Login.cshtml.cs .................. ✅ No changes (already ready)
│  ├─ Register.cshtml .................. ✏️ Added social buttons
│  └─ Register.cshtml.cs ............... ✏️ Added ExternalLogins support
│
├─ Program.cs .......................... ✅ No changes (already configured)
│
├─ appsettings.json .................... ✏️ Added placeholders
└─ appsettings.Development.json ........ ✏️ Added placeholders
```

This completes the implementation! 🎉
