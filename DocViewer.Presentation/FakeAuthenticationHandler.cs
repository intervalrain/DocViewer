using System.Security.Claims;
using System.Text.Encodings.Web;

using DocViewer.Application.Common.Security.Roles;
using DocViewer.Application.Common.Security.Permissions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace DocViewer.Presentation;

public class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    [Obsolete]
    public FakeAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var user = new
        {
            UserId = "00053997",
            UserName = "Rain Hu",
            Email = "rain_hu@umc.com"
        };

        var roles = new string[] { Role.User };

        var permissions = Permission.All;

        var claims = new List<Claim>
        {
            new Claim("id", user.UserId),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permissions", permission));
        }

        var identity = new ClaimsIdentity(claims, "FakeScheme");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "FakeScheme");

        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

