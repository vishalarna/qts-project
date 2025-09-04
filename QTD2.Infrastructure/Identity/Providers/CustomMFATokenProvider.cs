using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.ErrorHandler;
using QTD2.Infrastructure.QTD2Auth.Settings;
using System;
using System.Threading.Tasks;

public class CustomMfaTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser> where TUser : class
{
    private readonly CustomMfaSettings _customMfaSettings;

    public CustomMfaTokenProvider(IOptions<CustomMfaSettings> options)
    {
        _customMfaSettings = options.Value;
    }

    public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
        return Task.FromResult(true);
    }

    public async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
    {
        await manager.RemoveAuthenticationTokenAsync(user, "CustomMfaTokenProvider", purpose);

        Random random = new Random();
        int token = random.Next(100000, 1000000);
        string tokenString = token.ToString();

        await manager.SetAuthenticationTokenAsync(user, "CustomMfaTokenProvider", purpose, $"{tokenString}|{DateTime.UtcNow}");

        return tokenString;
    }

    public async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
    {
        var savedToken = await manager.GetAuthenticationTokenAsync(user, "CustomMfaTokenProvider", purpose);

        if (string.IsNullOrEmpty(savedToken))
            return false;

        var parts = savedToken.Split('|');
        if (parts.Length != 2)
            return false;

        var savedTokenValue = parts[0];
        if (savedTokenValue != token)
            return false;

        if (!DateTime.TryParse(parts[1], out var timestamp))
            return false;

        // Check if the token has expired
        if (DateTime.UtcNow > timestamp.Add(TimeSpan.FromMinutes(_customMfaSettings.TokenLifetimeMinutes)))
            return false;

        // Optionally, remove the token after successful validation
        await manager.RemoveAuthenticationTokenAsync(user, "CustomMfaProvider", purpose);

        return true;
    }
}