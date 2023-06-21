using IdContext.Core.Enumerable;
using IdContext.Core.ValueObject;
using Microsoft.AspNetCore.Identity;

namespace IdContext.Core.Entity;

public class User : IdentityUser
{
    public UserStatus Status { get; private set; } = default!;
    // It distiguish between users that have been authenticated by a Social Provider, they are not going to use two factor, becouse theirs provider is already implenenting it.
    public bool IsAuthenticatedExternaly { get; private set; } = default!;
    public UserProfile? Profile { get; private set; }

    private User() { }
    public static User CreateExternalUser(string email)
    {
        var newUser = new User
        {
            Status = UserStatus.Active,
            Email = email,
            EmailConfirmed = true,
            UserName = email,
            IsAuthenticatedExternaly = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
        };
        return newUser;
    }

    public static User CreateLocalUser(string email)
    {
        var newUser = new User
        {
            Status = UserStatus.Active,
            Email = email,
            UserName = email,
            IsAuthenticatedExternaly = false,
            TwoFactorEnabled = false,
            LockoutEnabled = false
        };
        return newUser;
    }


    public void UseTwoFactor()
    {
        TwoFactorEnabled = true;
    }

    public void DeactiveTwoFactor()
    {
        TwoFactorEnabled = false;
    }

    public string? HideEmail()
    {
        if (Email is not null)
        {

            string[] local = Email.Split("@");
            string domain = local[1];
            string email = local[0].Substring(0, 3);
            local = local[0].Split(email);
            string text = string.Empty;
            foreach (char st in local[1]) text += "*";
            return $"{email}{text}@{domain}";
        }
        else
        {
            return null;
        }
    }

    public string? HidePhone()
    {
        if (PhoneNumber is not null)
        {

            string number = PhoneNumber.Substring(PhoneNumber.Length - 3);
            string[] phone = PhoneNumber.Split(number);
            string hidden = string.Empty;
            foreach (char st in phone[0]) hidden += "*";
            return $"{hidden}{number}";
        }
        else
        {
            return null;
        }
    }


}