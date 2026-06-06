# OAuth Setup Guide for AutoOglasi

This guide explains how to configure Google and Microsoft OAuth authentication for the AutoOglasi application.

## Overview

The application now supports social login with:
- **Google OAuth**
- **Microsoft Account**
- **GitHub** (already configured)

Social login buttons are displayed on both the Login and Register pages. Users can choose to authenticate using their existing social accounts instead of creating a new username/password.

## Prerequisites

- User Secrets configured (for development)
- OAuth credentials from Google and/or Microsoft
- HTTPS enabled in development (required for OAuth redirects)

---

## 1. Google OAuth Setup

### Step 1: Create a Google Cloud Project

1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project or select an existing one
3. Enable the **Google+ API** (if not already enabled)
4. Navigate to **APIs & Services → Credentials**

### Step 2: Create OAuth 2.0 Credentials

1. Click **Create Credentials → OAuth 2.0 Client ID**
2. Select **Web application**
3. Add the following Authorized redirect URIs:
   - `https://localhost:5001/signin-google` (development)
   - `https://localhost:5002/signin-google` (development, if using different port)
   - `https://yourdomain.com/signin-google` (production)
4. Copy the **Client ID** and **Client Secret**

### Step 3: Store Credentials in User Secrets

```powershell
# Open user secrets
dotnet user-secrets init --project Web\AutoOglasi.Web

# Set Google credentials
dotnet user-secrets set "Google:ClientId" "YOUR_GOOGLE_CLIENT_ID" --project Web\AutoOglasi.Web
dotnet user-secrets set "Google:ClientSecret" "YOUR_GOOGLE_CLIENT_SECRET" --project Web\AutoOglasi.Web
```

### Step 4: Verify Configuration

The Login and Register pages will automatically display a "Google" button if credentials are configured.

---

## 2. Microsoft Account OAuth Setup

### Step 1: Register an Application in Azure AD

1. Go to [Azure Portal](https://portal.azure.com/)
2. Navigate to **Azure Active Directory → App registrations**
3. Click **New registration**
4. Enter the app name (e.g., "AutoOglasi")
5. Select **Accounts in any organizational directory and personal Microsoft accounts**
6. Click **Register**

### Step 2: Configure Redirect URIs

1. Go to **Authentication** in the left panel
2. Under **Redirect URIs**, add:
   - `https://localhost:5001/signin-microsoft` (development)
   - `https://localhost:5002/signin-microsoft` (development, if using different port)
   - `https://yourdomain.com/signin-microsoft` (production)
3. Click **Save**

### Step 3: Create a Client Secret

1. Go to **Certificates & secrets**
2. Click **New client secret**
3. Set the expiration period
4. Copy the **Value** (not the ID)

### Step 4: Note Your Credentials

1. From the **Overview** page, copy the **Application (client) ID**
2. Use the **Client secret value** from Step 3

### Step 5: Store Credentials in User Secrets

```powershell
# Set Microsoft credentials
dotnet user-secrets set "Microsoft:ClientId" "YOUR_MICROSOFT_CLIENT_ID" --project Web\AutoOglasi.Web
dotnet user-secrets set "Microsoft:ClientSecret" "YOUR_MICROSOFT_CLIENT_SECRET" --project Web\AutoOglasi.Web
```

### Step 6: (Optional) Set App ID URI

The App ID URI is already configured in `appsettings.Development.json`:

```json
"Microsoft": {
  "ClientId": "",
  "ClientSecret": "",
  "AppIdUri": "api://89db4f18-c351-4732-b2ac-f227607dee72"
}
```

You can update this to match your tenant if needed.

---

## 3. GitHub OAuth Setup (Already Configured)

If you want to enable GitHub login as well:

```powershell
# Set GitHub credentials
dotnet user-secrets set "GitHub:ClientId" "YOUR_GITHUB_CLIENT_ID" --project Web\AutoOglasi.Web
dotnet user-secrets set "GitHub:ClientSecret" "YOUR_GITHUB_CLIENT_SECRET" --project Web\AutoOglasi.Web
```

For detailed GitHub OAuth setup, see [GitHub OAuth Documentation](https://docs.github.com/en/developers/apps/building-oauth-apps).

---

## 4. User Secrets Management

### View All Configured Secrets

```powershell
dotnet user-secrets list --project Web\AutoOglasi.Web
```

### Remove a Secret

```powershell
dotnet user-secrets remove "Google:ClientId" --project Web\AutoOglasi.Web
```

### Clear All Secrets

```powershell
dotnet user-secrets clear --project Web\AutoOglasi.Web
```

---

## 5. Production Deployment

### Important Security Notes

1. **Never commit secrets** to version control
2. Use **Azure Key Vault** for production secrets
3. **Use HTTPS only** in production
4. Configure **CORS** appropriately for your domain
5. Regularly **rotate client secrets**

### Azure Key Vault Integration Example

```csharp
// In Program.cs (for production)
if (!builder.Environment.IsDevelopment())
{
	var keyVaultUrl = new Uri(builder.Configuration["KeyVault:Url"]);
	builder.Configuration.AddAzureKeyVault(
		keyVaultUrl,
		new DefaultAzureCredential());
}
```

---

## 6. Troubleshooting

### "Invalid redirect URI" Error

- Ensure the redirect URI exactly matches what's configured in the OAuth provider settings
- Check that HTTPS is enabled
- Verify the port number is correct

### Social Button Not Appearing

- Check if credentials are configured (empty strings in config are treated as not configured)
- Verify user secrets are properly set
- Check browser console for errors

### Authentication Fails

- Confirm Client ID and Client Secret are correct
- Check that the user's email domain is allowed in the OAuth provider settings
- Review application logs for authentication errors

### User Created but Profile Not Populated

- The app creates a user with the email from the OAuth provider
- Additional profile data (like full name) may need to be configured in the OAuth app settings
- Users can update their profile after login

---

## 7. Configuration Summary

### Configuration Keys

| Key | Purpose | Required |
|-----|---------|----------|
| `Google:ClientId` | Google OAuth Client ID | Only if enabling Google login |
| `Google:ClientSecret` | Google OAuth Client Secret | Only if enabling Google login |
| `Microsoft:ClientId` | Microsoft OAuth Client ID | Only if enabling Microsoft login |
| `Microsoft:ClientSecret` | Microsoft OAuth Client Secret | Only if enabling Microsoft login |
| `GitHub:ClientId` | GitHub OAuth Client ID | Only if enabling GitHub login |
| `GitHub:ClientSecret` | GitHub OAuth Client Secret | Only if enabling GitHub login |

### Default Configuration Files

- **Development**: `appsettings.Development.json` + user secrets
- **Production**: `appsettings.json` + Azure Key Vault or environment variables

---

## References

- [ASP.NET Core Authentication with External Providers](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/)
- [Google OAuth 2.0](https://developers.google.com/identity/protocols/oauth2)
- [Microsoft Identity Platform](https://learn.microsoft.com/en-us/azure/active-directory/develop/)
- [ASP.NET Core User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets)
