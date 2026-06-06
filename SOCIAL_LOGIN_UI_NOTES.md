# Social Login UI/UX Notes

## Button Styling

The social login buttons use your existing Bootstrap luxury theme:

```html
<button type="submit" 
		class="btn btn-outline-secondary py-2 text-uppercase small" 
		name="provider" 
		value="@provider.Name" 
		title="Log in using your @provider.DisplayName account">
	@provider.DisplayName
</button>
```

### Current Styling Classes
- `btn btn-outline-secondary` - Bootstrap outline button with secondary color
- `py-2` - Vertical padding
- `text-uppercase` - Uppercase text (matches your theme)
- `small` - Small font size

### Display Names
The buttons automatically display:
- ✅ "Google" (for Google OAuth)
- ✅ "Microsoft" (for Microsoft Account)
- ✅ "GitHub" (for GitHub OAuth)

---

## Button Layout

The social login buttons are displayed:

1. **Below the form** - After the primary login/register button
2. **Separated by HR line** - Visual separation: `<hr class="my-4 border-secondary opacity-10" />`
3. **In a grid** - Using `d-grid gap-2` for full-width stacked buttons
4. **Conditional** - Only shown if at least one provider is configured

### Login Page Layout
```
[Email Field]
[Password Field]
[Remember Me] [Forgot Password?]
[Login Button]
─────────────────────
Or sign in with:
[Google Button]
[Microsoft Button]
[GitHub Button]
─────────────────────
Don't have an account?
[Create Account Button]
[Resend Email Link]
```

### Register Page Layout
```
[Full Name Field]
[Email Field]
[Password Field]
[Confirm Password Field]
[Register Button]
─────────────────────
Or register with:
[Google Button]
[Microsoft Button]
[GitHub Button]
─────────────────────
Already registered?
[Sign In Button]
```

---

## Customization Options

### Change Button Colors

To change button colors, modify the class in the Razor view:

```html
<!-- Current: btn-outline-secondary (gray) -->
<button class="btn btn-outline-secondary">

<!-- Options: -->
<!-- btn-outline-primary (blue) -->
<!-- btn-outline-danger (red) -->
<!-- btn-outline-success (green) -->
<!-- btn-outline-warning (orange) -->
<!-- btn-outline-info (cyan) -->
<!-- btn-outline-dark (dark) -->
<!-- btn-outline-light (light) -->
```

### Add Provider-Specific Icons

You can enhance the buttons with provider icons (requires additional CSS or icon library):

```html
<button type="submit" class="btn btn-outline-secondary py-2">
	<i class="bi bi-google"></i> @provider.DisplayName
</button>
```

Or using Font Awesome:

```html
<button type="submit" class="btn btn-outline-secondary py-2">
	<i class="fab fa-@GetProviderIcon(provider.Name)"></i> @provider.DisplayName
</button>
```

### Add Brand Colors

Create CSS classes for each provider:

```css
.btn-google {
	border-color: #4285F4;
	color: #4285F4;
}

.btn-google:hover {
	background-color: #4285F4;
	border-color: #4285F4;
	color: white;
}

.btn-microsoft {
	border-color: #0078D4;
	color: #0078D4;
}

.btn-microsoft:hover {
	background-color: #0078D4;
	border-color: #0078D4;
	color: white;
}

.btn-github {
	border-color: #24292E;
	color: #24292E;
}

.btn-github:hover {
	background-color: #24292E;
	border-color: #24292E;
	color: white;
}
```

Then use conditionally:

```html
@foreach (var provider in Model.ExternalLogins)
{
	var providerClass = provider.Name.ToLower() switch
	{
		"google" => "btn-google",
		"microsoft" => "btn-microsoft",
		"github" => "btn-github",
		_ => "btn-outline-secondary"
	};

	<button type="submit" 
			class="btn py-2 text-uppercase small @providerClass" 
			name="provider" 
			value="@provider.Name">
		@provider.DisplayName
	</button>
}
```

---

## Accessibility

The current implementation includes:
- ✅ `title` attributes for tooltips
- ✅ `name` attribute for form submission
- ✅ Semantic form element usage
- ✅ Clear button labels
- ✅ Color isn't the only indicator (text labels present)

### Further Improvements (Optional)

Add ARIA labels:
```html
<button type="submit" 
		class="btn btn-outline-secondary py-2 text-uppercase small" 
		name="provider" 
		value="@provider.Name"
		aria-label="Log in using your @provider.DisplayName account">
	@provider.DisplayName
</button>
```

---

## Mobile Responsive

The layout is mobile-responsive:
- Buttons stack vertically on small screens (due to `d-grid gap-2`)
- Full width on all screen sizes
- Padding scales appropriately
- Text size adjusts with `small` class

---

## Browser Support

The social login functionality works on all modern browsers:
- ✅ Chrome/Edge (latest)
- ✅ Firefox (latest)
- ✅ Safari (latest)
- ✅ Mobile browsers

Required for OAuth redirects:
- ✅ Cookies enabled
- ✅ JavaScript enabled
- ✅ Third-party cookies (for OAuth flow)

---

## Testing

### Manual Testing Steps

1. **Without credentials**:
   - Empty `Google:ClientId`, `Microsoft:ClientId`, `GitHub:ClientId` in config
   - Social buttons should NOT appear

2. **With Google credentials**:
   - Set `Google:ClientId` and `Google:ClientSecret` in user secrets
   - Google button should appear
   - Click button → Redirects to Google login
   - Complete OAuth flow → User created and signed in

3. **With Microsoft credentials**:
   - Set `Microsoft:ClientId` and `Microsoft:ClientSecret` in user secrets
   - Microsoft button should appear
   - Click button → Redirects to Microsoft login
   - Complete OAuth flow → User created and signed in

4. **Return URL handling**:
   - Add `?returnUrl=/path` to Login/Register URLs
   - After successful OAuth, user should be redirected to return URL

---

## Performance Considerations

- **No additional database queries** for button rendering
- **Client-side** button visibility (determined at page load)
- **Minimal CSS overhead** (uses Bootstrap defaults)
- **No JavaScript required** (pure HTML form submission)

---

## Security Notes

- ✅ CSRF tokens automatically added by ASP.NET Core
- ✅ `asp-antifrogery="true"` in Login/Register forms
- ✅ OAuth flow handled securely by ASP.NET Core Identity
- ✅ Secrets never exposed in HTML/JavaScript
- ✅ HTTPS required for OAuth redirects
