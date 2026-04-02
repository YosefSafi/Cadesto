using Microsoft.Graph;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Cadesto.Logic.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class GraphEmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly GraphServiceClient _graphClient;

    public GraphEmailService(IConfiguration config)
    {
        _config = config;

        // Placeholders for MS Graph configuration
        // You will need to register an app in Azure Entra ID (Azure AD) and get these values.
        var tenantId = _config["AzureAd:TenantId"]; // The Directory (tenant) ID
        var clientId = _config["AzureAd:ClientId"]; // The Application (client) ID
        var clientSecret = _config["AzureAd:ClientSecret"]; // The Client Secret created in App Registrations

        // This is a placeholder implementation. 
        // In a real scenario, you would use ClientSecretCredential to authenticate.
        // var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        // _graphClient = new GraphServiceClient(credential);
        
        // Initializing with a dummy client for now to avoid crashes if config is missing
        _graphClient = new GraphServiceClient(new DelegateAuthenticationProvider((requestMessage) => Task.CompletedTask));
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Placeholder for sending email via Microsoft Graph API
        Console.WriteLine($"[GraphEmailService] Sending email to {to} with subject '{subject}'");
        
        /* Actual implementation would look like this:
        var message = new Message
        {
            Subject = subject,
            Body = new ItemBody
            {
                ContentType = BodyType.Html,
                Content = body
            },
            ToRecipients = new List<Recipient>
            {
                new Recipient { EmailAddress = new EmailAddress { Address = to } }
            }
        };

        await _graphClient.Users[_config["AzureAd:SenderEmail"]]
            .SendMail(message, false)
            .Request()
            .PostAsync();
        */

        await Task.CompletedTask;
    }
}
 public class DelegateAuthenticationProvider : IAuthenticationProvider
 {
     private readonly Func<System.Net.Http.HttpRequestMessage, Task> _authenticateRequestAsyncDelegate;

     public DelegateAuthenticationProvider(Func<System.Net.Http.HttpRequestMessage, Task> authenticateRequestAsyncDelegate)
     {
         _authenticateRequestAsyncDelegate = authenticateRequestAsyncDelegate;
     }

     public Task AuthenticateRequestAsync(System.Net.Http.HttpRequestMessage request)
     {
         return _authenticateRequestAsyncDelegate(request);
     }
 }
