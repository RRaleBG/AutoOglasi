namespace AutoOglasi.Services.Email;

using System;
using System.Net;
using System.Text;

public static class EmailTemplateBuilder
{
    private const string BrandName = "AutoOglasi";
    private const string BrandColor = "#d4af37";
    private const string BackgroundColor = "#0f0f10";
    private const string CardColor = "#171719";
    private const string TextColor = "#f4f4f4";
    private const string MutedColor = "#b7b7b7";
    private const string BorderColor = "rgba(212, 175, 55, 0.18)";

    public static string BuildEmailConfirmationEmail(string confirmationUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(confirmationUrl);

        return BuildTemplate(
            eyebrow: "Account verification",
            heading: "Confirm your email address",
            lead: "Thanks for joining AutoOglasi. Confirm your email address to activate your account and start using the platform.",
            buttonText: "Confirm email",
            buttonUrl: confirmationUrl,
            noteLabel: "If the button does not work, copy and paste this link into your browser:",
            noteValue: confirmationUrl,
            footer: "If you did not create this account, you can safely ignore this message.");
    }

    public static string BuildNewPostNotificationEmail(string postTitle, string postUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(postTitle);
        ArgumentException.ThrowIfNullOrWhiteSpace(postUrl);

        return BuildTemplate(
            eyebrow: "New listing published",
            heading: "A new ad is live on AutoOglasi",
            lead: $"A new listing has just been published: <strong>{Encode(postTitle)}</strong>. Open it to view the full details, images, and seller information.",
            buttonText: "View listing",
            buttonUrl: postUrl,
            noteLabel: "If the button does not work, copy and paste this link into your browser:",
            noteValue: postUrl,
            footer: "You received this message because you have an AutoOglasi account.");
    }

    private static string BuildTemplate(
        string eyebrow,
        string heading,
        string lead,
        string buttonText,
        string buttonUrl,
        string noteLabel,
        string noteValue,
        string footer)
    {
        var encodedButtonText = Encode(buttonText);
        var encodedButtonUrl = Encode(buttonUrl);
        var encodedNoteLabel = Encode(noteLabel);
        var encodedNoteValue = Encode(noteValue);
        var encodedFooter = Encode(footer);

        var builder = new StringBuilder();
        builder.Append("<!doctype html>");
        builder.Append("<html lang=\"en\">");
        builder.Append("<head>");
        builder.Append("<meta charset=\"utf-8\" />");
        builder.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");
        builder.Append($"<title>{Encode(heading)}</title>");
        builder.Append("</head>");
        builder.Append($"<body style=\"margin:0;padding:0;background:{BackgroundColor};font-family:Arial,Helvetica,sans-serif;color:{TextColor};\">");
        builder.Append($"<div style=\"display:none;max-height:0;overflow:hidden;opacity:0;color:transparent;\">{Encode(eyebrow)} · {Encode(BrandName)}</div>");
        builder.Append("<table role=\"presentation\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"background:");
        builder.Append(BackgroundColor);
        builder.Append(";padding:32px 12px;\">");
        builder.Append("<tr><td align=\"center\">");
        builder.Append($"<table role=\"presentation\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"max-width:640px;background:{CardColor};border:1px solid {BorderColor};border-radius:18px;overflow:hidden;\">");
        builder.Append("<tr><td style=\"padding:32px 32px 12px;text-align:center;\">");
        builder.Append($"<div style=\"font-size:12px;letter-spacing:4px;text-transform:uppercase;color:{BrandColor};font-weight:700;\">{Encode(BrandName)}</div>");
        builder.Append($"<div style=\"width:64px;height:1px;background:{BrandColor};margin:16px auto 0;\"></div>");
        builder.Append("</td></tr>");
        builder.Append("<tr><td style=\"padding:8px 32px 0;text-align:center;\">");
        builder.Append($"<div style=\"font-size:12px;letter-spacing:3px;text-transform:uppercase;color:{BrandColor};margin-bottom:12px;\">{Encode(eyebrow)}</div>");
        builder.Append($"<h1 style=\"margin:0 0 16px;font-size:28px;line-height:1.25;font-weight:300;color:{TextColor};text-transform:uppercase;letter-spacing:1px;\">{Encode(heading)}</h1>");
        builder.Append($"<p style=\"margin:0 auto 24px;max-width:520px;font-size:16px;line-height:1.7;color:{MutedColor};\">{lead}</p>");
        builder.Append($"<a href=\"{encodedButtonUrl}\" style=\"display:inline-block;padding:14px 28px;border:1px solid {BrandColor};color:{BackgroundColor};background:{BrandColor};text-decoration:none;font-size:13px;font-weight:700;letter-spacing:2px;text-transform:uppercase;border-radius:999px;\">{encodedButtonText}</a>");
        builder.Append("</td></tr>");
        builder.Append("<tr><td style=\"padding:24px 32px 0;text-align:center;\">");
        builder.Append($"<p style=\"margin:0 0 6px;font-size:13px;line-height:1.6;color:{MutedColor};\">{encodedNoteLabel}</p>");
        builder.Append($"<p style=\"margin:0;font-size:13px;line-height:1.6;color:{BrandColor};word-break:break-all;\">{encodedNoteValue}</p>");
        builder.Append("</td></tr>");
        builder.Append("<tr><td style=\"padding:24px 32px 32px;text-align:center;\">");
        builder.Append($"<p style=\"margin:0;font-size:12px;line-height:1.7;color:{MutedColor};\">{encodedFooter}</p>");
        builder.Append("</td></tr>");
        builder.Append("</table>");
        builder.Append($"<p style=\"margin:16px 0 0;font-size:11px;line-height:1.5;color:{MutedColor};text-align:center;\">© {DateTime.UtcNow.Year} {Encode(BrandName)}</p>");
        builder.Append("</td></tr></table></body></html>");

        return builder.ToString();
    }

    private static string Encode(string value)
    {
        return WebUtility.HtmlEncode(value);
    }
}
