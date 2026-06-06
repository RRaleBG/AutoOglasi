# Social Login - Quick Reference Card

## ⚡ 60-Second Setup

```powershell
# Google
dotnet user-secrets set "Google:ClientId" "..." --project Web\AutoOglasi.Web
dotnet user-secrets set "Google:ClientSecret" "..." --project Web\AutoOglasi.Web

# Microsoft
dotnet user-secrets set "Microsoft:ClientId" "..." --project Web\AutoOglasi.Web
dotnet user-secrets set "Microsoft:ClientSecret" "..." --project Web\AutoOglasi.Web

# Run
dotnet run --project Web\AutoOglasi.Web
```

## 🎨 Button Display Rules

- ✅ Buttons appear if `ClientId` and `ClientSecret` are configured
- ❌ Buttons hidden if either credential is empty/missing
- 🔄 Multiple providers show as stacked buttons

## 📍 Redirect URIs (Development)

```
https://localhost:5001/signin-google
https://localhost:5001/signin-microsoft
https://localhost:5001/signin-github
```

## 🔑 User Secrets Locations

**Windows**: `%APPDATA%\Microsoft\UserSecrets\aspnet-AutoOglasi-28FC1372-11FE-44D9-9A28-987D2F90AC42\secrets.json`

**Linux/Mac**: `~/.microsoft/usersecrets/aspnet-AutoOglasi-28FC1372-11FE-44D9-9A28-987D2F90AC42/secrets.json`

## 📋 Configuration Keys

```
Google:ClientId
Google:ClientSecret
Microsoft:ClientId
Microsoft:ClientSecret
GitHub:ClientId
GitHub:ClientSecret
```

## 🔗 OAuth Provider URLs

| Provider | URL |
|----------|-----|
| Google | https://console.cloud.google.com/ |
| Microsoft | https://portal.azure.com/ |
| GitHub | https://github.com/settings/developers |

## 📁 Modified Files

```
✅ Login.cshtml
✅ Login.cshtml.cs (no changes needed)
✅ Register.cshtml
✅ Register.cshtml.cs
✅ appsettings.json
✅ appsettings.Development.json
```

## 🧪 Quick Test

1. Set credentials with user secrets
2. Run: `dotnet run --project Web\AutoOglasi.Web`
3. Visit: `https://localhost:5001/Identity/Account/Login`
4. Buttons should appear
5. Click button → OAuth provider login → Back to app → User created

## 🚨 Common Issues

| Issue | Fix |
|-------|-----|
| Buttons not showing | Check if credentials are set (empty = hidden) |
| "Invalid redirect URI" | Verify URI in OAuth provider matches localhost:5001 |
| HTTPS error | OAuth requires HTTPS; use localhost (automatic) |
| Auth fails | Double-check Client ID/Secret; verify app is registered |

## 📚 Documentation

- `SOCIAL_LOGIN_QUICKSTART.md` - Quick setup
- `OAUTH_SETUP_GUIDE.md` - Detailed guide
- `SOCIAL_LOGIN_UI_NOTES.md` - Styling & customization
- `IMPLEMENTATION_SUMMARY.md` - Full details

## ✅ Feature Summary

```
✨ Google Login/Register
✨ Microsoft Login/Register
✨ GitHub Login/Register
✨ Automatic user creation on first OAuth login
✨ Configuration-driven (no hardcoded secrets)
✨ Bootstrap luxury theme styling
✨ Production-ready with Key Vault support
```

---

**Questions?** See the documentation files above.
