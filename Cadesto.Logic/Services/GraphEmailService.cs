using Microsoft.Extensions.Configuration;

namespace Cadesto.Logic.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class GraphEmailService : IEmailService
{
    private readonly IConfiguration _config;

    public GraphEmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Placeholder for sending email via Microsoft Graph API
        // In a real implementation, you would use GraphServiceClient from Microsoft.Graph
        Console.WriteLine($"[GraphEmailService] Mock sending email to {to} with subject '{subject}'");
        
        /* 
        var tenantId = _config["AzureAd:TenantId"];
        var clientId = _config["AzureAd:ClientId"];
        var clientSecret = _config["AzureAd:ClientSecret"];
        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        var graphClient = new GraphServiceClient(credential);
        
        var requestBody = new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody { ... };
        await graphClient.Users[_config["AzureAd:SenderEmail"]].SendMail.PostAsync(requestBody);
        */

        await Task.CompletedTask;
    }
}
