using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using ScentifyWebApp.Infrastructure.ModelConfigurations;
using ScentifyWebApp.Models.Entities;
using ScentifyWebApp.Models;
using System.Text;
using ScentifyWebApp.Infrastructure.Services.Contracts;

namespace ScentifyWebApp.Infrastructure.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IWebHostEnvironment _env;

        public EmailService(IWebHostEnvironment env,
            IOptions<EmailSettings> emailSettings)
        {
            _env = env;
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(List<CartItem> cartItems, CustomerInfo? customerInfo)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailSettings.SenderEmail));
                email.To.Add(MailboxAddress.Parse(customerInfo.EmailAddress));
                email.Subject = "DOUCÉ – Đơn hàng của bạn đã vào hệ thống";

                var filePath = Path.Combine(_env.WebRootPath, "email-templates", "order-confirmation.html");
                var template = await File.ReadAllTextAsync(filePath);
                // 2) Build the items rows
                var sbRows = new StringBuilder();
                decimal amount = 0;

                foreach (var item in cartItems)
                {
                    var perfume = item.Product;
                    var currentProductSizes = perfume.ProductSizes?.FirstOrDefault(x => x.VolumeMl == perfume.CurrentSize);

                    sbRows.AppendLine($@"
                            <tr>
                              <td>
                                <strong>{item.Product.Name}</strong><br>
                                {item.Product.CurrentSize} mL
                              </td>
                              <td>{item.Quantity}</td>
                              <td>{item.Product.Currency} {currentProductSizes?.Price.ToString("N0")}</td>
                            </tr>");
                    amount += ((currentProductSizes?.Price ?? 0) * item.Quantity);
                }

                // 3) Replace placeholders
                var bodyTemp = template
                   .Replace("{{CustomerFullName}}", customerInfo.FullName)
                   .Replace("{{PhoneNumber}}", customerInfo.PhoneNumber)
                   .Replace("{{ShippingAddress}}", customerInfo.Address)
                   .Replace("{{FormattedTotal}}", "VND " + amount.ToString("N0"))
                   .Replace("{{Year}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                   .Replace("{{Items}}", sbRows.ToString());

                email.Body = new TextPart("html") { Text = bodyTemp };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
