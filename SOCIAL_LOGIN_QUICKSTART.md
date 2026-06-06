# Quick Start: Social Login Configuration

## What Was Added

✅ **Login page** now displays social login buttons for configured providers  
✅ **Register page** now displays social registration buttons for configured providers  
✅ **Support for**: Google, Microsoft Account, and GitHub OAuth  
✅ **Configuration**: Environment-driven (no hardcoded secrets)

---

## Quick Setup (5 minutes)

### 1. Google OAuth (Optional)

```powershell
# Get credentials from: https://console.cloud.google.com/
# 1. Create OAuth 2.0 credentials (Web application)
# 2. Add redirect URI: https://localhost:5001/signin-google

# Store in user secrets:
dotnet user-secrets set "Google:ClientId" "YOUR_CLIENT_ID" --project Web\AutoOglasi.Web
dotnet user-secrets set "Google:ClientSecret" "YOUR_CLIENT_SECRET" --project Web\AutoOglasi.Web
```

### 2. Microsoft Account OAuth (Optional)

```powershell
# Get credentials from: https://portal.azure.com/
# 1. Create app registration in Azure AD
# 2. Add redirect URI: https://localhost:5001/signin-microsoft
# 3. Create client secret

# Store in user secrets:
dotnet user-secrets set "Microsoft:ClientId" "YOUR_CLIENT_ID" --project Web\AutoOglasi.Web
dotnet user-secrets set "Microsoft:ClientSecret" "YOUR_CLIENT_SECRET" --project Web\AutoOglasi.Web
```

### 3. GitHub OAuth (Optional)

```powershell
# Get credentials from: https://github.com/settings/developers
# 1. Create OAuth app
# 2. Set callback URL: https://localhost:5001/signin-github

# Store in user secrets:
dotnet user-secrets set "GitHub:ClientId" "YOUR_CLIENT_ID" --project Web\AutoOglasi.Web
dotnet user-secrets set "GitHub:ClientSecret" "YOUR_CLIENT_SECRET" --project Web\AutoOglasi.Web
```

### 4. Run the Application

```powershell
dotnet run --project Web\AutoOglasi.Web
```

Login buttons will automatically appear for any configured provider.

---

## How It Works

1. **User clicks** "Google" / "Microsoft" / "GitHub" button on Login/Register page
2. **Redirected** to OAuth provider's login page
3. **User authenticates** with their social account
4. **Redirected back** to the application
5. **User account created** automatically if first-time login
6. **User signed in** to the application

---

## Files Modified

| File | Change |
|------|--------|
| `Web/AutoOglasi.Web/Areas/Identity/Pages/Account/Login.cshtml` | Added social login buttons |
| `Web/AutoOglasi.Web/Areas/Identity/Pages/Account/Login.cshtml.cs` | Already had external logins support |
| `Web/AutoOglasi.Web/Areas/Identity/Pages/Account/Register.cshtml` | Added social register buttons |
| `Web/AutoOglasi.Web/Areas/Identity/Pages/Account/Register.cshtml.cs` | Added `ExternalLogins` property and populated in `OnGetAsync` |
| `Web/AutoOglasi.Web/appsettings.json` | Added OAuth configuration placeholders |
| `Web/AutoOglasi.Web/appsettings.Development.json` | Added OAuth configuration placeholders |

---

## Configuration Priority

The application checks configuration in this order:

1. **User Secrets** (development) ← Use this for local development
2. **Environment Variables** (all environments)
3. **appsettings.json** (not recommended for secrets)
4. **appsettings.{Environment}.json** (not recommended for secrets)

**Important**: If a provider's Client ID or Client Secret is empty, that provider's button won't appear.

---

## For Production

1. **Use Azure Key Vault** for secrets instead of user secrets
2. **Update redirect URIs** in OAuth provider settings to your production domain
3. **Enable HTTPS** (required for OAuth)
4. **Use environment variables** or key vault for storing credentials

Example: `https://yourdomain.com/signin-google`

---

## Need Help?

See `OAUTH_SETUP_GUIDE.md` for detailed setup instructions and troubleshooting.
