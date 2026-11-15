namespace AutoOglasi.Web.Infrastructure;

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

public sealed class SmtpEmailSender(IConfiguration configuration) : IEmailSender
{
    private const string EmailSettingsSectionName = "EmailSettings";

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(subject);
        ArgumentException.ThrowIfNullOrWhiteSpace(htmlMessage);

        var emailSettings = configuration.GetSection(EmailSettingsSectionName);
        var smtpHost = GetRequiredSetting(emailSettings, "SmtpHost");
        var smtpPort = emailSettings.GetValue<int?>("SmtpPort") ?? 25;
        var smtpUserName = GetRequiredSetting(emailSettings, "Username");
        var smtpPassword = GetRequiredSetting(emailSettings, "Password");
        var fromAddress = emailSettings["From"];
        var secureMode = emailSettings["Secure"];
        var enableSsl = GetEnableSsl(emailSettings.GetValue<bool?>("EnableSsl"), secureMode);

        using var client = new SmtpClient(smtpHost, smtpPort)
        {
            EnableSsl = enableSsl,
            Credentials = new NetworkCredential(smtpUserName, smtpPassword)
        };

        using var message = new MailMessage
        {
            From = new MailAddress(string.IsNullOrWhiteSpace(fromAddress) ? smtpUserName : fromAddress),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(email);
        await client.SendMailAsync(message).ConfigureAwait(false);
    }

    private static string GetRequiredSetting(IConfigurationSection emailSettings, string key)
    {
        var value = emailSettings[key];
        return string.IsNullOrWhiteSpace(value)
            ? throw new InvalidOperationException(FormattableString.Invariant($"Missing required EmailSettings:{key} configuration value."))
            : value;
    }

    private static bool GetEnableSsl(bool? enableSslSetting, string? secureMode)
    {
        if (!string.IsNullOrWhiteSpace(secureMode))
        {
            return secureMode.Trim().Equals("StartTls", StringComparison.OrdinalIgnoreCase)
                || secureMode.Trim().Equals("Ssl", StringComparison.OrdinalIgnoreCase)
                || secureMode.Trim().Equals("Tls", StringComparison.OrdinalIgnoreCase);
        }

        return enableSslSetting ?? false;
    }
}
