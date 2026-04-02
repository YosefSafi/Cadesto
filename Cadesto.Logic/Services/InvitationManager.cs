using Cadesto.Data;
using Cadesto.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadesto.Logic.Services;

public interface IInvitationManager
{
    Task<string> CreateInvitationAsync(string email);
    Task<bool> ValidateInvitationAsync(string email, string token);
    Task UseInvitationAsync(string email, string token);
}

public class InvitationManager : IInvitationManager
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public InvitationManager(ApplicationDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<string> CreateInvitationAsync(string email)
    {
        var token = Guid.NewGuid().ToString();
        var invitation = new Invitation
        {
            Email = email,
            Token = token,
            Expiry = DateTime.Now.AddDays(7),
            IsUsed = false
        };

        _context.Invitations.Add(invitation);
        await _context.SaveChangesAsync();

        await _emailService.SendEmailAsync(email, "You are invited to Cadesto", $"Please use this token to register: {token}");

        return token;
    }

    public async Task<bool> ValidateInvitationAsync(string email, string token)
    {
        var invitation = await _context.Invitations
            .FirstOrDefaultAsync(i => i.Email == email && i.Token == token && !i.IsUsed && i.Expiry > DateTime.Now);
        
        return invitation != null;
    }

    public async Task UseInvitationAsync(string email, string token)
    {
        var invitation = await _context.Invitations
            .FirstOrDefaultAsync(i => i.Email == email && i.Token == token);

        if (invitation != null)
        {
            invitation.IsUsed = true;
            await _context.SaveChangesAsync();
        }
    }
}
